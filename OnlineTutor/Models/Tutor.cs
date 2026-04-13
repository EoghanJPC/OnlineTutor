using System.ComponentModel.DataAnnotations;

namespace OnlineTutor.Models
{
	public class Tutor
	{
		public int TutorId { get; set; }

		[Required(ErrorMessage = "Tutor Name Required")]
		[StringLength(50, MinimumLength = 1)]
		public string TutorName { get; set; } = default!;

		[Required]
		public int SubjectId { get; set; }
		public Subject? Subject { get; set; } 

		public ICollection<Session> Sessions { get; set; } = new List<Session>();
	}
}
