using Microsoft.EntityFrameworkCore;
using Reminders.Models;

namespace Reminders.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Reminder> Reminders { get; set; }
        
    }
}