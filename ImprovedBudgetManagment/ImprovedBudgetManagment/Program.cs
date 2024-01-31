using ImprovedBudgetManagment.Data;
using ImprovedBudgetManagment.InfraSructures;
using ImprovedBudgetManagment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin",
        options => options.WithOrigins("http://localhost:3000",
                "https://localhost:3000", "http://localhost:3000/login")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IExpensService, ExpenseService>();

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddJwtToken(builder.Configuration);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();

