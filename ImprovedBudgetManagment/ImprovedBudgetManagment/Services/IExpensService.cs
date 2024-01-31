using ImprovedBudgetManagment.Models.DTO;

namespace ImprovedBudgetManagment.Services;

public interface IExpensService
{
    public Task SetExpense(ExpenseRequestDTO expenseRequestDto);

    public Task<List<ExpenseResponseDTO>> GetExpense(string email, string type);

    public Task<List<ExpenseResponseDTO>> GetExpense(string email, string type, string category);
}