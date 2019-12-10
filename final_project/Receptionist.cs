using System;
using System.Collections.Generic;
using System.Globalization;

namespace final_project
{
    public class Receptionist : Employee
    {
        public Receptionist(int Id, string FirstName, string LastName, DateTime JoinedOn) : base(Id, FirstName, LastName, JoinedOn) {}

        public override void AddNewClient(DbManager db)
        {
            Console.WriteLine("You have chosen to add a new client.");

            Console.WriteLine("Client name?");
            string name = Console.ReadLine();
            Console.WriteLine("Date of birth yyyy-MM-dd:");
            DateTime bday = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
            Console.WriteLine("Case type:\n1.Corporate\n2.Family\n3.Criminal\nType in a number.");
            int caseType = int.Parse(Console.ReadLine());
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

            Console.WriteLine("Choose one of the available lawyers for this profession by entering the id:\n");
            foreach (Lawyer lawyer in db.GetLawyersByClientCaseType(clientName))
            {
                Console.WriteLine(lawyer.ToString());
                Console.WriteLine();
            }
            int lawyerId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter date and time of the appointment in a valid format yyyy-MM-dd hh:mm");
            DateTime date = DateTime.ParseExact($"{Console.ReadLine()}:00", "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);

            Console.WriteLine("Type in a number to choose a Meetingroom:\n1.Aquarium\n2.Cube\n3.Cave");
            int meetingRoom = int.Parse(Console.ReadLine());

            db.SetAppointment(clientName, lawyerId, date, meetingRoom);
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
