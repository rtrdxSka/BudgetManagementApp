using System.Text.Json.Serialization;

namespace BudgetManagment.RequestModels;

public class RegisterRequest
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
}