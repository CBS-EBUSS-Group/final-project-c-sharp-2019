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

        public void GetAllAppointments()
        {
            Console.WriteLine("Getting all appointments");
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
