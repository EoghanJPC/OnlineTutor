using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;

namespace OnlineTutor.Controllers
{
	public class PaymentController : Controller
	{
		public PaymentController()
		{
			Stripe.StripeConfiguration.ApiKey = "sk_test_51TQnARLuScwtNoH6ZugxWc8Nwpu13sZ3gWVbiWqzgzRnCfsKaL2q8gPob89KqRBoAGWQHBM7ZCt6gUOZFZYK2hqc00Z4DgadXj";
		}

		public IActionResult Index() => View();

		[HttpPost]
		public IActionResult CreateDonation(long amount, string tutor)
		{
			var domain = "https://localhost:7004"; 

			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = amount * 100,
                            Currency = "eur",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = $"Donation to {tutor}",
								Description = "For excellent teaching and patience"
							},
						},
						Quantity = 1,
					},
				},
				Mode = "payment",
				SuccessUrl = domain + "/Payment/Success",
				CancelUrl = domain + "/Payment/Cancel",
			};

			var service = new SessionService();
			Session session = service.Create(options);

			return Redirect(session.Url);
		}

		public IActionResult Success() => View();
		public IActionResult Cancel() => View();
	}
}