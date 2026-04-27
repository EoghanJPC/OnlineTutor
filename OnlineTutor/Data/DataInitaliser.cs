using Microsoft.AspNetCore.Identity;

namespace OnlineTutor.Data
{
	public class DataInitaliser
	{
		public static async Task SeedRoles(IServiceProvider serviceProvider) {
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

			string[] roles = { "Admin", "Registered" };
			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role)) {
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}

			var adminEmail = "admin@OnlineTutor.com";
			var adminUser = await userManager.FindByNameAsync(adminEmail);

			if (adminUser == null)
			{
				var newAdmin = new IdentityUser
				{
					UserName = adminEmail,
					Email = adminEmail,
					EmailConfirmed = true
				};

				await userManager.CreateAsync(newAdmin, "Str0ngPassw0rd!");
				await userManager.AddToRoleAsync(newAdmin, "Admin");
			}
		}
	}
}
