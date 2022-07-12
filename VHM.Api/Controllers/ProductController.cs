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

namespace VHM.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _repository;
    private readonly IUnityOfWorkContext _uow;
    public ProductsController(IUnityOfWorkContext uow)
    {
        _repository = uow.GetRepository<Product>();
	_uow = uow;
    }

    [HttpGet]
    public async Task<dynamic> GetAll() => (await _repository.GetAllAsync())
	.Select(product => new { product.Id, product.Name, product.Proveedor.Nombre, product.Type.Description});
    
    [HttpPost]
    public async Task<IActionResult> Add(object model)
    {
	await _repository.AddAsync(models);
	await _uow.CommitAsync();
	return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int Id)
    {
	_repository.Delete(Id);
	_uow.CommitAsync();
	return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(object model)
    {
	await _repository.UpdateAsync(model);
	_uow.CommitAsync();
	return Ok();
    }
}
