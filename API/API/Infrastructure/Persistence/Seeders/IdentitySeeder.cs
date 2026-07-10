using Authorization.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Entities;

namespace Infrastructure.Persistence.Seeders
{
    public class IdentitySeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ApplicationDbContext _context;
        public IdentitySeeder(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task SeedAsync()
        {
            var adminRoleName = "ADMIN";


            Console.WriteLine("============>>>>>>>>>>>>USER CREATED"); 


            // ========================
            // ROLE
            // ========================
            var role = await _roleManager.FindByNameAsync(adminRoleName);

            if (role == null)
            {
                role = new IdentityRole<Guid>
                {
                    Name = adminRoleName,
                    NormalizedName = adminRoleName.ToUpper()
                };

                await _roleManager.CreateAsync(role);
            }

            role = await _roleManager.FindByNameAsync(adminRoleName);

            var adminUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Nom == "Admin");

            if (adminUser == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    Matricule = "admin",
                    Nom = "Admin",
                    Type = "DEFAULT",
                    Prenom = "User",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser, "Admin@123");

                Console.WriteLine("USER CREATED");
                Console.WriteLine($"RESULT: {result.Succeeded}");


                if (!result.Succeeded)
                {
                    throw new Exception("User creation failed: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                adminUser = await _userManager.FindByNameAsync("admin");


                Console.WriteLine("USER CREATED");
                Console.WriteLine($"RESULT: {result.Succeeded}");

            }

            var roles = await _userManager.GetRolesAsync(adminUser);

            if (!roles.Contains(adminRoleName))
            {
                await _userManager.AddToRoleAsync(adminUser, adminRoleName);
            }

            if (role != null)
            {
                await AssignAllPermissionsToRole(role);
            }
        }

        private async Task AssignAllPermissionsToRole(IdentityRole<Guid> role)
        {
            var permissions = await _context.Set<Permission>()
                .Select(p => p.Code)
                .ToListAsync();

            var existingClaims = await _roleManager.GetClaimsAsync(role);

            var existingValues = existingClaims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value)
                .ToHashSet();

            foreach (var permission in permissions)
            {
                if (!existingValues.Contains(permission))
                {
                    await _roleManager.AddClaimAsync(role,
                        new Claim("permission", permission));
                }
            }
        }
    }
}
