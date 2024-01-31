using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImprovedBudgetManagment.Models.Entitites;

public class IncomeExpenseRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(50)]
    public string Category { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(100)]
    public string Type { get; set; } // "Income" or "Expense"

    public string UserId { get; set; } = null!;
    
    public User User { get; set; } = null!;
}