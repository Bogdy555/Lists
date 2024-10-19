using Microsoft.AspNetCore.Mvc;
using Lists.Utilities;
using Lists.Models;



namespace Lists.Controllers
{

	public class CategoriesController : Controller
	{

		Database DefaultDatabase = Database.GetDefaultInstance();

		public CategoriesController()
		{

		}

		[HttpPost]
		public IActionResult Index([FromForm] String Password)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			ViewBag.Password = Password;
			DefaultDatabase.Lock();
			ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
			DefaultDatabase.Unlock();

			return View();
		}

		[HttpPost]
		public IActionResult New([FromForm] String Password, [FromForm] Category category)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			DefaultDatabase.Lock();
			if (!DefaultDatabase.AddCategory(category))
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to save!";

				ViewBag.Password = Password;
				DefaultDatabase.Lock();
				ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			DefaultDatabase.Unlock();

			ViewBag.Message = "Saved!";

			ViewBag.Password = Password;
			DefaultDatabase.Lock();
			ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
			DefaultDatabase.Unlock();

			return View("Index");
		}

		[HttpPost]
		public IActionResult Delete([FromForm] String Password, String Name)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			DefaultDatabase.Lock();
			List<Category> categories = DefaultDatabase.GetCategories((category) => { return category.Name == Name; });
			if (categories.Count != 1)
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to delete!";

				ViewBag.Password = Password;
				DefaultDatabase.Lock();
				ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			DefaultDatabase.DeleteCategory(categories[0]);
			DefaultDatabase.Unlock();

			ViewBag.Message = "Deleted!";

			ViewBag.Password = Password;
			DefaultDatabase.Lock();
			ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
			DefaultDatabase.Unlock();

			return View("Index");
		}

		[HttpPost]
		public IActionResult Edit([FromForm] String Password, String Name, [FromForm] Category newCategory)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			DefaultDatabase.Lock();
			List<Category> categories = DefaultDatabase.GetCategories((category) => { return category.Name == Name; });
			if (categories.Count != 1)
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to update!";

				ViewBag.Password = Password;
				DefaultDatabase.Lock();
				ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			if (!DefaultDatabase.UpdateCategory(categories[0], newCategory))
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to update!";

				ViewBag.Password = Password;
				DefaultDatabase.Lock();
				ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			DefaultDatabase.Unlock();

			ViewBag.Message = "Updated!";

			ViewBag.Password = Password;
			DefaultDatabase.Lock();
			ViewBag.Categories = DefaultDatabase.GetCategories((category) => { return true; });
			DefaultDatabase.Unlock();

			return View("Index");
		}

	}

}
