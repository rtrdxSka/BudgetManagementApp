using BudgetManagment.DbModels;
using BudgetManagment.Models;
using BudgetManagment.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BudgetManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public RegisterController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut]
    public IActionResult Register([FromBody] RegisterRequest model)
    {
        // Validate unique email
        if (_dbContext.Users.Any(u => u.Email == model.Email))
        {
            return Conflict("Email already exists");
        }

        // In a real-world scenario, hash the password before storing it
        var newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password, // Hash the password in a production scenario
            Email = model.Email
        };
       

        _dbContext.Users.Add(newUser);
        var expenses = new Expense()
        {
            Id = newUser.Id,
            Value = 0
        };
        var income = new Income()
        {
            Id = newUser.Id,
            Value = 0
        };
        _dbContext.Expenses.Add(expenses);
        _dbContext.Incomes.Add(income);
        // Access the ID of the added user
        _dbContext.SaveChanges();

        // You can use the addedUserId as needed, for example, returning it in the response
        return Ok(new { UserId = newUser.Id });
    }
}