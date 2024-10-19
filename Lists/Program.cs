using Lists.Utilities;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

Database.Password = File.ReadAllText("Password");

var DefaultDatabase = Database.GetDefaultInstance();

DefaultDatabase.Lock();

if (!DefaultDatabase.Load("DefaultDatabase"))
{
	app.Logger.LogWarning("Could not load the default database file");
}

DefaultDatabase.Unlock();

bool SaveThreadRunning = false;

Thread SaveThread = new Thread(() =>
{
	while (SaveThreadRunning)
	{
		DefaultDatabase.Lock();
		if (!DefaultDatabase.Save("DefaultDatabase"))
		{
			app.Logger.LogError("Could not save the database!");
		}
        DefaultDatabase.Unlock();
		Thread.Sleep(10000);
    }
});

SaveThreadRunning = true;

SaveThread.Start();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute("default", "", new { controller = "Home", action = "Home" });
app.MapControllerRoute("CategoriesIndex", "Categories", new { controller = "Categories", action = "Index" });
app.MapControllerRoute("CategoriesNew", "Categories/New", new { controller = "Categories", action = "New" });
app.MapControllerRoute("CategoriesDelete", "Categories/Delete/{Name}", new { controller = "Categories", action = "Delete" });
app.MapControllerRoute("CategoriesEdit", "Categories/Edit/{Name}", new { controller = "Categories", action = "Edit" });
app.MapControllerRoute("ItemsIndex", "Items/{CategoryName}", new { controller = "Items", action = "Index" });
app.MapControllerRoute("ItemsNew", "Items/New/{CategoryName}", new { controller = "Items", action = "New" });
app.MapControllerRoute("ItemsDelete", "Items/Delete/{CategoryName}/{Name}", new { controller = "Items", action = "Delete" });
app.MapControllerRoute("ItemsEdit", "Items/Edit/{CategoryName}/{Name}", new { controller = "Items", action = "Edit" });
app.MapControllerRoute("SubitemsIndex", "Subitems/{ItemName}/{ItemCategoryName}", new { controller = "Subitems", action = "Index" });
app.MapControllerRoute("SubitemsNew", "Subitems/New/{ItemName}/{ItemCategoryName}", new { controller = "Subitems", action = "New" });
app.MapControllerRoute("SubitemsDelete", "Subitems/Delete/{ItemName}/{ItemCategoryName}/{Name}", new { controller = "Subitems", action = "Delete" });
app.MapControllerRoute("SubitemsEdit", "Subitems/Edit/{ItemName}/{ItemCategoryName}/{Name}", new { controller = "Subitems", action = "Edit" });

app.Run();

SaveThreadRunning = false;

SaveThread.Join();

DefaultDatabase.Lock();

if (!DefaultDatabase.Save("DefaultDatabase"))
{
	app.Logger.LogError("Could not save the database!");
}

DefaultDatabase.Unlock();
