using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OnlineTutor.Models;

namespace OnlineTutor.Tests
{
	public class ModelTesting
	{
		[Fact]
		public void TutorMissingName()
		{
			var tutor = new Tutor { TutorName = "" };
			var valContext = new ValidationContext(tutor);
			var results = new List<ValidationResult>();

			var validationCheck = Validator.TryValidateObject(tutor, valContext, results, true);

			Assert.False(validationCheck);
		}

		[Fact]
		public void SessionMissingLink()
		{
			var session = new Session { MeetingLink = "" };
			var valContext = new ValidationContext(session);
			var results = new List<ValidationResult>();

			var validationCheck = Validator.TryValidateObject(session, valContext, results, true);

			Assert.False(validationCheck);
		}
	}
}
