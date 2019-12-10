using System;
namespace final_project
{
    public class Case
    {
        private enum CaseTypes { General, Corporate, Family, Criminal}

        private int Id;
        private int ClientId;
        private string ClientName { get; set; }
        private CaseTypes TypeOfCase { get; set; }
        private DateTime StartDate { get; set; }
        private string TotalCharges { get; set; }

        public Case(string clientName, int caseType, DateTime startDate, string totalCharges)
        {
            ClientName = clientName;
            TypeOfCase = (CaseTypes)caseType;
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
            return $"Client Name: {ClientName},\nCase type: {TypeOfCase},\nStartdate: {StartDate.ToShortDateString()},\nTotal charges: {TotalCharges}\n";
        }
    }
}
