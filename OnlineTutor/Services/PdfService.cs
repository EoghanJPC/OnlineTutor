using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OnlineTutor.Services
{
	public class PdfService
	{
		public byte[] GenerateSessionNotes(string notes, DateTime sessionTime, string subject)
		{
			QuestPDF.Settings.License = LicenseType.Community;

			return Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Margin(60);
					page.Header().Text($"{subject} - Session Notes").FontSize(25).SemiBold();

					page.Content().PaddingVertical(20).Column(col =>
					{
						col.Item().Text($"Session Date: {sessionTime:f}").Italic();
						col.Item().PaddingTop(10).LineHorizontal(1);
						col.Item().PaddingTop(20).Text(notes).FontSize(12);
					});

					page.Footer().AlignCenter().Text(x =>
					{
						x.Span("From OnlineTutor - ");
						x.CurrentPageNumber();
					});
				});
			}).GeneratePdf();
		}
	}
}
