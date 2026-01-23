enter to this path : dekhkon.tj\src\backend
command for migrations: dotnet ef migrations add "name" -p .\Infrastructure\ -s .\MainApp\ --output-dir DataAccess/Migrations
command for database update : dotnet ef database update -p .\Infrastructure\ -s .\MainApp\ 