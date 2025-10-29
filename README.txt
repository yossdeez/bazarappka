Steps to set up the app:

In Program.cs add your server link for database on line 11 - options.UseSqlServer("Server=[InsertYourServerLinkHere];Database=BazarDB;Trusted_Connection=True;TrustServerCertificate=True;");.

Then proceed to open Package Manager Console and type in these commands:
add-migration Initial
update-database

This version includes a seeding method, which automatically adds 20 rows in database for testing the app. If you want to disable this feature go into appsettings.json and change "SeedMode": "Prefilled" on line 9 to "SeedMode": "Clean"
