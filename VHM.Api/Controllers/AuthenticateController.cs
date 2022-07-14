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
using Repositories;
using Core.Data.Repository;

namespace PDE.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<Empleado> _repo;
    private readonly IUnityOfWorkContext _uow;

    public AuthenticateController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
	IUnityOfWorkContext uow,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
	_repo = uow.GetRepository<Empleado>();
	_uow = uow;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var str = JsonConvert.SerializeObject(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.UserData, str),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var token = GetToken(authClaims);
            return Ok(new TokenModel
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                user = str,
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
	try{
        var userExists = await _userManager.FindByNameAsync(model.UserName);
        if (userExists != null)
            return BadRequest("El usuario ya existe!");

        var user = new IdentityUser
        {
            UserName = model.UserName,
        };

        var entity = new Empleado
        {
            Names = model.Names,
            Surnames = model.Surnames,
            UserId = user.Id,
            Birthday = model.Birthday,
            DocId = model.DocId,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return BadRequest("la creacion del usuario ha fallado!");
	await _repo.AddAsync(entity);
	await _uow.CommitAsync();
	}
	catch(Exception ex)
	{
	}
        return Ok("Usuario creado correctamente");
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

}

