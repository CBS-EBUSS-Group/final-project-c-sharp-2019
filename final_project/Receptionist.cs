using System;
using System.Collections.Generic;
using System.Globalization;

namespace final_project
{
    public class Receptionist : Employee
    {
        public Receptionist(int Id, string FirstName, string LastName, DateTime JoinedOn) : base(Id, FirstName, LastName, JoinedOn) {}

        // takes user input and calls DbManager to create a new client in the database
        public override void AddNewClient(DbManager db)
        {
            Console.WriteLine("You have chosen to add a new client.");

            Console.WriteLine("Client name?");
            string name = Console.ReadLine();

            Console.WriteLine("Date of birth yyyy-MM-dd:");
            DateTime bday;

            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out bday))
            {
                Console.WriteLine("You have entered a wrong date format. Please try again.");
            }

            Console.WriteLine("Case type:\n1.Corporate\n2.Family\n3.Criminal\nType in a number.");
            int caseType;

            while (!int.TryParse(Console.ReadLine(), out caseType) || caseType < 1 || caseType > 3)
            {
                Console.WriteLine("Please choose 1, 2 or 3.");
            }

            Console.WriteLine("Street:");
            string street = Console.ReadLine();

            Console.WriteLine("ZIP");
            string zip = Console.ReadLine();

            Console.WriteLine("City:");
            string city = Console.ReadLine();

            db.SetClient(name, bday, caseType, street, zip, city);

            Console.WriteLine("You have successfully added a new client!");
        }

        // takes user input and calls DbManager to create a new appointment in the database
        public override void AddNewAppointment(DbManager db)
        {
            Console.WriteLine("You have chosen to add a new appointment.");

            List<int> lawyerIds = new List<int>();
            string clientName = "";
            int tryCount = 0;

            while (lawyerIds.Count == 0)
            {
                if (tryCount > 0)
                    Console.WriteLine("No such client found in database. Please try again.");

                Console.WriteLine("Enter client Name:");
                clientName = Console.ReadLine();

                foreach (Lawyer lawyer in db.GetLawyersByClientCaseType(clientName))
                {
                    lawyerIds.Add(lawyer.GetId());
                    Console.WriteLine(lawyer.ToString());
                    Console.WriteLine();
                }

                tryCount++;
            }

            Console.WriteLine("Choose one of the available lawyers for this profession by entering the ID:\n");
            int lawyerId;

            while (!int.TryParse(Console.ReadLine(), out lawyerId) || !lawyerIds.Contains(lawyerId))
            {
                Console.WriteLine("You have entered a wrong ID. Please choose one of the provided IDs:");
            }

            Console.WriteLine("Enter date and time of the appointment in a valid format yyyy-MM-dd HH:mm");
            DateTime date;

            while (!DateTime.TryParseExact($"{Console.ReadLine()}:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) || date < DateTime.Now)
            {
                Console.WriteLine("You have entered a wrong date format or invalid date (in the past). Please try again.");
            }

            Console.WriteLine("Type in a number to choose a Meetingroom:\n1.Aquarium\n2.Cube\n3.Cave");
            int meetingRoom;

            while (!int.TryParse(Console.ReadLine(), out meetingRoom) || meetingRoom < 1 || meetingRoom > 3)
            {
                Console.WriteLine("Please choose 1, 2 or 3.");
            }

            db.SetAppointment(clientName, lawyerId, date, meetingRoom);
        }

        // prints all appointments fetched from the database
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

        // prints all appointments for a given date fetched from the database
        public override void ListDailyAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointsments of a specific date.\n");
            Console.WriteLine("Date yyyy-MM-dd:");
            DateTime date;

            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.WriteLine("You have entered a wrong date format. Please try again.");
            }

            List<Appointment> listOfDailies = db.GetDailyAppointments(date);

            if (listOfDailies.Count == 0)
            {
                Console.WriteLine("\nNo appointments found for the date you entered.");
                return;
            }

            foreach (Appointment appointment in listOfDailies)
            {
                Console.WriteLine(appointment.ToString());
            }
            Console.WriteLine();
        }

        // prints all clients fetched from the database
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
