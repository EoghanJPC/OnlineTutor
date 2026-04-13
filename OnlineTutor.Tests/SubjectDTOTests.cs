using System;
using System.Collections.Generic;
using System.Text;
using OnlineTutor.DTOs;

namespace OnlineTutor.Tests
{
	public class SubjectDTOTests
	{
		[Fact]
		public void CorrectMapping()
		{
			var dto = new SubjectDTO
			{
				SubjectName = "DTO Testing",
				Description = "The Incredible Testing of Impecable DTOs",
			};

			Assert.Equal("DTO Testing", dto.SubjectName);
			Assert.Equal("The Incredible Testing of Impecable DTOs", dto.Description);
		}
	}
}
