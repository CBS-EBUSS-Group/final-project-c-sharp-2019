using System;
using System.Text;
using System.Collections.Generic;
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

        public void CreateSchema()
        {
            Console.WriteLine("Creating database schema...");
            IDbCommand dbcmd = Connection.CreateCommand();

            StringBuilder command = new StringBuilder();

            string lawyerSchema = "CREATE TABLE salespeople (id TEXT PRIMARY KEY, first_name TEXT NOT NULL, last_name TEXT NOT NULL, commission_rate REAL NOT NULL);";
            string receptionistSchema = "CREATE TABLE receptionists (id INTEGER PRIMARY KEY, first_name TEXT NOT NULL, last_name TEXT NOT NULL);";

            command.Append(lawyerSchema).Append(receptionistSchema);

            dbcmd.CommandText = command.ToString();

            dbcmd.ExecuteNonQuery();
            
        }

        public void CreateSeed()
        {
            Console.WriteLine("Creating seed database...");
            IDbCommand dbcmd = Connection.CreateCommand();

            StringBuilder command = new StringBuilder("BEGIN TRANSACTION;");

            // receptionist credentials
            string receptionistCredentials = "INSERT INTO receptionists(id, first_name, last_name) VALUES(1, 'Peter', 'Parker');";

            // senior lawyer information
            string srLawyer1 = "";
            string srLawyer2 = "";
            string srLawyer3 = "";

            // junior lawyer information
            string jrLawyer1 = "";
            string jrLawyer2 = "";
            string jrLawyer3 = "";
            string jrLawyer4 = "";
            string jrLawyer5 = "";
            string jrLawyer6 = "";
            string jrLawyer7 = "";
            string jrLawyer8 = "";
            string jrLawyer9 = "";

            string[] lawyers = { srLawyer1, srLawyer2, srLawyer3, jrLawyer1, jrLawyer2, jrLawyer3, jrLawyer4, jrLawyer5, jrLawyer6, jrLawyer7, jrLawyer8, jrLawyer9 };

            foreach (string lawyer in lawyers)
                command.Append(lawyer);

            command.Append(receptionistCredentials);

            command.Append("COMMIT;");

            dbcmd.CommandText = command.ToString();

            dbcmd.ExecuteNonQuery();
        }

        public void GetAllAppointments()
        {
            Console.WriteLine("Getting all appointments");
            /*
            IDbCommand dbcmd = Connection.CreateCommand();

            string query = "Select * from Student";
            dbcmd.CommandText = query;

            IDataReader reader = dbcmd.ExecuteReader();

            List<ABC> searchResult = new List<ABC>();

            while (reader.Read())
            {
                Appointment appointment = new Appointment();
                searchResult.Add()
                Console.WriteLine($"Student Id: {reader.GetInt32(0)}, Student Name: {reader.GetString(1)}, Program: {reader.GetString(2)}");
            }

            return searchResult;
            */
        }
        
    }
}
