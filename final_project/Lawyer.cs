using System;
using System.Collections.Generic;


namespace final_project
{
    public class Lawyer : Employee
    {
        public enum Rank { Junior, Senior}
        public enum Field { Undeclared, Corporate, FamilyCase, CriminalCase }

        private readonly DateTime dateOfBirth;
        public Rank Seniority  { get; set; }
        public Field Specialization  { get; set; }
        private List<int> ClientList = new List<int>();

        public Lawyer() { } // for testing: remove later

        // Design decision: Constructor sets a standard value of Junior Rank for Seniority and Undeclared for Specialization, if no value given
        public Lawyer(int Id, string FirstName, string LastName, DateTime JoinedOn, DateTime birthdate, int seniority = 1, int specialization = 1) : base(Id, FirstName, LastName, JoinedOn)
        {
            dateOfBirth = birthdate;
            Seniority = (Rank)seniority;
            Specialization = (Field)specialization;
        }

        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
        }

        public override string ToString()
        {
            return $"Lawyer's Employee Id: {Id}, \nFirst Name: {FirstName}, \nLast Name: {LastName}, \nJoined on: {JoinedOn}, \nBirthdate: {DateOfBirth}, \nSeniority: {Seniority}, \nLegal Specialization {Specialization}.\n";
        }

        public void AddCase(int clientId)
        {
            //Search for Cases in Database with clientId

            //Print the Cases already in the Database

            //Ask if they want to add a new one? Y/N

            //If yes, let them give arguments to a list, create case with constructor

            Console.WriteLine("Successfully added Case: {}");
        }
    }
}
