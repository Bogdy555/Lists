using Microsoft.AspNetCore.Mvc;
using Lists.Utilities;
using Lists.Models;



namespace Lists.Controllers
{

	public class ItemsController : Controller
	{

		Database DefaultDatabase = Database.GetDefaultInstance();

		public ItemsController()
		{

		}

		[HttpPost]
		public IActionResult Index(String Password, String CategoryName)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			ViewBag.Password = Password;
			ViewBag.CategoryName = CategoryName;
			DefaultDatabase.Lock();
			ViewBag.Items = DefaultDatabase.GetItems((item) => { return item.CategoryName == CategoryName; });
			DefaultDatabase.Unlock();

			return View();
		}

		[HttpPost]
		public IActionResult New(String Password, String CategoryName, Item item)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			DefaultDatabase.Lock();
			if (!DefaultDatabase.AddItem(item))
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to save!";

				ViewBag.Password = Password;
				ViewBag.CategoryName = CategoryName;
				DefaultDatabase.Lock();
				ViewBag.Items = DefaultDatabase.GetItems((item) => { return item.CategoryName == CategoryName; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			DefaultDatabase.Unlock();

			ViewBag.Message = "Saved!";

			ViewBag.Password = Password;
			ViewBag.CategoryName = CategoryName;
			DefaultDatabase.Lock();
			ViewBag.Items = DefaultDatabase.GetItems((item) => { return item.CategoryName == CategoryName; });
			DefaultDatabase.Unlock();

			return View("Index");
		}

		[HttpPost]
		public IActionResult Delete(String Password, String CategoryName, String Name)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			DefaultDatabase.Lock();
			List<Item> items = DefaultDatabase.GetItems((item) => { return item.Name == Name && item.CategoryName == CategoryName; });
			if (items.Count != 1)
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to delete!";

				ViewBag.Password = Password;
				ViewBag.CategoryName = CategoryName;
				DefaultDatabase.Lock();
				ViewBag.Items = DefaultDatabase.GetItems((item) => { return item.CategoryName == CategoryName; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			DefaultDatabase.DeleteItem(items[0]);
			DefaultDatabase.Unlock();

			ViewBag.Message = "Deleted!";

			ViewBag.Password = Password;
			ViewBag.CategoryName = CategoryName;
			DefaultDatabase.Lock();
			ViewBag.Items = DefaultDatabase.GetItems((item) => { return item.CategoryName == CategoryName; });
			DefaultDatabase.Unlock();

			return View("Index");
		}

	}

}
