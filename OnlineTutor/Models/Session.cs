using System.ComponentModel.DataAnnotations;

namespace OnlineTutor.Models
{
	public class Session
	{
		public int SessionId { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name = "Session Start")]
		public DateTime SessionTime { get; set; }

		[Required]
		[Url]
		public string MeetingLink { get; set; } = default!;

		[Required]
		public int TutorId { get; set; }
		public Tutor? Tutor { get; set; }
	}
}
