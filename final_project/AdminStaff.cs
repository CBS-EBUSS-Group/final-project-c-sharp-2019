using System;
using System.Collections.Generic;

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

        public override void ListAllCases(DbManager db)
        {
            Console.WriteLine("You have chosen to list all cases.\n");
            List<string> listOfCases = db.GetAllCases();

            foreach (string entry in listOfCases)
            {
                Console.WriteLine(entry);
                
            }
            Console.WriteLine();
        }

        public override void ListAllAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointments.\n");
            List<string> listOfAppointments = db.GetAllAppointments();

            foreach (string entry in listOfAppointments)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine();
        }


    }
}
