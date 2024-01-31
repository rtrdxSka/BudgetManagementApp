namespace ImprovedBudgetManagment.Models.DTO;

public class ExpenseResponseDTO
{
    public required string Category { get; set; }
    
    public required decimal Amount { get; set; }
        
    public required DateTime Date { get; set; }
}