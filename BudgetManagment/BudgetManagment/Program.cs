using System.Text.Json.Serialization;
using BudgetManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Server=KSTOICHKOV-DW;Database=budgetManagment;User Id=sa;Password=123;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BudgetApp", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Enable OAuth2 implicit flow for Swagger UI
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
