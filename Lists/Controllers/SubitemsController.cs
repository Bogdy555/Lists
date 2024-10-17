using Microsoft.AspNetCore.Mvc;
using Lists.Utilities;
using Lists.Models;



namespace Lists.Controllers
{

	public class SubitemsController : Controller
	{

		Database DefaultDatabase = Database.GetDefaultInstance();

		public SubitemsController()
		{

		}

		[HttpPost]
		public IActionResult Index(String Password, String ItemName, String ItemCategoryName)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			ViewBag.Password = Password;
			ViewBag.ItemName = ItemName;
			ViewBag.ItemCategoryName = ItemCategoryName;
			DefaultDatabase.Lock();
			ViewBag.Subitems = DefaultDatabase.GetSubitems((subitem) => { return subitem.ItemName == ItemName && subitem.ItemCategoryName == ItemCategoryName; });
			DefaultDatabase.Unlock();

			return View();
		}

		[HttpPost]
		public IActionResult New(String Password, String ItemName, String ItemCategoryName, Subitem subitem)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			DefaultDatabase.Lock();
			if (!DefaultDatabase.AddSubitem(subitem))
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to saved!";

				ViewBag.Password = Password;
				ViewBag.ItemName = ItemName;
				ViewBag.ItemCategoryName = ItemCategoryName;
				DefaultDatabase.Lock();
				ViewBag.Subitems = DefaultDatabase.GetSubitems((subitem) => { return subitem.ItemName == ItemName && subitem.ItemCategoryName == ItemCategoryName; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			DefaultDatabase.Unlock();

			ViewBag.Message = "Saved!";

			ViewBag.Password = Password;
			ViewBag.ItemName = ItemName;
			ViewBag.ItemCategoryName = ItemCategoryName;
			DefaultDatabase.Lock();
			ViewBag.Subitems = DefaultDatabase.GetSubitems((subitem) => { return subitem.ItemName == ItemName && subitem.ItemCategoryName == ItemCategoryName; });
			DefaultDatabase.Unlock();

			return View("Index");
		}

		[HttpPost]
		public IActionResult Delete(String Password, String ItemName, String ItemCategoryName, String Name)
		{
			if (Password != Database.Password)
			{
				TempData.Add("Message", "Wrong password!");

				return Redirect("/");
			}

			DefaultDatabase.Lock();
			List<Subitem> subitems = DefaultDatabase.GetSubitems((subitem) => { return subitem.Name == Name && subitem.ItemName == ItemName && subitem.ItemCategoryName == ItemCategoryName; });
			if (subitems.Count != 1)
			{
				DefaultDatabase.Unlock();

				ViewBag.Message = "Failed to delete!";

				ViewBag.Password = Password;
				ViewBag.ItemName = ItemName;
				ViewBag.ItemCategoryName = ItemCategoryName;
				DefaultDatabase.Lock();
				ViewBag.Subitems = DefaultDatabase.GetSubitems((subitem) => { return subitem.ItemName == ItemName && subitem.ItemCategoryName == ItemCategoryName; });
				DefaultDatabase.Unlock();

				return View("Index");
			}
			DefaultDatabase.DeleteSubitem(subitems[0]);
			DefaultDatabase.Unlock();

			ViewBag.Message = "Deleted!";

			ViewBag.Password = Password;
			ViewBag.ItemName = ItemName;
			ViewBag.ItemCategoryName = ItemCategoryName;
			DefaultDatabase.Lock();
			ViewBag.Subitems = DefaultDatabase.GetSubitems((subitem) => { return subitem.ItemName == ItemName && subitem.ItemCategoryName == ItemCategoryName; });
			DefaultDatabase.Unlock();

			return View("Index");
		}

	}

}
