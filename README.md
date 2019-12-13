# CBS Course Introduction to Programming and Application Design (C#) - Final Project

**This project was created by students of [Copenhagen Business School](https://www.cbs.dk/en) (CBS) in the course of the Master's degree in Business Administration and E-business:**

## General requirements and application specs

The task was to build a console application in C# applying object-oriented programming principles.

**Specs:**

A company named LegalX provides Corporate and Private legal services to its clients. The company has following resources:

- 3 Senior Lawyers => (Id, Firstname, Lastname, DOB, Seniority, Specialization, JoinedOn)
- 9 Junior Lawyers => (Id, Firstname, Lastname, DOB, Seniority, Specialization, JoinedOn)
- 3 Administrative staff members => (Id, Firstname, Lastname, JoinedOn, Role)
- 1 Receptionist (Id, Firstname, Lastname, JoinedOn)

The company resides in the heart of Gotham City. The company building has 3 meeting rooms that are named **Aquarium**, **Cube** and **Cave**. These rooms are used for client meetings. Aquarium has capacity for 20 persons. Cube and Cave can accommodate 10 and 8 persons respectively. Lawyers have their domain of expertise which can be **Corporate specialist**, **FamilyCase** specialist and **CriminalCase** specialist.

Company has clients from different areas in Gotham city and clients are assigned to a lawyer for an appointment and later for creating a case eventually after meeting.

- Users (Lawyer, Admin, Receptionist) should be able to access application only by username and password.
- Receptionist should be able to perform following tasks
    - Login
    - Register a new client
    - Add a new appointment
    - List all appointments
    - List all appointments for the day
    - List all clients
- Lawyer should be able to perform following tasks
    - Login
    - List all cases
    - Add a new case
    - List all appointments
- Admin staff should be able to perform following tasks:
    - Login
    - List all cases
    - List all appointments

## Solution

We decided to use a simple SQlite database as data storage for our application. When the initial setup runs, seed data is read in from a text file and written to the database.
We created three different, user-dependent workflows and an Employee class hierarchy to allow for polymorphism. All console operations are conducted in the Employee's child classes, while database communications happen in the DbManager class.

Enjoy the program :)

## Instructions

Please follow the listed steps to set up the console application correctly on your PC:

1. Open the final_project.sln
2. Navigate to the ENV.cs file in the solution manager
3. Open the ENV.cs file. There will be two properties of the C# class that you need to change (indicated below):
    1. SQLite URI - Here you will need to add your systems path to a directory where you would like to save the SQLite database, which will be created on first program start.
    2. SeedData - Here you enter the local root path of the application + /final_project/seed/seed.txt
4. Open NuGet package manager and download/install Mono.Data.SQLite, which is used to access the SQLite database in this application
5. If you are on windows, you might have to naviage to your project settings (top of screen Project > properties) and change the build settings to run the program with x64 bit architecture.
6. Build the Solution and Run it. The application should compile and build a database for you based on the seed.

```csharp
    public static class ENV
    {
        // please enter a file directory for the sqlite database
        private static readonly string SQLiteURI = "Data Source=< enter a file path for your database here >";

        // please enter the local root path of the application + /final_project/seed/seed.txt
        private static readonly string SeedData = "< enter the root directory of the application here >/final_project/seed/seed.txt";

        // ...
    }
```

Created by:
[Maxwell Burda](https://github.com/MCBurda), [Dorian Nguyen](https://github.com/orgs/CBS-EBUSS-Group/people/doriannguyen), [Dominik Gadesmann](https://github.com/domgdsman)

Uploaded: 13-12-2019
