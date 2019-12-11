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
        private DateTime Date { get; set; }
        private RoomNames MeetingRoom { get; set; }
        


        public Appointment(string clientName, string lawyerName, DateTime date, int meetingRoom)
        {
            ClientName = clientName;
            LawyerName = lawyerName;
            Date = date;
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

        // returns true, if the given room and time do not collide with this appointment; returns false on collision
        public bool RoomIsAvailable(int room, DateTime date)
        {
            if (room == (int)MeetingRoom && ((date > Date && date < Date.AddHours(1)) || (date.AddHours(1) > Date && date.AddHours(1) < Date.AddHours(1))))
                return false;
            else return true;
        }

        // returns true if the given lawyer and time do not collide with this appointment; returns false on collision
        public bool LawyerIsAvailabile(int lawyerId, DateTime date)
        {
            if (lawyerId == LawyerId && ((date > Date && date < Date.AddHours(1)) || (date.AddHours(1) > Date && date.AddHours(1) < Date.AddHours(1))))
                return false;
            else return true;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nClient name: {ClientName}\nLawyer name: {LawyerName}\nDate: {Date.ToLocalTime().ToString("U")}\nMeeting room: {MeetingRoom}\n";
        }
    }
}
