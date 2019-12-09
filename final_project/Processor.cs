using System;

namespace final_project
{
    public class Processor
    {
        private DbManager db;
        private Employee user;

        public Processor()
        {
            db = new DbManager();
            db.Connect();
            db.CreateSeed();
        }

        public void Process()
        {
            Console.WriteLine("Welcome to the LegalX CRM System!");

            LoginProcess();

                if (user is Lawyer)
                    LawyerProgramFlow();
                else if (user is AdminStaff)
                    AdminProgramFlow();
                else if (user is Receptionist)
                    ReceptionistProgramFlow();

            Console.ReadLine(); // Testing
        }

        private void LoginProcess()
        {
            Console.WriteLine("Please login to your LegalX account with your username and password.");

            Console.WriteLine("username:");
            string username = Console.ReadLine();

            Console.WriteLine("password:");
            string password = Console.ReadLine();

            user = db.Logon(username, password);
        }

        private void LawyerProgramFlow()
        {
            // print lawyer menu
            int condition = 0;
            do
            {
                user.Greeting();
                Console.WriteLine("1) Add new case\n2) List all cases\n3) List all appointments");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        user.AddNewCase(db);
                        condition++;
                        break;

                    case "2":
                        user.ListAllCases(db);
                        condition++;
                        break;

                    case "3":
                        user.ListAllAppointments(db);
                        condition++;
                        break;

                    default:
                        Console.WriteLine("Please choose 1, 2 or 3.");
                        break;
                }
            } while (condition < 1);
        }

        private void AdminProgramFlow()
        {
            // print admin menu
            int condition = 0;
            do
            {
                user.Greeting();
                Console.WriteLine("1) List all cases\n2) List all appointments");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        user.ListAllCases(db);
                        condition++;
                        break;

                    case "2":
                        user.ListAllAppointments(db);
                        condition++;
                        break;

                    default:
                        Console.WriteLine("Please choose 1 or 2");
                        break;
                }
            } while (condition < 1);
        }


        private void ReceptionistProgramFlow()
        {
            // print receptionist menu
            int condition = 0;
            do
            {
                user.Greeting();
                Console.WriteLine("1) Register a new client\n2) Add new appointment\n3) List all appointments\n4) List all appointments of a specific date\n5) List all clients");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        user.AddNewClient(db);
                        condition++;
                        break;

                    case "2":
                        user.AddNewAppointment(db);
                        condition++;
                        break;
                    case "3":
                        user.ListAllAppointments(db);
                        condition++;
                        break;

                    case "4":
                        user.ListDailyAppointments(db);
                        condition++;
                        break;

                    case "5":
                        user.ListAllClients(db);
                        condition++;
                        break;

                    default:
                        Console.WriteLine("Please choose 1, 2, 3, 4 or 5.");
                        break;
                }
            } while (condition < 1);
        }



    }
}
