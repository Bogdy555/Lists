using Lists.Models;
using System.Text.Json;



namespace Lists.Utilities
{

	public class Database
	{

		public static String? Password = null;
		private static Database DefaultInstance = new Database();

		private Mutex Locker = new();
		private List<Category> Categories = new();
		private List<Item> Items = new();
		private List<Subitem> Subitems = new();

		public Database()
		{

		}

		public static Database GetDefaultInstance()
		{
			return DefaultInstance;
		}

		public void Lock()
		{
			Locker.WaitOne();
		}

		public void Unlock()
		{
			Locker.ReleaseMutex();
		}

		public bool AddCategory(Category category)
		{
			if (!category.IsValid())
			{
				return false;
			}

			foreach (Category originalCategory in Categories)
			{
				object? checkKey = originalCategory.GetKey();

				if (checkKey != null && checkKey.Equals(category.GetKey()))
				{
					return false;
				}
			}

			try
			{
				Categories.Add(category);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public bool AddItem(Item item)
		{
			if (!item.IsValid())
			{
				return false;
			}

			bool foundCategory = false;

			foreach (Category category in Categories)
			{
				object? checkKey = category.GetKey();

				if (checkKey != null && checkKey.Equals(item.GetCategoryForeignKey()))
				{
					foundCategory = true;
					break;
				}
			}

			if (!foundCategory)
			{
				return false;
			}

			foreach (Item originalItem in Items)
			{
				object? checkKey = originalItem.GetKey();

				if (checkKey != null && checkKey.Equals(item.GetKey()))
				{
					return false;
				}
			}

			try
			{
				Items.Add(item);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public bool AddSubitem(Subitem subitem)
		{
			if (!subitem.IsValid())
			{
				return false;
			}

			bool foundItem = false;

			foreach (Item item in Items)
			{
				object? checkKey = item.GetKey();

				if (checkKey != null && checkKey.Equals(subitem.GetItemForeignKey()))
				{
					foundItem = true;
					break;
				}
			}

			if (!foundItem)
			{
				return false;
			}

			foreach (Subitem originalSubitem in Subitems)
			{
				object? checkKey = originalSubitem.GetKey();

				if (checkKey != null && checkKey.Equals(subitem.GetKey()))
				{
					return false;
				}
			}

			try
			{
				Subitems.Add(subitem);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public List<Category> GetCategories(Func<Category, bool> filter)
		{
			List<Category> result = new();

			foreach (Category category in Categories)
			{
				if (filter(category))
				{
					result.Add(category);
				}
			}

			return result;
		}

		public List<Item> GetItems(Func<Item, bool> filter)
		{
			List<Item> result = new();

			foreach (Item item in Items)
			{
				if (filter(item))
				{
					result.Add(item);
				}
			}

			return result;
		}

		public List<Subitem> GetSubitems(Func<Subitem, bool> filter)
		{
			List<Subitem> result = new();

			foreach (Subitem subitem in Subitems)
			{
				if (filter(subitem))
				{
					result.Add(subitem);
				}
			}

			return result;
		}

		public void DeleteCategory(Category category)
		{
			if (!Categories.Contains(category))
			{
				return;
			}

			for (int index = 0; index < Items.Count; index++)
			{
				Item item = Items[index];

				object? checkKey = item.GetCategoryForeignKey();

				if (checkKey != null && checkKey.Equals(category.GetKey()))
				{
					DeleteItem(item);
					index--;
				}
			}

			Categories.Remove(category);
		}

		public void DeleteItem(Item item)
		{
			if (!Items.Contains(item))
			{
				return;
			}

			for (int index = 0; index < Subitems.Count; index++)
			{
				Subitem subitem = Subitems[index];

				object? checkKey = subitem.GetItemForeignKey();

				if (checkKey != null && checkKey.Equals(item.GetKey()))
				{
					DeleteSubitem(subitem);
					index--;
				}
			}

			Items.Remove(item);
		}

		public void DeleteSubitem(Subitem subitem)
		{
			if (!Subitems.Contains(subitem))
			{
				return;
			}

			Subitems.Remove(subitem);
		}

		public bool UpdateCategory(Category originalCategory, Category newCategory)
		{
			if (!newCategory.IsValid())
			{
				return false;
			}

			if (originalCategory.GetKey() != newCategory.GetKey())
			{
				foreach (Category category in Categories)
				{
					object? checkKey = category.GetKey();

					if (checkKey != null && checkKey.Equals(newCategory.GetKey()))
					{
						return false;
					}
				}
			}

			var originalCategoryKey = originalCategory.GetKey();

			originalCategory.Name = newCategory.Name;
			originalCategory.Description = newCategory.Description;

			if (originalCategory.GetKey() != originalCategoryKey)
			{
				foreach (Item originalItem in Items)
				{
					object? checkKey = originalItem.GetCategoryForeignKey();

					if (checkKey != null && checkKey.Equals(originalCategoryKey))
					{
						var newItem = new Item
						{
							Name = originalItem.Name,
							Description = originalItem.Description,
							CategoryName = originalItem.Name
						};

						newItem.SetCategoryForeignKey(originalCategory.GetKey());

						UpdateItem(originalItem, newItem);
					}
				}
			}

			return true;
		}

		public bool UpdateItem(Item originalItem, Item newItem)
		{
			if (!newItem.IsValid())
			{
				return false;
			}

			bool foundCategory = false;

			foreach (Category category in Categories)
			{
				object? checkKey = category.GetKey();

				if (checkKey != null && checkKey.Equals(newItem.GetCategoryForeignKey()))
				{
					foundCategory = true;
					break;
				}
			}

			if (!foundCategory)
			{
				return false;
			}

			if (originalItem.GetKey() != newItem.GetKey())
			{
				foreach (Item item in Items)
				{
					object? checkKey = item.GetKey();

					if (checkKey != null && checkKey.Equals(newItem.GetKey()))
					{
						return false;
					}
				}
			}

			var originalItemKey = originalItem.GetKey();

			originalItem.Name = newItem.Name;
			originalItem.Description = newItem.Description;
			originalItem.CategoryName = newItem.CategoryName;

			if (originalItem.GetKey() != originalItemKey)
			{
				foreach (Subitem originalSubitem in Subitems)
				{
					object? checkKey = originalSubitem.GetItemForeignKey();

					if (checkKey != null && checkKey.Equals(originalItemKey))
					{
						var newSubitem = new Subitem
						{
							Name = originalSubitem.Name,
							Description = originalSubitem.Description,
							ItemName = originalSubitem.ItemName,
							ItemCategoryName = originalSubitem.ItemCategoryName
						};

						newSubitem.SetItemForeignKey(originalItem.GetKey());

						UpdateSubitem(originalSubitem, newSubitem);
					}
				}
			}

			return true;
		}

		public bool UpdateSubitem(Subitem originalSubitem, Subitem newSubitem)
		{
			if (!newSubitem.IsValid())
			{
				return false;
			}

			bool foundItem = false;

			foreach (Item item in Items)
			{
				object? checkKey = item.GetKey();

				if (checkKey != null && checkKey.Equals(newSubitem.GetItemForeignKey()))
				{
					foundItem = true;
					break;
				}
			}

			if (!foundItem)
			{
				return false;
			}

			if (originalSubitem.GetKey() != newSubitem.GetKey())
			{
				foreach (Subitem subitem in Subitems)
				{
					object? checkKey = subitem.GetKey();

					if (checkKey != null && checkKey.Equals(newSubitem.GetKey()))
					{
						return false;
					}
				}
			}

			var originalSubitemKey = originalSubitem.GetKey();

			originalSubitem.Name = newSubitem.Name;
			originalSubitem.Description = newSubitem.Description;
			originalSubitem.ItemName = newSubitem.ItemName;
			originalSubitem.ItemCategoryName = newSubitem.ItemCategoryName;

			if (originalSubitem.GetKey() != originalSubitemKey)
			{

			}

			return true;
		}

		public bool Load(String filePath)
		{
			try
			{
				String jsonCategories = File.ReadAllText(filePath + ".categories.json");

				Func<String, List<Category>> FuncCategories = (String json) =>
				{
					List<Category>? obj = JsonSerializer.Deserialize<List<Category>>(json);

					if (obj == null)
					{
						return new List<Category>();
					}

					return obj;
				};

				Categories = FuncCategories(jsonCategories);

				String jsonItems = File.ReadAllText(filePath + ".items.json");

				Func<String, List<Item>> FuncItems = (String json) =>
				{
					List<Item>? obj = JsonSerializer.Deserialize<List<Item>>(json);

					if (obj == null)
					{
						return new List<Item>();
					}

					return obj;
				};

				Items = FuncItems(jsonItems);

				String jsonSubitems = File.ReadAllText(filePath + ".subitems.json");

				Func<String, List<Subitem>> FuncSubitems = (String json) =>
				{
					List<Subitem>? obj = JsonSerializer.Deserialize<List<Subitem>>(json);

					if (obj == null)
					{
						return new List<Subitem>();
					}

					return obj;
				};

				Subitems = FuncSubitems(jsonSubitems);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public bool Save(String filePath)
		{
			try
			{
				var options = new JsonSerializerOptions();

				options.WriteIndented = true;

				String jsonCategories = JsonSerializer.Serialize(Categories, options) + "\r\n";

				File.WriteAllText(filePath + ".categories.json", jsonCategories);

				String jsonItems = JsonSerializer.Serialize(Items, options) + "\r\n";

				File.WriteAllText(filePath + ".items.json", jsonItems);

				String jsonSubitems = JsonSerializer.Serialize(Subitems, options) + "\r\n";

				File.WriteAllText(filePath + ".subitems.json", jsonSubitems);
			}
			catch
			{
				return false;
			}

			return true;
		}

	}

}
