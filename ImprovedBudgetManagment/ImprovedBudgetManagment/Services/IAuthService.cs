using ImprovedBudgetManagment.Models.DTO;
using ImprovedBudgetManagment.Models.Entitites;
using Microsoft.AspNetCore.Identity;

namespace ImprovedBudgetManagment.Services;

public interface IAuthService
{
    public string GenerateJwtToken(User user);
    public Task<string> GenerateRefreshToken(User user);
    public Task<User?> GetUserFromRefreshToken(string refreshToken);
    public Task<string?> GetEmailConfirmationToken(User user);
    public Task<User> RegisterUser(UserDTO model);
    public Task<User?> LoginUser(UserDTO model);
    public Task SignInUser(User user);
    public Task LogoutUser(User user);
    public Task<IdentityResult> ConfirmEmail(User user, string token);
    public Task<string> GenerateForgottenPasswordLink(User user);
    public Task<User?> GetUserByEmail(string email);
    public Task<User?> GetUserById(string id);
}