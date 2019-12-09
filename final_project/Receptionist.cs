using System;
using System.Collections.Generic;

namespace final_project
{
    public class Receptionist : Employee
    {

        public Receptionist(int Id, string FirstName, string LastName, DateTime JoinedOn) : base(Id, FirstName, LastName, JoinedOn) {}

        public override void AddNewClient(DbManager db)
        {
            Console.WriteLine("You have chosen to add a new client.");

            Console.WriteLine("Client Name?");
            string name = Console.ReadLine();
            Console.WriteLine("Date of birth yyyy-MM-dd:");
            DateTime bday = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
            Console.WriteLine("Casetype:");
            string caseType = Console.ReadLine();
            Console.WriteLine("Street:");
            string street = Console.ReadLine();
            Console.WriteLine("ZIP");
            string zip = Console.ReadLine();
            Console.WriteLine("City:");
            string city = Console.ReadLine();

            db.SetClient(name, bday, caseType, street, zip, city);

            Console.WriteLine("You have successfully added a new client!");
        }

        public override void AddNewAppointment(DbManager db)
        {
            Console.WriteLine("You have chosen to add a new appointment.");

            Console.WriteLine("Client Name:");
            string clientName = Console.ReadLine();
            Console.WriteLine("Lastname of lawyer:");
            string lawyerLastName = Console.ReadLine();
            Console.WriteLine("Date yyyy/MM/dd:");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
            Console.WriteLine("Meetingroom");
            string meetingRoom = Console.ReadLine();

            db.SetAppointment(clientName, lawyerLastName, date, meetingRoom);

            Console.WriteLine("You have successfully added a new client!");
        }

        public override void ListAllAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointments.\n");
            List<string> listOfAppointments = db.GetAllAppointments();

            foreach (string entry in listOfAppointments)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine();
        }

        public override void ListDailyAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointsments of a specific date.\n");
            Console.WriteLine("Date yyyy-MM-dd:");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
            List<string> listOfDailies = db.GetDailyAppointments(date);

            foreach (string entry in listOfDailies)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine();
        }

        public override void ListAllClients(DbManager db)
        {
            Console.WriteLine("You have chosen to list all clients.\n");
            List<string> listOfClients = db.GetAllClients();

            foreach (string entry in listOfClients)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine();
        }
    }
}
