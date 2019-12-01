using System; 


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

        public override string ToString()
        {
            return $"Employee Id: {Id}, \nFirst Name: {FirstName}, \nLast Name {LastName}, \nJoined on: {JoinedOn}. \n";
        }

    }
}
