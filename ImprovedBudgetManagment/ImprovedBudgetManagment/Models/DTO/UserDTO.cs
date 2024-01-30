using System.ComponentModel.DataAnnotations;

namespace ImprovedBudgetManagment.Models.DTO;

public class UserDTO
{

    public  string FirstName { get; set; }

    public  string LastName { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}