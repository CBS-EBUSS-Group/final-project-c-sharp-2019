using System;
namespace final_project
{
  public class Client
  {

    private int Id;
    private string Name { get; set; }
    private readonly DateTime DOB;
    private string CaseType { get; set; }
    private string Street { get; set; }
    private int Zip { get; set; }
    private string City { get; set; }

    public Client(string name, DateTime dob, string caseType, string street, int zip, string city)
    {
      Name = name;
      DOB = dob;
      CaseType = caseType;
      Street = street;
      Zip = zip;
      City = city;
    }

    public void SetId(int id)
    {
      Id = id;
    }

    public override string ToString()
    {
      return $"Name: {Name},\n Date of birth: {DOB},\nCasetype on: {CaseType},\nStreet: {Street},\nZIP: {Zip},\nCity: {City}.";
    }
  }
}
