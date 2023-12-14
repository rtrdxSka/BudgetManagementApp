using BudgetManagment;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class IncomeController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public IncomeController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut("{id}")]
    [HttpGet]
    public IActionResult GetIncome(int id)
    {
        // Validate unique email
        if (!_dbContext.Incomes.Any(i => i.Id == id))
        {
            return Conflict("Wrong id");
        }

        return Ok(_dbContext.Incomes.FirstOrDefault(i => i.Id == id)!.Value);
    }

    [HttpPut("{id}/{income}")]
    public IActionResult UpdateIncome(int id, int income)
    {
        // Validate unique email
        if (!_dbContext.Incomes.Any(i => i.Id == id))
        {
            return Conflict("Wrong id");
        }

        _dbContext.Incomes.FirstOrDefault(i => i.Id == id)!.Value = income;
        _dbContext.SaveChanges();

        return Ok("User registered successfully");
    }
}
