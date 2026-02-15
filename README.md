Look Closely - ASP.NET Core Project

   Project Overview

"Look Closely" is a web-based hidden object game developed as a project for the ASP.NET Fundamentals course at SoftUni. The application allows users to explore various levels, find hidden objects, and compete for the highest scores.

 --  Key Features

•	Level Management: Full CRUD operations (Create, Read, Update, Delete) for game levels.
•	Leaderboard: A global ranking system displaying the top 10 players based on their performance.
•	User Profiles: Personalized profile pages featuring user bios and score history.
•	Security & Roles: Role-based access control, ensuring administrative functions are restricted to "Admin" users.

 --  Technologies
•	ASP.NET Core 8.0 (MVC Architecture)
•	Entity Framework Core
•	MS SQL Server
•	ASP.NET Core Identity (Authentication & Authorization)
•	Bootstrap 5 (Responsive UI Design)

--   Setup and Installation

1.	Clone the repository:
Bash
git clone https://github.com/hi6tnikyt/LookClosely_Original_2026
Database Configuration:
	Update appsettings.json or use User Secrets to provide your connection string:
	 
 --   JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=LookCloselyOriginal;Trusted_Connection=True;MultipleActiveResultSets=true"
}

2.	Apply Migrations: Run the following command in the Package Manager Console:
PowerShell
Update-Database
Or using .NET CLI:
Bash
dotnet ef database update
3.	Run the Project:
	Press F5 in Visual Studio or run dotnet run in the terminal.
	 
--   Author
[Dimitar]



