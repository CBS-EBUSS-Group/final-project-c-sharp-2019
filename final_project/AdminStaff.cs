using System;


namespace final_project
{
    
    public class AdminStaff : Employee
    {
        public enum AdminFunction { Accounting, HumanResources, OfficeManagement }
        public AdminFunction Role { get; set; }

        public AdminStaff(int Id, string FirstName, string LastName, DateTime JoinedOn, int role = 1) : base(Id, FirstName, LastName, JoinedOn)
        {
            Role = (AdminFunction)role;
        }

        public override string ToString()
        {
            return $"Administrative Staff's Employee Id: {Id}, \nFirst Name: {FirstName}, \nLast Name: {LastName}, \nJoined on: {JoinedOn}, \nAdministrative Role: {Role}.\n";
        }

    }
}
