using System;
using System.IO;
using System.Text;
using System.Data;
using Mono.Data.Sqlite;

namespace final_project
{
    public class DbManager
    {
        private string DatabaseURI { get; set; }
        private IDbConnection Connection { get; set; }

        public DbManager()
        {
            DatabaseURI = ENV.GetURI();
        }

        public void Connect()
        {
            Connection = new SqliteConnection(DatabaseURI);
            Connection.Open();
        }

        public void Disconnect()
        {
            Connection.Close();
        }

        private bool DbExists()
        {
            bool dbExists = false;

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='lawyers';";

            dbcmd.CommandText = command;
            
            try
            {
                IDataReader reader = dbcmd.ExecuteReader();

                while (reader.Read())
                {
                    dbExists = reader.GetInt32(0) != 0;
                }

                reader.Close();

                return dbExists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return true;
        }

        //public void CreateSchema()
        //{
        //    if (!DbExists())
        //    {
        //        IDbCommand dbcmd = Connection.CreateCommand();

        //        StringBuilder command = new StringBuilder();

        //        string lawyerSchema = "CREATE TABLE lawyers (id INTEGER PRIMARY KEY, first_name TEXT NOT NULL, last_name TEXT NOT NULL, birthdate TEXT NOT NULL, seniority TEXT NOT NULL, specialization TEXT NOT NULL, date_joined TEXT NOT NULL);";
        //        string receptionistSchema = "CREATE TABLE receptionists (id INTEGER PRIMARY KEY, first_name TEXT NOT NULL, last_name TEXT NOT NULL, date_joined TEXT NOT NULL);";

        //        command.Append(lawyerSchema).Append(receptionistSchema);

        //        dbcmd.CommandText = command.ToString();

        //        dbcmd.ExecuteNonQuery();
        //    }
        //}

        public void CreateSeed()
        {
            if (!DbExists())
            {
                IDbCommand dbcmd = Connection.CreateCommand();

                string command = System.IO.File.ReadAllText(ENV.GetSeedData());

                dbcmd.CommandText = command;

                try
                {
                    Console.WriteLine("Creating Database Schema...");
                    Console.WriteLine("Creating Seed Database...");
                    dbcmd.ExecuteNonQuery();
                    Console.WriteLine("Data entries added to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while creating seed database: {ex.Message}");
                }
            }
            else
                Console.WriteLine("Local database already has entries. Stopped seeding process.");
            
        }

        // gets all the appointments for the day
        public void GetAppointment(DateTime day)
        {
            Console.WriteLine("Getting specific appointment");
        }

        public void SetAppointment()
        {
            Console.WriteLine("Appointment set");
        }

        public void GetAllAppointments(int lawyerId)
        {
            string clientName = "";
            string dateTime = "";
            string meetingRoom = "";

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, date_time, meeting_room FROM appointments INNER JOIN clients ON clients.id = appointments.client_id WHERE appointments.lawyer_id = {lawyerId}";

            dbcmd.CommandText = query;

            IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                clientName = reader.GetString(0);
                dateTime = reader.GetString(1);
                meetingRoom = reader.GetString(2);
                // figure out how to read multiple database rows, implement.
            }

            reader.Close();

            Console.WriteLine($"client: {clientName}\ntime: {dateTime}\nroom: {meetingRoom}");
        }

        public void GetAllClients()
        {
            Console.WriteLine("Getting all clients");
        }

        public void SetCase()
        {
            Console.WriteLine("Saving case");
        }

        public void GetAllCases()
        {
            Console.WriteLine("Getting all cases");
        }

        public void CheckCredentials()
        {
            Console.WriteLine("Checking credentials");
        }
    }
}
