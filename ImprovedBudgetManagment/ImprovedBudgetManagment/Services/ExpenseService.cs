using ImprovedBudgetManagment.Data;
using ImprovedBudgetManagment.Models.DTO;
using ImprovedBudgetManagment.Models.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImprovedBudgetManagment.Services;

public class ExpenseService(AppDbContext dbContext, UserManager<User> userManager) : IExpensService
{
    
    public async Task SetExpense(ExpenseRequestDTO expenseRequestDto)
    {
        try
        {
            var user = await GetUser(expenseRequestDto.Email);


            var expense = new IncomeExpenseRecord
            {
                Date = expenseRequestDto.Date,
                Category = expenseRequestDto.Category,
                Amount = expenseRequestDto.Amount,
                Type = expenseRequestDto.Type,
                UserId = user.Id
            };
            await dbContext.IncomeExpenseRecords.AddAsync(expense);
            await dbContext.SaveChangesAsync();
        }
        catch(Exception e)
        {
            throw;
        }

        
    }

    public async Task<List<ExpenseResponseDTO>> GetExpense(string email, string type)
    {
        var user = await GetUser(email);
        
        return await dbContext.IncomeExpenseRecords
            .Where(i => user.Id == i.UserId && type== i.Type)
            .Select(i => new ExpenseResponseDTO
            {
                Category = i.Category,
                Amount = i.Amount,
                Date = i.Date
            })
            .ToListAsync();
    }
    public async Task<List<ExpenseResponseDTO>> GetExpense(string email, string type, string category)
    {
        var user = await GetUser(email);
        
        return await dbContext.IncomeExpenseRecords
            .Where(i => user.Id == i.UserId && i.Category == category && type==i.Type)
            .Select(i => new ExpenseResponseDTO
            {
                Category = i.Category,
                Amount = i.Amount,
                Date = i.Date
            })
            .ToListAsync();
    }

    private async Task<User> GetUser(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("User not Found");

        return user;
    }
    
}