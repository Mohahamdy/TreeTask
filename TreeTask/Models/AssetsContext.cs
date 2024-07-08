using Microsoft.EntityFrameworkCore;

namespace TreeTask.Models
{
    public class AssetsContext: DbContext
    {
        public AssetsContext(DbContextOptions<AssetsContext> options) : base(options)
        { }

        public DbSet<AssetsTree> AssetsTrees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
