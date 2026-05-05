using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTutor.Data;
using OnlineTutor.DTOs;
using OnlineTutor.Models;
using System.Net.Http;
using OnlineTutor.Services;

namespace OnlineTutor.Controllers
{
    [Authorize]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PdfService _pdfService = new PdfService();

        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sessions
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
			using (var client = new HttpClient())
			{
				try
				{
					string url = "https://zenquotes.io/api/random/" + DateTime.Now.Ticks;
					var response = await client.GetFromJsonAsync<List<QuoteResponse>>(url);

					if (response != null && response.Count > 0)
					{
						ViewBag.Quote = response[0].q;
						ViewBag.Author = response[0].a;
					}
				}
				catch
				{
					ViewBag.Quote = "The beautiful thing about learning is that no one can take it away from you.";
					ViewBag.Author = "B.B. King";
				}
			}

			var sessions = await _context.Sessions
				.Include(s => s.Tutor)
					.ThenInclude(t => t.Subject)
				.ToListAsync();

			var viewModel = sessions.Select(s => new SessionDTO
			{
				SessionId = s.SessionId,
				FormattedTime = s.SessionTime.ToString("f"),
				MeetingLink = s.MeetingLink,
				TutorName = s.Tutor?.TutorName ?? "Unknown Tutor",
				SubjectName = s.Tutor?.Subject?.SubjectName ?? "N/A",
                StudyNotes = s.StudyNotes
			}).ToList();

			return View(viewModel);
		}

		public class QuoteResponse
		{
			public string q { get; set; }
			public string a { get; set; }

		}

		// GET: Sessions/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Tutor)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["TutorId"] = new SelectList(_context.Tutors, "TutorId", "TutorName");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([Bind("SessionId,SessionTime,MeetingLink,TutorId,StudyNotes")] Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorId"] = new SelectList(_context.Tutors, "TutorId", "TutorName", session.TutorId);
            return View(session);
        }

		// GET: Sessions/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["TutorId"] = new SelectList(_context.Tutors, "TutorId", "TutorName", session.TutorId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [Bind("SessionId,SessionTime,MeetingLink,TutorId,StudyNotes")] Session session)
        {
            if (id != session.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.SessionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorId"] = new SelectList(_context.Tutors, "TutorId", "TutorName", session.TutorId);
            return View(session);
        }

		// GET: Sessions/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Tutor)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.SessionId == id);
        }

        [Authorize]
        public async Task<IActionResult> DownloadNotes(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.Tutor)
                    .ThenInclude(t => t.Subject)
                .FirstOrDefaultAsync(s => s.SessionId == id);


            if (session == null || string.IsNullOrEmpty(session.StudyNotes)) return NotFound();

            var subjectName = session.Tutor?.Subject?.SubjectName ?? "Session";
            var pdf = _pdfService.GenerateSessionNotes(session.StudyNotes, session.SessionTime, subjectName);

            return File(pdf, "application/pdf", $"{subjectName} - StudyNotes.pdf");
        }
    }
}
