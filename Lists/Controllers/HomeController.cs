using Microsoft.AspNetCore.Mvc;
using Lists.Utilities;
using Lists.Models;



namespace Lists.Controllers
{

	public class HomeController : Controller
	{

		Database DefaultDatabase = Database.GetDefaultInstance();

		public HomeController()
		{

		}

		[HttpGet]
		public IActionResult Home()
		{
			if (TempData.ContainsKey("Message"))
			{
				ViewBag.Message = TempData["Message"];
			}

			return View();
		}

	}

}
