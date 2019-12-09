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
            Console.WriteLine("Date of birth_");
            DateTime bday = DateTime.Parse(Console.ReadLine());
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
            string name = Console.ReadLine();
            Console.WriteLine("Date of birth:");
            DateTime bday = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Casetype:");
            string caseType = Console.ReadLine();
            Console.WriteLine("Street:");
            string street = Console.ReadLine();
            Console.WriteLine("ZIP");
            string zip = Console.ReadLine();
            Console.WriteLine("City:");
            string city = Console.ReadLine();

            //db.SetAppointment(lol, name, bday, caseType, street, zip, city);

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

        }

        public override void ListDailyAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointsments of a specific date.\n");
            Console.WriteLine("Date:");
            DateTime date = DateTime.Parse(Console.ReadLine());
            List<string> listOfDailies = db.GetDailyAppointments(date);

            foreach (string entry in listOfDailies)
            {
                Console.WriteLine(entry);
            }
        }

        public override void ListAllClients(DbManager db)
        {
            Console.WriteLine("You have chosen to list all clients.\n");
            List<string> listOfClients = db.GetAllClients();

            foreach (string entry in listOfClients)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
