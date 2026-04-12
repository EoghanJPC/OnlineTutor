using System.ComponentModel.DataAnnotations;

namespace OnlineTutor.Models
{
	public class Tutor
	{
		public int TutorId { get; set; }
		
		[Required]
		public string TutorName { get; set; }

		[Required]
		public int SubjectId { get; set; }
		public string SubjectName { get; set; }

		public ICollection<Session> Sessions { get; set; }
	}
}
