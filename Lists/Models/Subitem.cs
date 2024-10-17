namespace Lists.Models
{

	public class Subitem
	{

		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? ItemName { get; set; }
		public string? ItemCategoryName { get; set; }

		public Tuple<String, Tuple<String, String>>? GetKey()
		{
			if (Name == null || ItemName == null || ItemCategoryName == null)
			{
				return null;
			}

			return new Tuple<String, Tuple<String, String>>(Name, new Tuple<String, String>(ItemName, ItemCategoryName));
		}

		public bool IsValid()
		{
			return Name != null && Description != null && ItemName != null && ItemCategoryName != null;
		}

		public Tuple<String, String>? GetItemForeignKey()
		{
			if (ItemName == null || ItemCategoryName == null)
			{
				return null;
			}

			return new Tuple<String, String>(ItemName, ItemCategoryName);
		}

		public void SetItemForeignKey(Tuple<String, String>? key)
		{
			if (key == null)
			{
				ItemName = null;
				ItemCategoryName = null;

				return;
			}

			ItemName = key.Item1;
			ItemCategoryName = key.Item2;
		}

	}

}
