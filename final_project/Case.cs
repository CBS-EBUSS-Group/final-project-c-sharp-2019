using System;
namespace final_project
{
    public class Case
    {
        public enum CaseType { Corporate, Family, Criminal}

        private int Id;
        private int ClientId;
        private string ClientName { get; set; }
        public CaseType TypeOfCase { get; set; }
        private DateTime StartDate { get; set; }
        private int TotalCharges { get; set; }

        //default casetype
        public Case(string clientName, int caseType, DateTime startDate, int totalCharges)
        {
            ClientName = clientName;
            TypeOfCase = (CaseType)caseType;
            StartDate = startDate;
            TotalCharges = totalCharges;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetClientId(int clientId)
        {
            ClientId = clientId;
        }

        public override string ToString()
        {
            return $"Case ID: {Id},\nClient Name: {ClientName},\nCaseType: {TypeOfCase},\nStartdate: {StartDate},\nTotal charges: {TotalCharges}.";
        }
    }
}
