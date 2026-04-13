using OnlineTutor.DTOs;
using OnlineTutor.Models;

namespace OnlineTutor.Tests
{
	public class SessionDTOTests
	{
		[Fact]
		public void FormattedTimeCorrect()
		{
			var dto = new SessionDTO
			{
				FormattedTime = new DateTime(2026, 4, 30, 14, 0, 0).ToString("f")
			};

			Assert.Contains("30 April 2026", dto.FormattedTime);
			Assert.Contains("14:00", dto.FormattedTime);
		}

		[Fact]
		public void UpcomingSession_True()
		{
			var futureDate = DateTime.Now.AddDays(1).ToString("f");

			var dto = new SessionDTO { FormattedTime = futureDate };

			Assert.True(dto.UpcomingSession);
		}

		[Fact]
		public void CorrectMapping()
		{
			var subject = new Subject { SubjectName = "DTO Testing" };
			var tutor = new Tutor { TutorName = "J Bloggs" };
			var session = new Session
			{
				SessionTime = new DateTime(2026, 4, 30, 14, 0, 0),
				Tutor = tutor,
			};

			var dto = new SessionDTO
			{
				TutorName = session.Tutor.TutorName,
				SubjectName = session.Tutor.Subject.SubjectName,
				FormattedTime = session.SessionTime.ToString("f")
			};

			Assert.Equal("J Bloggs", dto.TutorName);
			Assert.Equal("DTO Testing", dto.SubjectName);
			Assert.Contains("30 April 2026", dto.FormattedTime);
		}
	}
}
