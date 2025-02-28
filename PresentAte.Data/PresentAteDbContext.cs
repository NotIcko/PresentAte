namespace PresentAte.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PresentAte.Data.Models;
    using System.Reflection.Emit;

    public class PresentAteDbContext : IdentityDbContext<ApplicationUser>
    {
        public PresentAteDbContext() { }

        public PresentAteDbContext(
            DbContextOptions<PresentAteDbContext> options)
            : base(options) { }

        public virtual DbSet<ApplicationUser> Users { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Presentation> Presentations { get; set; } = null!;
        public DbSet<EssayTheme> EssayThemes { get; set; } = null!;
        public DbSet<Essay> Essays { get; set; } = null!;
        public DbSet<EssaySuggestion> EssaySuggestions { get; set; } = null!;

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

            builder.Entity<Essay>()
            .HasOne(e => e.User)
            .WithMany(u => u.Essays)
            .OnDelete(DeleteBehavior.NoAction); 

            builder.Entity<Essay>()
                .HasOne(e => e.Theme)
                .WithMany(t => t.Essays)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EssaySuggestion>()
                .HasOne(s => s.Essay)
                .WithMany(e => e.Suggestions)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
