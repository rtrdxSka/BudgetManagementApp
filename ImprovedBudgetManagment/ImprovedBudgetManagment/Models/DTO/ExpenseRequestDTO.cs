using System.ComponentModel.DataAnnotations;

namespace ImprovedBudgetManagment.Models.DTO;

public class ExpenseRequestDTO
{
    [Required]
    public required DateTime Date { get; set; }
    [Required]
    public required string Category { get; set; }
    [Required]
    public required decimal Amount { get; set; }
    [Required]
    public required string Type { get; set; }
    [Required]
    public required string Email { get; set; }
}