using HabibaARR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HabibaARR.Data;

public class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Check if database already initialized
        if (context.Users.Any())
            return;

        // Get role manager from DI - we'll need to do this through a service
        // For now, we'll manually create roles via direct DB access
        var rolesExist = context.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM AspNetRoles") > 0;

        if (!rolesExist)
        {
            CreateRoles(context);
            CreateDefaultUsers(context);
        }
    }

    private static void CreateRoles(ApplicationDbContext context)
    {
        var roles = new[]
        {
            new IdentityRole { Id = "1", Name = "ADMIN", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "2", Name = "DIRECTEUR", NormalizedName = "DIRECTEUR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "3", Name = "GESTIONNAIRE", NormalizedName = "GESTIONNAIRE", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Id = "4", Name = "RESPONSABLE", NormalizedName = "RESPONSABLE", ConcurrencyStamp = Guid.NewGuid().ToString() }
        };

        context.Roles.AddRange(roles);
        context.SaveChanges();
    }

    private static void CreateDefaultUsers(ApplicationDbContext context)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var users = new[]
        {
            new ApplicationUser
            {
                Id = "admin-001",
                UserName = "admin@novec.fr",
                Email = "admin@novec.fr",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "NOVEC",
                Position = "Administrateur",
                Department = "Direction Informatique",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                NormalizedEmail = "ADMIN@NOVEC.FR",
                NormalizedUserName = "ADMIN@NOVEC.FR"
            },
            new ApplicationUser
            {
                Id = "directeur-001",
                UserName = "directeur@novec.fr",
                Email = "directeur@novec.fr",
                EmailConfirmed = true,
                FirstName = "Jean",
                LastName = "DIRECTEUR",
                Position = "Directeur Général",
                Department = "Direction Générale",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                NormalizedEmail = "DIRECTEUR@NOVEC.FR",
                NormalizedUserName = "DIRECTEUR@NOVEC.FR"
            },
            new ApplicationUser
            {
                Id = "gestionnaire-001",
                UserName = "gestionnaire@novec.fr",
                Email = "gestionnaire@novec.fr",
                EmailConfirmed = true,
                FirstName = "Marie",
                LastName = "GESTIONNAIRE",
                Position = "Chef de Projet",
                Department = "Direction de Projet",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                NormalizedEmail = "GESTIONNAIRE@NOVEC.FR",
                NormalizedUserName = "GESTIONNAIRE@NOVEC.FR"
            },
            new ApplicationUser
            {
                Id = "responsable-001",
                UserName = "responsable@novec.fr",
                Email = "responsable@novec.fr",
                EmailConfirmed = true,
                FirstName = "Pierre",
                LastName = "RESPONSABLE",
                Position = "Responsable Opérationnel",
                Department = "Opérations",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                NormalizedEmail = "RESPONSABLE@NOVEC.FR",
                NormalizedUserName = "RESPONSABLE@NOVEC.FR"
            }
        };

        foreach (var user in users)
        {
            user.PasswordHash = hasher.HashPassword(user, "Test@12345");
            context.Users.Add(user);
        }
        context.SaveChanges();

        // Assign roles
        var roleAssignments = new[]
        {
            new IdentityUserRole<string> { UserId = "admin-001", RoleId = "1" },
            new IdentityUserRole<string> { UserId = "directeur-001", RoleId = "2" },
            new IdentityUserRole<string> { UserId = "gestionnaire-001", RoleId = "3" },
            new IdentityUserRole<string> { UserId = "responsable-001", RoleId = "4" }
        };

        context.UserRoles.AddRange(roleAssignments);
        context.SaveChanges();
    }
}
