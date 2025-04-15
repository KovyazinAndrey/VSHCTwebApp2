using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VSHCTwebApp.Components.Models;

namespace VSHCTwebApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Competence> Competence { get; set; }
        public DbSet<UserCompetence> UserCompetence { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Competence>().ToTable("Competence");

            builder.Entity<UserCompetence>(entity =>
            {
                // Составной первичный ключ
                entity.HasKey(uc => new { uc.UserId, uc.CompetenceId });

                // Настройка связей
                entity.HasOne(uc => uc.User)
                    .WithMany(u => u.UserCompetences)
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uc => uc.Competence)
                    .WithMany(c => c.UserCompetences)
                    .HasForeignKey(uc => uc.CompetenceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация для Competence
            builder.Entity<Competence>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Category).HasConversion<string>();

                entity.HasData(
                    new Competence { Id = 1, Name = "C#", Category = CompetenceCategory.ProgrammingLanguage },
                    new Competence { Id = 2, Name = "JavaScript", Category = CompetenceCategory.ProgrammingLanguage },
                    new Competence { Id = 3, Name = "ASP.NET Core", Category = CompetenceCategory.Framework },
                    new Competence { Id = 4, Name = "React", Category = CompetenceCategory.Framework },
                    new Competence { Id = 5, Name = "SQL Server", Category = CompetenceCategory.Database },
                    new Competence { Id = 6, Name = "PostgreSQL", Category = CompetenceCategory.Database },
                    new Competence { Id = 7, Name = "Docker", Category = CompetenceCategory.DevOps },
                    new Competence { Id = 8, Name = "Kubernetes", Category = CompetenceCategory.DevOps }
                );
            });
        }
    }
}
