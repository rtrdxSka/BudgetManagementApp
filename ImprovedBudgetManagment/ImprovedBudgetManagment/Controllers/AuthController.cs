using System.Net;
using ImprovedBudgetManagment.InfraSructures;
using ImprovedBudgetManagment.Models.DTO;
using ImprovedBudgetManagment.Models.Entitites;
using ImprovedBudgetManagment.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace ImprovedBudgetManagment.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    private readonly IAuthService _authService;

    public AuthController(UserManager<User> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDTO userDto)
    {
        var user = new User
        {
            UserName = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email
        };
        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] UserDTO model)
    {
        try
        {
            var user = await _authService.LoginUser(model);
            if (user is null)
                return Unauthorized();
        
            var token = _authService.GenerateJwtToken(user);
            var refreshToken = await _authService.GenerateRefreshToken(user);
            
            Response.Cookies.Append("RefreshToken", refreshToken, CookieOptionManager.GenerateRefreshCookieOptions());
            Response.Cookies.Append("AccessToken", token, CookieOptionManager.GenerateAccessCookieOptions());
        
            return Ok(new {user.Email,user.FirstName,user.LastName});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutDTO logoutDto)
    {
       var user =  await _userManager.FindByEmailAsync(logoutDto.Email);
       if (user != null)
       {
          await _authService.LogoutUser(user);
         

          
          return Ok("Success");

       }

       return NotFound();
    }
    
}