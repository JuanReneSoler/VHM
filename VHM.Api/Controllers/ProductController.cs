using Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;
using Models.Entities;
using Core.Data.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Models.Dtos;

namespace VHM.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _repository;
    private readonly IUnityOfWorkContext _uow;
    private readonly IMapper _mapper;
    public ProductsController(IUnityOfWorkContext uow, IMapper mapper)
    {
        _repository = uow.GetRepository<Product>();
        _uow = uow;
	_mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<dynamic> GetAll() =>
           (await _repository.GetAllAsync(x => x.Include(y => y.Proveedor).Include(y => y.Type)))
           .Select(product => new
           {
               Id= product.Id,
               Name = product.Name,
               Type = product.Type.Description,
               Provider = product.Proveedor.Nombre,
	       Price = product.PriceUnit,
	       Description = product.Description
           }).ToList();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Add(ProductDto model)
    {
	var entity = _mapper.Map<Product>(model);
        await _repository.AddAsync(entity);
        await _uow.CommitAsync();
        return Ok();
    }

    [HttpDelete]
    [AllowAnonymous]
    public async Task<IActionResult> Delete(int Id)
    {
        await _repository.DeleteAsync(Id);
        await _uow.CommitAsync();
        return Ok();
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Update(ProductDto model)
    {
	var entity = _mapper.Map<Product>(model);
        await _repository.UpdateAsync(entity);
        _uow.CommitAsync();
        return Ok();
    }
}
