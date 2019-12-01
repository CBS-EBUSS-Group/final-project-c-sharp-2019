using System;
using System.IO;
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
            Console.WriteLine("Connecting to SQLite Database");
            Connection = new SqliteConnection(DatabaseURI);
            Connection.Open();
        }

        public void Disconnect()
        {
            Connection.Close();
            Console.WriteLine("Disconnected from Database.");
        }

        private bool DbExists()
        {
            bool dbExists = false;

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='lawyers';";

            dbcmd.CommandText = command;
            
            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dbExists = reader.GetInt32(0) != 0;
                    }

                    reader.Close();
                }

                return dbExists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return true;
        }

        public void CreateSeed()
        {
            Console.WriteLine("Creating database schema and seeding database...");

            if (!DbExists())
            {
                IDbCommand dbcmd = Connection.CreateCommand();

                string command = File.ReadAllText(ENV.GetSeedData());

                dbcmd.CommandText = command;

                try
                {
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





        // ############# Employee Database Methods #############

        private bool CredentialsAreValid(string username, string password)
        {
            // add logic here
            return true;
        }

        // Receptionist, Lawyer, Amins >>> login
        public Lawyer Login(string username, string password)
        {
            if (CredentialsAreValid(username, password))
            {
                // add logic here
                Console.WriteLine("Logging in...");
            }

            return new Lawyer();
        }

        // Receptionist >>> registers new client
        public void SetClient(int id, string name, DateTime bday, string caseType, string street, string zip, string city)
        {
            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO clients('id', 'name', 'birthdate', 'case_type', 'street', 'zip', 'city') VALUES('{id}', '{name}', '{bday.Year}-{bday.Month}-{bday.Day}', '{caseType}', '{street}', '{zip}', '{city}')";

            dbcmd.CommandText = command;

            try
            {
                dbcmd.ExecuteNonQuery();
                Console.WriteLine("Client added to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
        }

        private bool RoomIsBooked()
        {
            // add logic here
            return false;
        }

        private bool LawyerIsAvailable()
        {
            // add logic here
            return true;
        }

        // Receptionist >>> adds a new appointment
        public void SetAppointment()
        {
            // check whether a meeting room is already booked or not
            // check for lawyer specialization: two options. 1.) "GetLawyersBySpecialization()" only offer a list of lawyers WHERE specialization = '<specialization>' 2.) "LawyerIsOfType(enum specialzation)" enter name > search lawyer > check specialization > return bool
            // check whether lawyer is available

            Console.WriteLine("Saving appointment to Database");
        }

        // Receptionist, Lawyer, AdminStaff >>> lists all appointments ...done
        public List<string> GetAllAppointments()
        {
            List<string> consoleText = new List<string>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, date_time, meeting_room FROM appointments INNER JOIN clients ON clients.id = appointments.client_id";

            dbcmd.CommandText = query;

            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                do
                {
                    while (reader.Read())
                    {
                        string appointmentPrompt = $"name: {reader.GetString(0)}\ntime: {reader.GetString(1)}\nroom: {reader.GetString(2)}";

                        consoleText.Add(appointmentPrompt);

                        // for testing
                        Console.WriteLine(appointmentPrompt);
                    }
                } while (reader.NextResult());

                reader.Close();
            }

            return consoleText;
        }

        // Receptionist >>> lists all appointments for a selected date ...done
        public List<string> GetDailyAppointments(DateTime day)
        {
            List<string> consoleText = new List<string>();

            string startOfDay = $"'{day.Year}-{day.Month}-{day.Day} 00:00:00'";
            string endOfDay = $"'{day.Year}-{day.Month}-{day.Day} 23:59:59'";

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, date_time, meeting_room FROM appointments INNER JOIN clients ON clients.id = appointments.client_id WHERE strftime('%Y-%m-%d %H:%M:%S', date_time) BETWEEN {startOfDay} AND {endOfDay}";
            
            dbcmd.CommandText = query;

            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                do
                {
                    while (reader.Read())
                    {
                        string appointmentPrompt = $"name: {reader.GetString(0)}\ntime: {reader.GetString(1)}\nroom: {reader.GetString(2)}";

                        consoleText.Add(appointmentPrompt);

                        // for testing
                        Console.WriteLine(appointmentPrompt);
                    }
                } while (reader.NextResult());

                reader.Close();
            }

            return consoleText;
        }

        // Receptionist >>> lists all clients ...done
        public List<string> GetAllClients()
        {
            List<string> consoleText = new List<string>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT * FROM clients";

            dbcmd.CommandText = query;

            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                do
                {
                    while (reader.Read())
                    {
                        string clientPrompt = $"name: {reader.GetString(1)}\nbirthdate: {reader.GetString(2)}\ncase type: {reader.GetString(3)}\nstreet: {reader.GetString(4)}\nzip: {reader.GetString(5)}\ncity: {reader.GetString(6)}";

                        consoleText.Add(clientPrompt);

                        // for testing
                        Console.WriteLine(clientPrompt);
                    }
                } while (reader.NextResult());

                reader.Close();
            }

            return consoleText;
        }

        // Lawyer, AdminStaff >>> lists all cases
        public List<string> GetAllCases()
        {
            List<string> consoleText = new List<string>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = "SELECT name, case_type, start_date, total_charges FROM cases INNER JOIN clients ON clients.id = case.client_id";

            dbcmd.CommandText = query;

            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                do
                {
                    while (reader.Read())
                    {
                        string casePrompt = $"name: {reader.GetString(0)}\ncase type: {reader.GetString(1)}\nstart date: {reader.GetString(2)}\ntotal charges: {reader.GetString(3)}";

                        consoleText.Add(casePrompt);

                        // for testing
                        Console.WriteLine(casePrompt);
                    }
                } while (reader.NextResult());

                reader.Close();
            }

            return consoleText;
        }

        // returns a client statt void, aber Dorian hat die client class noch nicht erstellt
        private int GetClientId(string clientName)
        {
            int clientId = 0;

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT id FROM clients WHERE name like '%{clientName}%'";

            dbcmd.CommandText = query;

            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientId = reader.GetInt32(0);

                    // for testing
                    Console.WriteLine(clientId);
                }

                reader.Close();
            }

            return clientId;
        }

        // Lawyer >>> adds a new case
        public void SetCase(int id,string clientName, string caseType, DateTime date, string totalCharges)
        {
            int clientId = GetClientId(clientName);

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO cases('id', 'client_id', 'case_type', 'start_date', 'total_charges') VALUES('{id}', '{clientId}', '{caseType}', '{date.Year}-{date.Month}-{date.Day} {date.Hour}:{date.Minute}:{date.Second}','{totalCharges}')";

            dbcmd.CommandText = command;

            try
            {
                dbcmd.ExecuteNonQuery();
                Console.WriteLine("Client added to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
        }
    }
}
