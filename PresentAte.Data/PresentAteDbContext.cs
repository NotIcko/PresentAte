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
        public DbSet<Comment> Comments { get; set; } = null!;

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

            builder.Entity<Comment>()
                .HasOne(s => s.Essay)
                .WithMany(e => e.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed initial themes
            builder.Entity<EssayTheme>().HasData(
                new EssayTheme { ThemeId = 1, ThemeName = "Мълчанието - сила или безсилие" },
                new EssayTheme { ThemeId = 2, ThemeName = "Живот в мрежата" },
                new EssayTheme { ThemeId = 3, ThemeName = "Тежестта на думите" },
                new EssayTheme { ThemeId = 4, ThemeName = "Смисълът на благодеянието - Йордан Йовков \" Песента на колелетата \"" },
                new EssayTheme { ThemeId = 5, ThemeName = "Съдбовният избор на човека - Йордан Йовков \" Шибил \"" },
                new EssayTheme { ThemeId = 6, ThemeName = "Човекът и неговите съмнения - Елин Пелин \" Чорба от греховете на отец Никодим \"" },
                new EssayTheme { ThemeId = 7, ThemeName = "Родното и чуждото - Алеко Константинов \" Бай Ганьо \"" },
                new EssayTheme { ThemeId = 8, ThemeName = "Реалност и измислица - Елин Пелин \" Косачи \"" }, new EssayTheme { ThemeId = 9, ThemeName = "Доброта и самота - Йордан Йовков \" Серафим \"" },
                new EssayTheme { ThemeId = 10, ThemeName = "Човекът и вярата - Никола Вапцаров \" Вяра \"" }
            );
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
