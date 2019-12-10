using System;

namespace final_project
{
    public class Appointment
    {
        private enum RoomNames { Undefined, Aquarium, Cube, Cave }

        private int Id;
        private int ClientId;
        private int LawyerId;
        private string ClientName { get; set; }
        private string LawyerName { get; set; }
        private DateTime DateTime { get; set; }
        private RoomNames MeetingRoom { get; set; }
        


        public Appointment(string clientName, string lawyerName, DateTime dateTime, int meetingRoom)
        {
            ClientName = clientName;
            LawyerName = lawyerName;
            DateTime = dateTime;
            MeetingRoom = (RoomNames)meetingRoom;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetClientId(int clientId)
        {
            ClientId = clientId;
        }

        public void SetLawyerId(int lawyerId)
        {
            LawyerId = lawyerId;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nClient name: {ClientName}\nLawyer name: {LawyerName}\nDate: {DateTime.ToLocalTime().ToString("U")}\nMeetingRoom: {MeetingRoom}\n";
        }
    }
}
