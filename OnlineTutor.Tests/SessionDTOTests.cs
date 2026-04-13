using OnlineTutor.DTOs;

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
	}
}
