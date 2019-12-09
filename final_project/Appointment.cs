using System;
namespace final_project
{
    public class Appointment
    {
        private int Id;
        private int ClientId;
        private int LawyerId;
        private string ClientName { get; set; }
        private string LawyerName { get; set; }
        private DateTime DateTime { get; set; }
        private string MeetingRoom { get; set; }
        


        public Appointment(string clientName, string lawyerName, DateTime dateTime, string meetingRoom)
        {
            ClientName = clientName;
            LawyerName = lawyerName;
            DateTime = dateTime;
            MeetingRoom = meetingRoom;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetClientId(int clientId)
        {
            ClientId = clientId;
        }

        public void SetLawyertId(int lawyerId)
        {
            LawyerId = lawyerId;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Client name: {ClientName}, Lawyer name: {LawyerName}, Datetime {DateTime.ToLongTimeString()}, MeetingRoom: {MeetingRoom}";
        }
    }
}
