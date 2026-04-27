using Microsoft.AspNetCore.Mvc;

namespace OnlineTutor.Controllers
{
	public class PaymentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
