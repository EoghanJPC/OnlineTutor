using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineTutor.Models;

namespace OnlineTutor.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
	{
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Tutor> Tutors { get; set; }
		public DbSet<Session> Sessions { get; set; }
	}
}
