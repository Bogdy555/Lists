namespace Lists.Models
{

	public class Item
	{

		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? CategoryName { get; set; }

		public Tuple<String, String>? GetKey()
		{
			if (Name == null || CategoryName == null)
			{
				return null;
			}

			return new Tuple<String, String>(Name, CategoryName);
		}

		public bool IsValid()
		{
			return Name != null && Description != null && CategoryName != null;
		}

		public String? GetCategoryForeignKey()
		{
			if (CategoryName == null)
			{
				return null;
			}

			return CategoryName;
		}

		public void SetCategoryForeignKey(String? key)
		{
			if (key == null)
			{
				CategoryName = null;
			}

			CategoryName = key;
		}

	}

}
