using ImprovedBudgetManagment.Models.DTO;
using ImprovedBudgetManagment.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ImprovedBudgetManagment.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ExpensesController(IExpensService expensService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostExpenses([FromBody]ExpenseRequestDTO expenseRequestDto)
    {
        await expensService.SetExpense(expenseRequestDto);

        return Ok("Success");

    }
    
    [HttpGet]
    public async Task<IActionResult> GetExpenses(string email, string type, string? category = null)
    {
        return category is null ? Ok(await expensService.GetExpense(email,type)) : Ok(await expensService.GetExpense(email,type,category));
    }
    
}