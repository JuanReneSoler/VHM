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

namespace PDE.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var str = JsonConvert.SerializeObject(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.UserData, str),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

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

#region probar este metodo
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.UserName);
        if (userExists != null)
           return BadRequest("El usuario ya existe!"); 

        var user =  new IdentityUser()
        {
        };
        
        var result = await _userManager.CreateAsync(user, $"Pde@{model.UserName}");
        if (!result.Succeeded)
           return BadRequest("la creacion del usuario ha fallado!"); 

        return Ok("Usuario creado correctamente");
    }
#endregion

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

