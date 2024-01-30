using Microsoft.AspNetCore.Identity;

namespace ImprovedBudgetManagment.Models.Entitites;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime?  RefreshTokenValidity { get; set; }
}