using System;
namespace final_project
{
  public class Client
  {
    public enum TypeOfCase { Corporate, Family, Criminal}

    private int Id;
    private string Name { get; set; }
    private readonly DateTime DOB;
    private TypeOfCase CaseType { get; set; }
    private string Street { get; set; }
    private string Zip { get; set; }
    private string City { get; set; }

    public Client(string name, DateTime dob, int caseType, string street, string zip, string city)
    {
      Name = name;
      DOB = dob;
      CaseType = (TypeOfCase)caseType;
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
      return $"Name: {Name},\nDate of birth: {DOB.ToShortDateString()},\nCasetype: {CaseType},\nStreet: {Street},\nZIP: {Zip},\nCity: {City}.";
    }
  }
}
