using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetIdentityOnly.Models
{
    public class IdentityDbConn:IdentityDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\Projects\\sqlite3.db\\IdentityOnly.db");
        }            
    }
}
