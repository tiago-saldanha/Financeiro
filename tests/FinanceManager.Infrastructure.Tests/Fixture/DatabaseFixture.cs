using FinanceManager.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Tests.Data
{
    public class DatabaseFixture
    {
        public AppDbContext CreateContext()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}