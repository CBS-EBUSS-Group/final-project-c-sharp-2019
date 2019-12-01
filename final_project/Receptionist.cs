using System;


namespace final_project
{
    public class Receptionist : Employee
    {

        public Receptionist(int Id, string FirstName, string LastName, DateTime JoinedOn) : base(Id, FirstName, LastName, JoinedOn) {}

        public override string ToString()
        {
            return $"Administrative Staff's Employee Id: {Id}, \nFirst Name: {FirstName}, \nLast Name: {LastName}, \nJoined on: {JoinedOn}.\n";
        }

        public void RegisterClient() { }

        public void AddAppointment() { }

    }
}
