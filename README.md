# BlazorPWA

## Table of contents
* [Motivation](#motivation)
* [General information](#general-information)
* [Technologies](#technologies)
* [Setup](#setup)
* [Sample views](#Sample-views)

## Motivation
This project was created as a study task for the subject "Programming of desktop and mobile applications".  
My main goal was to get to know Blazor and understand the concept of Progressive Web Applications.

## General information
The business field of the project was a website for managing employees, projects and teams.  
The website offers the following features:

- Employee and project management
- Creating new technologies and teams
- Installation as a progressive application
- Offline operation

The deployed application can be seen at [https://blazorpwasite.azurewebsites.net/](https://blazorpwasite.azurewebsites.net/)

## Technologies

- .NET 5
- C# 9
- Blazor
- EF Core 5
- MSSQL

### Used libraries and packages

- DnetIndexedDb [(Github repository)](https://github.com/amuste/DnetIndexedDb)
- Syncfusion [(Project url)](https://www.syncfusion.com/blazor-components?utm_source=nuget&utm_medium=listing)
- Swagger 

## Setup
To set up a website, follow these steps:

1. Clone repository from github
2. Build solution
3. Setup database by providing connectionString to your database and updating it with migrations  

Example configuration
```javascript
"ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS1;Database=BlazorPWA;Trusted_Connection=True;"
  },
```
4. In .NET Core CLI run  
  `dotnet ef database update`  
  or in Package Manager Console run  
  `Update-Database`
5. Start program

## Sample views
<p align="center">
    <b>Home page in the browser</b><br>
    <img src="/images/home-page.png">
    <b>Home page in the desktop application</b><br>
    <img src="/images/installed-app-desktop.png">
    <b>Example page in the mobile application</b><br>
    <img src="/images/Example-page-mobile.png" width="480"><br>
    <b>Employee page</b><br>
    <img src="/images/employees-page.png">
    <b>Technologies page</b><br>
    <img src="/images/technologies-page.png">
    <b>Projects page</b><br>
    <img src="/images/projects-page.png">
</p>
