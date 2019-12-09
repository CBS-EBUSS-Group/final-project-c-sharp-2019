using System;
namespace final_project
{
    public class Case
    {
        public enum CaseType { Corporate, Family, Criminal}

        protected int Id { get; set; }
        protected int ClientId { get; set; }
        public CaseType TypeOfCase { get; set; }
        protected DateTime StartDate { get; set; }
        protected int TotalCharges { get; set; }

        public Case(int id, int clientId, int typeOfCase, DateTime startDate, int totalCharges)
        {
            Id = id;
            ClientId = clientId;
            TypeOfCase = (CaseType)typeOfCase;
            StartDate = startDate;
            TotalCharges = totalCharges;
        }
    }
}
