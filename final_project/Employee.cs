using System;

namespace final_project
{
    public abstract class Employee
    {
        protected int Id { get; set; }
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected DateTime JoinedOn { get; set; }

        public Employee(int id, string firstName, string lastName, DateTime joinedOn)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JoinedOn = joinedOn;
        }

        // Method to greet users logging in. Allows access to private property FirstName.
        public void Greeting()
        {
            Console.WriteLine($"\nWelcome {FirstName}, what do you want to do today?");
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {FirstName} {LastName}\nJoined on: {JoinedOn.ToShortDateString()}";
        }


        // All methods below are implemented on an employee level to allow for polymorphic behaviour


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
