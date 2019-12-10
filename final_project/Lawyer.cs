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

        // Design decision: Constructor sets a standard value of Junior Rank for Seniority and Undeclared for Specialization, if no value given
        public Lawyer(int Id, string FirstName, string LastName, DateTime JoinedOn, DateTime dateOfBirth, int seniority = 1, int specialization = 1) : base(Id, FirstName, LastName, JoinedOn)
        {
            DateOfBirth = dateOfBirth;
            Seniority = (Rank)seniority;
            Specialization = (Field)specialization;
        }

        public override void AddNewCase(DbManager db)
        {
            Console.WriteLine("You have chosen to register a new case");

            Console.WriteLine("Please type in the client's name");
            string clientName = Console.ReadLine();

            Console.WriteLine("Casetype:\n1.Corporate\n2.Family\n3.Criminal\nType in a number.");
            int caseType = int.Parse(Console.ReadLine());

            Console.WriteLine("Date yyyy-MM-dd:");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Console.WriteLine("Total Charges:");
            string totalCharges = Console.ReadLine();

            db.SetCase(Id, clientName, caseType, date, totalCharges);

            Console.WriteLine("You have successfully added a new case!");

        }

        public override void ListMyAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list your appointments.\n");

            List<Appointment> appointmentList = db.GetMyAppointments(Id);

            foreach (Appointment appointment in appointmentList)
            {
                Console.WriteLine(appointment.ToString());
            }
            Console.WriteLine();

        }

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
    }
}
