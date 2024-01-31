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
    public async Task<List<ExpenseResponseDTO>> GetExpense(string email, string type, string category, int? month, int? year)
    {
        var user = await GetUser(email);
        
        return await dbContext.IncomeExpenseRecords
            .Where(i => user.Id == i.UserId && i.Category == category && type==i.Type && i.Date.Month == month && i.Date.Year == year)
            .Select(i => new ExpenseResponseDTO
            {
                Category = i.Category,
                Amount = i.Amount,
                Date = i.Date,
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
    public async Task<List<string>> GetCategories()
    {
        return await dbContext.IncomeExpenseRecords.Select(i => i.Category).Distinct().ToListAsync();
    }
    public async Task<List<ChartDTO>> GetChartData(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("User not Found");

        var transactions = dbContext.IncomeExpenseRecords
            .Where(i => i.UserId == user.Id)
            .GroupBy(entry => new { entry.Date.Month, entry.Category, entry.Type })
            .Select(group => new
            {
                Month = group.Key.Month,
                Category = group.Key.Category,
                Type = group.Key.Type,
                TotalAmount = group.Sum(entry => entry.Amount)
            }).ToList();

        var categories = transactions.Select(t => t.Category).Distinct();

        List<ChartDTO> chartData = new List<ChartDTO>();

        foreach (var category in categories)
        {
            var data = new decimal[12]; // 12 months

            var categoryTransactions = transactions.Where(t => t.Category == category);

            foreach (var transaction in categoryTransactions)
            {
                int monthIndex = transaction.Month - 1; // Month is 1-based, array is 0-based
                data[monthIndex] += transaction.TotalAmount;
            }

            chartData.Add(new ChartDTO
            {
                Name = category,
                Data = data
            });
        }

        return chartData;
    }


}