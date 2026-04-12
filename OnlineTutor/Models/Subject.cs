namespace OnlineTutor.Models
{
	public class Subject
	{
		public int SubjectId { get; set; }

		public string SubjectName { get; set; }

		public string SubjectDesc { get; set; }

		public ICollection<Tutor> Tutors { get; set; }
	}
}
