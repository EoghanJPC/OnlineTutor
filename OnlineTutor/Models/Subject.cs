using System.ComponentModel.DataAnnotations;

namespace OnlineTutor.Models
{
	public class Subject
	{
		public int SubjectId { get; set; }

		[Required]
		public string SubjectName { get; set; } = default!;

		public string SubjectDesc { get; set; } = default!;

		public ICollection<Tutor> Tutors { get; set; } = new List<Tutor>();
	}
}
