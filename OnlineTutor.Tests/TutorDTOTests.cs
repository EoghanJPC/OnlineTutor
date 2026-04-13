using System;
using System.Collections.Generic;
using System.Text;
using OnlineTutor.DTOs;

namespace OnlineTutor.Tests
{
	public class TutorDTOTests
	{
		[Fact]
		public void CorrectMapping()
		{
			var dto = new TutorDTO
			{
				TutorName = "J Bloggs",
				SubjectName = "DTO Testing",
			};

			Assert.Equal("J Bloggs", dto.TutorName);
			Assert.Equal("DTO Testing", dto.SubjectName);
		}
	}
}
