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
            List<Case> caseList = db.GetAllCases();

            foreach (Case @case in caseList)
            {
                Console.WriteLine(@case.ToString());
            }
            Console.WriteLine();
        }

        public override void ListAllAppointments(DbManager db)
        {
            Console.WriteLine("You have chosen to list all appointments.\n");
            
            List<Appointment> appointmentList = db.GetAllAppointments();

            foreach (Appointment appointment in appointmentList)
            {
                Console.WriteLine(appointment.ToString());
            }
            Console.WriteLine();

        }

    }
}
