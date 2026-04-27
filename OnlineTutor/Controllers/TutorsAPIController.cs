using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTutor.Data;

namespace OnlineTutor.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TutorsAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public TutorsAPIController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task <IActionResult> GetTutors()
		{
			var tutors = await _context.Tutors
				.Include(t => t.Subject)
				.Select(t => new
				{
					t.TutorId,
					t.TutorName,
					Subject = t.Subject != null ? t.Subject.SubjectName : "No Subject",
					SessionCount = t.Sessions.Count()
				})
				.ToListAsync();

			return Ok(tutors);
		}
	}
}
