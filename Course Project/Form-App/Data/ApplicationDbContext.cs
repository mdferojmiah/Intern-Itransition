using Form_App.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Form_App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Template> Templates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Form> Forms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // one to many relationship between Creator and Template
            builder.Entity<Template>()
                .HasOne(t => t.Creator)
                .WithMany(a => a.Templates)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many relationship between Form and Template
            builder.Entity<Form>()
                .HasOne(a => a.Template)
                .WithMany(t => t.Forms)
                .HasForeignKey(f => f.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many relationship between Form and Respondent
            builder.Entity<Form>()
                .HasOne(a => a.Respondent)
                .WithMany(t => t.Forms)
                .HasForeignKey(f => f.RespondentId)
                .OnDelete(DeleteBehavior.Restrict);
            // one to many relationship between Question and Template
            builder.Entity<Question>()
                .HasOne(t => t.Template)
                .WithMany(u => u.Questions)
                .HasForeignKey(f => f.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many relationship between Question and Answer
            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(m => m.Answers)
                .HasForeignKey(f => f.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
            // one to many relationship between Form and Answer
            builder.Entity<Answer>()
                .HasOne(a => a.Form)
                .WithMany(m => m.Answers)
                .HasForeignKey(f => f.FormId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
