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

        public DbManager() // Constructor of the class that gets the database location on disk
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

            IDbCommand dbcmd = Connection.CreateCommand(); // CreateCommand is executed on Connection variable that holds an IDbConnection object, which returns an IDbCommand object. The IDbCommand Interface allows commands to be run against the database.

            string command = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='lawyers';"; //The SQL COUNT function returns the number of rows in a query. NULL value will not be counted.

            dbcmd.CommandText = command; // IDbCommand has a Property called CommandText that Gets or sets the text command to run against the data source.

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader()) // The ExecuteReader Method of the IDbCommand interface Executes the CommandText against the Connection and builds an IDataReader.
                {
                    while (reader.Read()) // Read method of the IDataReader Interface Advances the IDataReader to the next record
                    {
                        dbExists = reader.GetInt32(0) != 0; // Bool dbExists is set to true or false based on the output of the IDataReader Object method GetInt32(0), which Gets the 32-bit signed integer value of the specified field (in this case, the ID of the first lawyer) and compares it to zero.
                    }

                    reader.Close(); // Close the connection to the database
                }

                return dbExists; // return Bool value to indicate that a first row was found or false if not
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return false; // If exception, return false
        }

        public void CreateSeed()
        {
            Console.WriteLine("Creating database schema and seeding database...");

            if (!DbExists()) // If Database does not exist, create a new database with Seed 
            {
                IDbCommand dbcmd = Connection.CreateCommand(); // CreateCommand is executed on Connection variable that holds an IDbConnection object, which returns an IDbCommand object. The IDbCommand Interface allows commands to be run against the database.

                string command = File.ReadAllText(ENV.GetSeedData()); // The contents of the seed.txt file, localted at the file path returned by the GetSeedData method of the ENV class, are read into the command variable. The seed.txt file contains a SQL command that creates an SQL database with a set of predefined entries for Lawyers, Receptionists, AdminStaff, appointments, and clients.

                dbcmd.CommandText = command; // The command property resembles the command that is to be run on a SQL database, is set to the file path of the seed.txt file in the ENV class

                try
                {
                    dbcmd.ExecuteNonQuery(); // Tries to execute an SQL command contained in the seed.txt file against the Connection object and returns the number of rows affected.. Thus, it is a NonQuery method.
                    Console.WriteLine("Data entries added to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while creating seed database: {ex.Message}"); // Any error message triggers this exception and informs the user of the fialed attempt.
                }
            }
            else
                Console.WriteLine("Local database already has entries. Stopped seeding process."); // If DbExists was true and entries exist in the local database, then it is used.

        }





        // ############# Employee Database Methods #############

        private bool CredentialsAreValid(string username, string password)
        {
            string storedpass;

            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee credential record from credentials table
            string command = $"SELECT username, password FROM credentials WHERE username='{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    storedpass = reader.GetString(1);

                    if (storedpass == password) // Check if password stored in SQLite database is the same as the password entered.
                    {
                        return true;
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return false;
        }

        public string GetEmployeeType(string username)
        {
            string employeeType;

            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee type from the employee table, using the employee foreign key from the credentials table
            string command = $"SELECT type FROM credentials WHERE username = '{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    employeeType = reader.GetString(0);
                }
                return employeeType;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null;
        }

        public Lawyer GetLawyerRecord(string username)
        {
            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee credential record from credentials table
            string command = $"SELECT id, first_name, last_name, date_joined, birthdate, seniority, specialization FROM lawyers WHERE username = '{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    Lawyer NewLawyer = new Lawyer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), reader.GetInt32(5), reader.GetInt32(6));
                    return NewLawyer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null; // Null if the database could not find the record.
        }

        public AdminStaff GetAdminStaff(string username)
        {
            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee credential record from credentials table
            string command = $"SELECT id, first_name, last_name, date_joined, role FROM admins WHERE username = '{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    AdminStaff NewAdmin = new AdminStaff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)), reader.GetInt32(4));
                    return NewAdmin;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null; // Null if the database could not find the record.
        }

        public Receptionist GetReceptionist(string username)
        {

            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee credential record from credentials table
            string command = $"SELECT id, first_name, last_name, date_joined FROM receptionists WHERE username = '{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    Receptionist NewRecep = new Receptionist(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)));
                    return NewRecep;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null; // Null if the database could not find the record.
        }

        // Receptionist, Lawyer, Amins >>> login
        public Employee Login(string username, string password)
        {
            // Based on the username passed into the login function, we create the respective employee child object

            if (GetEmployeeType(username) == "lawyer")
            {
                return GetLawyerRecord(username);
            }
            else if (GetEmployeeType(username) == "adminstaff")
            {
                return GetAdminStaff(username);
            }
            else if (GetEmployeeType(username) == "receptionist")
            {
                return GetReceptionist(username);
            }
            else
            {
                Console.WriteLine("An unknown error has occurred.");
            }
            return null; // Returns an empty lawyer to show that an error occurred.
        }

        /*
        public int CreateNewId(string tableName) // Searches for a specified table and returns the last id + 1, in order to insert a new record
        {
            int latestId;

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"SELECT id FROM '{tableName}' ORDER BY id desc";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    latestId = reader.GetInt32(0);

                }

                return (latestId + 1);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return 0;


        }
        */

        // Receptionist >>> registers new client
        public void SetClient(string name, DateTime bday, string caseType, string street, string zip, string city)
        {
            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO clients('name', 'birthdate', 'case_type', 'street', 'zip', 'city') VALUES('{name}', '{bday.Year}-{bday.Month}-{bday.Day}', '{caseType}', '{street}', '{zip}', '{city}')";

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
        public void SetAppointment(string clientName, string lawyerLastName, DateTime date, string meetingRoom)
        {
            int clientId = GetIdFromTableByColumn("clients", clientName, "name");
            int lawyerId = GetIdFromTableByColumn("lawyers", lawyerLastName, "last_name");
​
        IDbCommand dbcmd = Connection.CreateCommand();
​
        string command = $"INSERT INTO appointments('client_id', 'lawyer_id', 'date_time', 'meeting_room') VALUES('{clientId}', '{lawyerId}', '{date.Year}-{date.Month}-{date.Day} {date.Hour}:{date.Minute}:{date.Second}','{meetingRoom}')";
​
        dbcmd.CommandText = command;
​
        try
            {
                dbcmd.ExecuteNonQuery();
                Console.WriteLine("Appointment added to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            // check whether a meeting room is already booked or not
            // check for lawyer specialization: two options. 1.) "GetLawyersBySpecialization()" only offer a list of lawyers WHERE specialization = '<specialization>' 2.) "LawyerIsOfType(enum specialzation)" enter name > search lawyer > check specialization > return bool
            // check whether lawyer is available
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

            string query = "SELECT name, type, start_date, total_charges FROM cases INNER JOIN clients ON client_id = clients.id";

            dbcmd.CommandText = query;

            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                do
                {
                    while (reader.Read())
                    {
                        string casePrompt = $"name: {reader.GetString(0)}\ncase type: {reader.GetInt32(1)}\nstart date: {reader.GetString(2)}\ntotal charges: {reader.GetString(3)}";

                        consoleText.Add(casePrompt);

                    }
                } while (reader.NextResult());

                reader.Close();
            }

            return consoleText;
        }

        // returns a client statt void, aber Dorian hat die client class noch nicht erstellt
        private int GetIdFromTableByColumn(string tableName, string columnName, string searchWord)
        {
            int id = 0;

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT id FROM {tableName} WHERE {columnName} like '%{searchWord}%'";

            dbcmd.CommandText = query;

            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);

                    // for testing
                    Console.WriteLine(id);
                }

                reader.Close();
            }

            return id;
        }

        // Lawyer >>> adds a new case
        public void SetCase(int id, string clientName, string caseType, DateTime date, string totalCharges)
        {
            int clientId = GetIdFromTableByColumn("clients", "name", clientName);

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO cases('id', 'client_id', 'type', 'start_date', 'total_charges') VALUES('{id}', '{clientId}', '{caseType}', '{date.Year}-{date.Month}-{date.Day} {date.Hour}:{date.Minute}:{date.Second}','{totalCharges}')";

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
