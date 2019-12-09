using System;
using System.Collections.Generic;

namespace final_project
{
    public abstract class Employee
    {
        protected int Id { get; set; }
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected DateTime JoinedOn { get; set; }

        public Employee() { } // remove later: for testing

        public Employee(int id, string firstName, string lastName, DateTime joinedOn)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JoinedOn = joinedOn;
        }

        public virtual void Greeting() // Virtual method to greet users logging in. Allows access to private property FirstName. Is defined as virtual, in order to allow polymorphism with child classes.
        {
            Console.WriteLine($"Welcome {FirstName}, what do you want to do today?");
        }

        public virtual void AddNewClient(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");
        }

        public virtual void AddNewAppointment(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");

        }

        public virtual void AddNewCase(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");

        }

        public virtual void ListAllAppointments(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");
        }

        public virtual void ListDailyAppointments(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");
        }

        public virtual void ListAllClients(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");
        }

        public virtual void ListAllCases(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");
        }
    }
}
