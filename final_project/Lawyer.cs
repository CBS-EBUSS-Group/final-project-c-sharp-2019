using System;
using System.Collections.Generic;
using System.Globalization;

namespace final_project
{
    public class Lawyer : Employee
    {
        public enum Rank { Junior, Senior }
        public enum Field { General, Corporate, Family, Criminal }

        private readonly DateTime DateOfBirth;
        public Rank Seniority { get; set; }
        public Field Specialization { get; set; }

        // Design decision: Constructor sets a standard value of Junior Rank for Seniority and General for Specialization, if no value given
        public Lawyer(int Id, string FirstName, string LastName, DateTime JoinedOn, DateTime dateOfBirth, int seniority = 0, int specialization = 0) : base(Id, FirstName, LastName, JoinedOn)
        {
            DateOfBirth = dateOfBirth;
            Seniority = (Rank)seniority;
            Specialization = (Field)specialization;
        }

        // takes user input and calls DbManager to create a new case in the database
        public override void AddNewCase(DbManager db)
        {
            Console.WriteLine("\nYou have chosen to register a new case.");

            Console.WriteLine("\nPlease type in the client's name");
            string clientName = Console.ReadLine();

            int caseType = (int)Specialization;

            Console.WriteLine("Please enter a date in the format yyyy-MM-dd:");
            DateTime date;

            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.WriteLine("\nYou have entered a wrong date format. Please try again.");
            }

            Console.WriteLine("Total Charges:");
            string totalCharges = Console.ReadLine();

            db.SetCase(Id, clientName, caseType, date, totalCharges);

            Console.WriteLine("\nYou have successfully added a new case!");

        }

        // prints all appointments for the given lawyer id fetched from the database
        public override void ListMyAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list your appointments.\n");

            List<Appointment> appointmentList = db.GetAppointmentsByLawyerId(Id);

            foreach (Appointment appointment in appointmentList)
            {
                Console.WriteLine(appointment.ToString());
            }
            Console.WriteLine();

        }

        // prints all cases for the given lawyer id fetched from the database
        public override void ListMyCases(DbManager db)
        {
            Console.WriteLine("You have chosen to list your cases.\n");
            List<Case> caseList = db.GetMyCases(Id);

            foreach (Case @case in caseList)
            {
                Console.WriteLine(@case.ToString());
            }
            Console.WriteLine();
        }

        public int GetId()
        {
            return Id;
        }
    }
}
