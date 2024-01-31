using ImprovedBudgetManagment.Models.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImprovedBudgetManagment.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions options) : base(options)  {}
    
    public DbSet<IncomeExpenseRecord> IncomeExpenseRecords { get; set; } = null!;
    
}