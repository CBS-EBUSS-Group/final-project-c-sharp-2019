using System;
using System.Collections.Generic;
using System.Globalization;

namespace final_project
{
    public class Receptionist : Employee
    {
        public Receptionist() { } // remove later for testing
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
            Console.WriteLine("Lawyer Name:");
            string lawyerLastName = Console.ReadLine();
            Console.WriteLine("Give a valid date and time yyyy-MM-dd hh:mm");

            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);

            Console.WriteLine("Meetingroom");
            string meetingRoom = Console.ReadLine();

            db.SetAppointment(clientName, lawyerLastName, date, meetingRoom);
        }

        public override void ListAllAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointments.\n");
            List<Appointment> listOfAppointments = db.GetAllAppointments();

            foreach (Appointment appointment in listOfAppointments)
            {
                Console.WriteLine(appointment.ToString());
            }
            Console.WriteLine();
        }

        public override void ListDailyAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointsments of a specific date.\n");
            Console.WriteLine("Date yyyy-MM-dd:");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            /*
            int[] dateArr = Array.ConvertAll(Console.ReadLine().Split('-'), int.Parse);

            DateTime date = new DateTime(dateArr[0], dateArr[1], dateArr[2]);
            */
            List<Appointment> listOfDailies = db.GetDailyAppointments(date);

            foreach (Appointment appointment in listOfDailies)
            {
                Console.WriteLine(appointment.ToString());
            }
            Console.WriteLine();
        }

        public override void ListAllClients(DbManager db)
        {
            Console.WriteLine("You have chosen to list all clients.\n");
            List<Client> clientList = db.GetAllClients();

            foreach (Client client in clientList)
            {
                Console.WriteLine(client.ToString());
            }
            Console.WriteLine();
        }
    }
}
