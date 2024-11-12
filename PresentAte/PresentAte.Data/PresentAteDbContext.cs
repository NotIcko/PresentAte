namespace PresentAte.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PresentAte.Data.Models;

    public class PresentAteDbContext : IdentityDbContext<User>
    {
        public PresentAteDbContext() { }

        public PresentAteDbContext(
            DbContextOptions<PresentAteDbContext> options)
            : base(options) { }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Presentation> Presentations { get; set; } = null!;

        protected override void OnModelCreating(
            ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<History>()
                .HasKey(h => new 
                { 
                    h.UserId, 
                    h.PresentationId, 
                });
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
