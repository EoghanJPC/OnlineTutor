namespace OnlineTutor.Models
{
	public class Session
	{
		public int SessionId { get; set; }

		public DateTime SessionTime { get; set; }

		public int TutorId { get; set; }

		public Tutor Tutor { get; set; }
	}
}
