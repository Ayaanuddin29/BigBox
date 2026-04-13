ComplaintManagement (.NET 8) - Minimal scaffold
=============================================

Projects
- ComplaintManagement.API  -> ASP.NET Core Web API (Identity + JWT)
- ComplaintManagement.UI   -> ASP.NET Core MVC UI (calls API)

Quick start (Windows)
1. Ensure .NET 8 SDK is installed.
2. Open solution in Visual Studio 2022/2023 or VS Code.
3. In API project (ComplaintManagement.API) update appsettings.json connection string if needed.
4. From API project folder, run:
   dotnet ef migrations add InitIdentity
   dotnet ef database update
   (You may need to install dotnet-ef tool: dotnet tool install --global dotnet-ef)

5. Run API project (set as startup). It will seed an admin user:
   username: admin
   password: Admin@123

6. Update UI appsettings.json ApiBaseUrl to the API HTTPS URL (by default https://localhost:7071)
7. Run UI project and navigate to /Account/Login

Notes:
- This is a minimal scaffold. Add additional endpoints, error handling, and production hardening as needed.
