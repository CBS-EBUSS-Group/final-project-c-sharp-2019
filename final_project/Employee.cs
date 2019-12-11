using System;
using System.Collections.Generic;

namespace final_project
{
    public abstract class Employee
    {
        protected int Id { get; set; } // make private
        protected string FirstName { get; set; } // make private
        protected string LastName { get; set; } // make private
        protected DateTime JoinedOn { get; set; } // make private

        public Employee(int id, string firstName, string lastName, DateTime joinedOn)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JoinedOn = joinedOn;
        }

        public virtual void Greeting() // Virtual method to greet users logging in. Allows access to private property FirstName. Is defined as virtual, in order to allow polymorphism with child classes.
        {
            Console.WriteLine($"\nWelcome {FirstName}, what do you want to do today?");
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {FirstName} {LastName}\nJoined on: {JoinedOn.ToShortDateString()}";
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

        public virtual void ListMyCases(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");
        }

        public virtual void ListMyAppointments(DbManager db)
        {
            Console.WriteLine("User does not have access to this function.");
        }
    }
}
