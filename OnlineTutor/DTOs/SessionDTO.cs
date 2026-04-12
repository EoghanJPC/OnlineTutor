namespace OnlineTutor.DTOs
{
	public class SessionDTO
	{
		public int SessionId { get; set; }

		public string FormattedTime { get; set; } = default!;
		public string MeetingLink { get; set; } = default!;

		public string TutorName { get; set; } = default!;
		public string SubjectName { get; set; } = default!;

		public bool UpcomingSession => DateTime.Parse(FormattedTime) > DateTime.Now;
	}
}
