using HabibaARR.Models;
using Action = HabibaARR.Models.Action;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabibaARR.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ActionPlan> ActionPlans { get; set; }
    public DbSet<Action> Actions { get; set; }
    public DbSet<Evidence> Evidences { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<ActionLog> ActionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ActionPlan configuration
        modelBuilder.Entity<ActionPlan>()
            .HasKey(ap => ap.Id);

        modelBuilder.Entity<ActionPlan>()
            .HasOne(ap => ap.Manager)
            .WithMany(u => u.CreatedActionPlans)
            .HasForeignKey(ap => ap.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ActionPlan>()
            .Property(ap => ap.Reference)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<ActionPlan>()
            .Property(ap => ap.Title)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<ActionPlan>()
            .HasIndex(ap => ap.Reference)
            .IsUnique();

        // Action configuration
        modelBuilder.Entity<Action>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Action>()
            .HasOne(a => a.ActionPlan)
            .WithMany(ap => ap.Actions)
            .HasForeignKey(a => a.ActionPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Action>()
            .HasOne(a => a.Responsible)
            .WithMany(u => u.AssignedActions)
            .HasForeignKey(a => a.ResponsibleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Action>()
            .HasOne(a => a.Manager)
            .WithMany()
            .HasForeignKey(a => a.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Action>()
            .Property(a => a.Reference)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Action>()
            .Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Action>()
            .HasIndex(a => a.Reference)
            .IsUnique();

        // Evidence configuration
        modelBuilder.Entity<Evidence>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Evidence>()
            .HasOne(e => e.Action)
            .WithMany(a => a.Evidences)
            .HasForeignKey(e => e.ActionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Evidence>()
            .HasOne(e => e.SubmittedBy)
            .WithMany()
            .HasForeignKey(e => e.SubmittedById)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Evidence>()
            .HasOne(e => e.ReviewedBy)
            .WithMany()
            .HasForeignKey(e => e.ReviewedById)
            .OnDelete(DeleteBehavior.SetNull);

        // Attachment configuration
        modelBuilder.Entity<Attachment>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.Evidence)
            .WithMany(e => e.Attachments)
            .HasForeignKey(a => a.EvidenceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.UploadedBy)
            .WithMany()
            .HasForeignKey(a => a.UploadedById)
            .OnDelete(DeleteBehavior.Restrict);

        // ActionLog configuration
        modelBuilder.Entity<ActionLog>()
            .HasKey(al => al.Id);

        modelBuilder.Entity<ActionLog>()
            .HasOne(al => al.Action)
            .WithMany(a => a.Logs)
            .HasForeignKey(al => al.ActionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ActionLog>()
            .HasOne(al => al.CreatedBy)
            .WithMany(u => u.ActionLogs)
            .HasForeignKey(al => al.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        modelBuilder.Entity<ActionLog>()
            .HasIndex(al => al.CreatedAt);

        modelBuilder.Entity<Action>()
            .HasIndex(a => a.Status);

        modelBuilder.Entity<Action>()
            .HasIndex(a => a.DueDate);
    }
}
