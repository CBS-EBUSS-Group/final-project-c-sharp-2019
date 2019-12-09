using System;
namespace final_project
{
    public class Appointment
    {
        protected int Id { get; set; }
        protected int ClientId { get; set; }
        protected int LawyerId { get; set; }
        protected DateTime DateTime { get; set; }
        protected string MeetingRoom { get; set; }


        public Appointment(int id, int clientId, int lawyerId, DateTime dateTime, string meetingRoom)
        {
            Id = id;
            ClientId = clientId;
            LawyerId = lawyerId;
            DateTime = dateTime;
            MeetingRoom = meetingRoom;
        }

        public override string ToString()
        {
            return $"ID: {Id}, ClientID: {ClientId}, LawyerID: {LawyerId}, Datetime {DateTime}, MeetingRoom: {MeetingRoom}";
        }
    }
}
