using System;
using System.Collections.Generic;


namespace final_project
{
    public class Lawyer : Employee
    {
        public enum Rank { Junior, Senior }
        public enum Field { Corporate, FamilyCase, CriminalCase }

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

            Console.WriteLine("Client Name?");
            string clientName = Console.ReadLine();
            Console.WriteLine("Casetype:");
            string caseType = Console.ReadLine();
            Console.WriteLine("Date yyyy-MM-dd:");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
            Console.WriteLine("Total Charges:");
            string totalCharges = Console.ReadLine();

            db.SetCase(Id, clientName, caseType, date, totalCharges);

            Console.WriteLine("You have successfully added a new case!");

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


        public override void ListAllCases(DbManager db)
        {
            Console.WriteLine("You have chosen to list all cases.\n");
            List<string> listOfCases = db.GetAllCases();

            foreach (string entry in listOfCases)
            {
                Console.WriteLine(entry);
            }
        }

    }
}
