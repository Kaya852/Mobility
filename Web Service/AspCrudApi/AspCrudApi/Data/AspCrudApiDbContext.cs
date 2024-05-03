using AspCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCrudApi.Data
{
    public class AspCrudApiDbContext:DbContext
    {
        public AspCrudApiDbContext(DbContextOptions<AspCrudApiDbContext> options) : base(options)
        {
            
        }
        public DbSet<Record> Record { get; set; } = default!; //Tablo
    }
}
