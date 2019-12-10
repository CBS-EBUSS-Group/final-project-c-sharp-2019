using System;

namespace final_project
{
  public class Client
  {
    public enum TypeOfCase { General, Corporate, Family, Criminal}

    private int Id;
    private string Name { get; set; }
    private readonly DateTime BirthDate;
    private TypeOfCase CaseType { get; set; }
    private string Street { get; set; }
    private string Zip { get; set; }
    private string City { get; set; }

    public Client(string name, DateTime bday, int caseType, string street, string zip, string city)
    {
      Name = name;
      BirthDate = bday;
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
      return $"Name: {Name}\nDate of birth: {BirthDate.ToShortDateString()}\nCase type: {CaseType}\nStreet: {Street}\nZIP: {Zip}\nCity: {City}\n";
    }
  }
}
