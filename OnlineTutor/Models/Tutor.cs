namespace OnlineTutor.Models
{
	public class Tutor
	{
		public int TutorId { get; set; }

		public string TutorName { get; set; }

		public int SubjectId { get; set; }

		public string SubjectName { get; set; }

		public ICollection<Session> Sessions { get; set; }
	}
}
