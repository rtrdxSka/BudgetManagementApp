using BudgetManagment.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public LoginController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

        if (user != null)
            return Accepted();
        return NotFound();
    }
}