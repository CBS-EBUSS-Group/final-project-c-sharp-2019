using System;
namespace final_project
{
    public class Case
    {
        public enum CaseType { General, Corporate, Family, Criminal}

        private int Id;
        private int ClientId;
        private string ClientName { get; set; }
        public CaseType TypeOfCase { get; set; }
        private DateTime StartDate { get; set; }
        private string TotalCharges { get; set; }

        //default casetype
        public Case(string clientName, int caseType, DateTime startDate, string totalCharges)
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
            return $"Client Name: {ClientName},\nCase type: {TypeOfCase},\nStartdate: {StartDate.ToShortDateString()},\nTotal charges: {TotalCharges}\n";
        }
    }
}
