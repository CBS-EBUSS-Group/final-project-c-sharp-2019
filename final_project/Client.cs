using System;
namespace final_project
{
    public class Client
    {

        protected int Id { get; set; }
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected DateTime DOB { get; set; }
        protected string CaseType { get; set; }
        protected string Street { get; set; }
        protected int Zip { get; set; }
        protected string City { get; set; }

        public Client(int id, string firstName, string lastName, DateTime dob, string caseType, string street, int zip, string city)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DOB = dob;
            CaseType = caseType;
            Street = street;
            Zip = zip;
            City = city;
        }
    }
}
