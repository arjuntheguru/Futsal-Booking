# Futsal-Booking

Booking System
 
Technical Overview
 
- Developed using ASP.NET Core 2.2
- Makes use of Entity Framework Core
- Uses HTML,Javascript and CSS
 
This project has a multi tier architecture.
It implements Repository Design Pattern.
*Repository Pattern has been implemented on top of Entity Framework Core*
 
- goalza.booking.core is the BLL (Buisness Logic Layer) of the application.
- goalza.booking.core consists of all the Domain classes and DTO's (Data Transfer Objects).
- Igoalza.booking.core also defines Interfaces for the Repository Pattern .
 
- goalza.booking.infrastructure is the DAL (Data Access Layer) of the application.
- goalza.booking.infrastructure consists of implementation for the interfaces.
- goalza.booking.infrastructure consists of model configration and DbContext files.
 
- goalza.booking.web is the Application Layer and Presentation Layer
 
There exists two DbContexts
-BookingContext (For Domain Classes)
-ApplicationDbContext (For ASP.NET Core Identity)
 
This application is developed using the code first approach.
There is no batch file for creating the database.
 
- Select goalza.booking.web as StartUp Project
 
Steps to create the Database
 
- Open Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console)
- Select goalza.booking.web as Default Project
- Run the following commands
 
  Add-Migration (give the migration a name) -Context BookingContext -OutputDir Data/Migrations/Domain
  update-database -Context BookingContext
  Add-Migration (give the migration a name) -Context ApplicationDbContext -OutputDir Data/Migrations
  update-database -Context ApplicationDbContext 
 
** Contact ARJUN SUBEDI for further queries **
