namespace Lists.Models
{

	public class Category
	{

		public string? Name { get; set; }
		public string? Description { get; set; }

		public String? GetKey()
		{
			if (Name == null)
			{
				return null;
			}

			return Name;
		}

		public bool IsValid()
		{
			return Name != null && Description != null;
		}

	}

}
