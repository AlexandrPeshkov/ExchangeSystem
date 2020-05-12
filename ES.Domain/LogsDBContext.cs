using Microsoft.EntityFrameworkCore;

namespace ES.Data
{
    public class LogsDBContext : DbContext
    {
        public LogsDBContext(DbContextOptions<LogsDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
