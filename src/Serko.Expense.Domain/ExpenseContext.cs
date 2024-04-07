using Microsoft.EntityFrameworkCore;
using Serko.Expense.Domain.Models;

namespace Serko.Expense.Domain;

public class ExpenseContext : DbContext
{
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Models.Expense> Expenses { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ExpenseContext(DbContextOptions<ExpenseContext> options)
        : base(options)
    {
        
    }
}
