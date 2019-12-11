using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System.Globalization;

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

        // serves as a helper function to CreateSeed() to detect, if a database exists by querying for the lawyer table
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

            return false; // If exception, return false
        }

        // creates the seed database by reading in sqlite commands from the included seed.txt file in the repository (does not overwrite an existing database)
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

        // validates the username and password input and checks the strings against the database table 'credentials'
        // if credentials are correct, the 'type' field in the database links to the corresponding function to create a child object of the Employee class
        public Employee Login(string username, string password)
        {
            string type = "";

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT * FROM credentials WHERE username = '{username}'";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    if (reader.Read() == false || reader.GetString(1) != password)
                    {
                        Console.WriteLine("Wrong credentials.");
                        return null;
                    }

                    type = reader.GetString(2);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            switch (type)
            {
                case "lawyer":
                    return GetLawyer(username);
                case "admin":
                    return GetAdmin(username);
                case "receptionist":
                    return GetReceptionist(username);
                default:
                    return null;
            }
        }

        // returns an instance of Lawyer for the login
        private Lawyer GetLawyer(string username)
        {
            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee credential record from credentials table
            string command = $"SELECT lawyer_id, first_name, last_name, date_joined, birthdate, seniority, specialization FROM lawyers WHERE username = '{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    Lawyer NewLawyer = new Lawyer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), reader.GetInt32(5), reader.GetInt32(6));

                    reader.Close();

                    return NewLawyer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null; // Null if the database could not find the record.
        }

        // returns an instance of AdminStaff for the login
        private AdminStaff GetAdmin(string username)
        {
            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee credential record from credentials table
            string command = $"SELECT admin_id, first_name, last_name, date_joined, role FROM admins WHERE username = '{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    AdminStaff NewAdmin = new AdminStaff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)), reader.GetInt32(4));

                    reader.Close();

                    return NewAdmin;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null; // Null if the database could not find the record.
        }

        // returns an instance of Receptionist for the login
        private Receptionist GetReceptionist(string username)
        {

            IDbCommand dbcmd = Connection.CreateCommand();

            // Fetch the employee credential record from credentials table
            string command = $"SELECT receptionist_id, first_name, last_name, date_joined FROM receptionists WHERE username = '{username}'";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    Receptionist NewRecep = new Receptionist(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)));

                    reader.Close();

                    return NewRecep;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null; // Null if the database could not find the record.
        }

        // Receptionist >>> registers new client
        public void SetClient(string name, DateTime bday, int caseType, string street, string zip, string city)
        {
            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO clients('name', 'birthdate', 'case_type', 'street', 'zip', 'city') VALUES('{name}', '{bday.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}', '{caseType}', '{street}', '{zip}', '{city}')";

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

        // Receptionist >>> adds a new appointment
        public void SetAppointment(string clientName, int lawyerId, DateTime date, int meetingRoom)
        {
            List<Appointment> appointmentList = GetAllAppointments();
            bool isRoomAvailable = true;
            bool isLawyerAvailable = true;

            foreach (Appointment appointment in appointmentList)
            {
                if (!appointment.RoomIsAvailable(meetingRoom, date))
                    isRoomAvailable = false;
                if (!appointment.LawyerIsAvailabile(lawyerId, date))
                    isLawyerAvailable = false;
            }

            if (!isLawyerAvailable)
            {
                Console.WriteLine("Lawyer is unavailable at that time! Please choose another lawyer or a different time!");
                return;
            }

            if (!isRoomAvailable)
            {
                Console.WriteLine("Room is unavailable at that time! Please try another room!");
                return;
            }

            int clientId = GetFieldFromTableByColumn("client_id", "clients", "name", clientName);

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO appointments('a_client_id', 'a_lawyer_id', 'date_time', 'meeting_room') VALUES('{clientId}', '{lawyerId}', '{date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}', {meetingRoom})";


            dbcmd.CommandText = command;

            try
            {
                dbcmd.ExecuteNonQuery();
                Console.WriteLine("\nAppointment successfully added to database.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
        }

        // Receptionist >>> returns correct lawyers for the case type of the given client for Receptionist.AddNewAppointment()
        public List<Lawyer> GetLawyersByClientCaseType(string clientName)
        {
            List<Lawyer> lawyerList = new List<Lawyer>();
            int clientCaseType = GetFieldFromTableByColumn("case_type", "clients", "name", clientName);

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"SELECT lawyer_id, first_name, last_name, date_joined, birthdate, seniority, specialization FROM lawyers WHERE specialization = {clientCaseType}";

            dbcmd.CommandText = command;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            Lawyer lawyer = new Lawyer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDateTime(reader.GetString(3)), Convert.ToDateTime(reader.GetString(4)), reader.GetInt32(5), reader.GetInt32(6));

                            lawyerList.Add(lawyer);
                        }
                    } while (reader.NextResult());

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return lawyerList;
        }

        // Receptionist, Lawyer, AdminStaff >>> lists all appointments
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointmentList = new List<Appointment>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, first_name, last_name, date_time, meeting_room, appointment_id, lawyer_id, client_id FROM appointments INNER JOIN clients ON clients.client_id = appointments.a_client_id INNER JOIN lawyers ON lawyers.lawyer_id = appointments.a_lawyer_id";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment(reader.GetString(0), $"{reader.GetString(1)} {reader.GetString(2)}", DateTime.ParseExact(reader.GetString(3), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), reader.GetInt32(4));
                            appointment.SetId(reader.GetInt32(5));
                            appointment.SetLawyerId(reader.GetInt32(6));
                            appointment.SetClientId(reader.GetInt32(7));

                            appointmentList.Add(appointment);
                        }
                    } while (reader.NextResult());

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return appointmentList;
        }

        // Receptionist >>> lists all appointments for a selected date
        public List<Appointment> GetDailyAppointments(DateTime day)
        {
            List<Appointment> appointmentList = new List<Appointment>();

            string startOfDay = day.ToString("yyyy-MM-dd HH:mm:ss");
            string endOfDay = day.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd HH:mm:ss");

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, first_name, last_name, date_time, meeting_room, appointment_id, lawyer_id, client_id FROM appointments INNER JOIN clients ON clients.client_id = appointments.a_client_id INNER JOIN lawyers ON lawyers.lawyer_id = appointments.a_lawyer_id WHERE strftime('%Y-%m-%d %H:%M:%S', date_time) BETWEEN '{startOfDay}' AND '{endOfDay}'";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment(reader.GetString(0), $"{reader.GetString(1)} {reader.GetString(2)}", DateTime.ParseExact(reader.GetString(3), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).AddHours(1), reader.GetInt32(4));
                            appointment.SetId(reader.GetInt32(5));
                            appointment.SetLawyerId(reader.GetInt32(6));
                            appointment.SetClientId(reader.GetInt32(7));

                            appointmentList.Add(appointment);
                        }
                    } while (reader.NextResult());

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return appointmentList;
        }

        // Lawyer >>> lists personal appointments
        public List<Appointment> GetAppointmentsByLawyerId(int id)
        {
            List<Appointment> appointmentList = new List<Appointment>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, first_name, last_name, date_time, meeting_room, appointment_id, lawyer_id, client_id FROM appointments INNER JOIN clients ON clients.client_id = appointments.a_client_id INNER JOIN lawyers ON lawyers.lawyer_id = appointments.a_lawyer_id WHERE a_lawyer_id = {id}";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment(reader.GetString(0), $"{reader.GetString(1)} {reader.GetString(2)}", DateTime.ParseExact(reader.GetString(3), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), reader.GetInt32(4));
                            appointment.SetId(reader.GetInt32(5));
                            appointment.SetLawyerId(reader.GetInt32(6));
                            appointment.SetClientId(reader.GetInt32(7));

                            appointmentList.Add(appointment);
                        }
                    } while (reader.NextResult());

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return appointmentList;
        }

        // Receptionist >>> lists all clients
        public List<Client> GetAllClients()
        {
            List<Client> clientList = new List<Client>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT * FROM clients";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            Client client = new Client(reader.GetString(1), DateTime.ParseExact(reader.GetString(2), "yyyy-MM-dd", CultureInfo.InvariantCulture), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                            client.SetId(reader.GetInt32(0));

                            clientList.Add(client);
                        }
                    } while (reader.NextResult());

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return clientList;
        }

        // Lawyer, AdminStaff >>> lists all cases
        public List<Case> GetAllCases()
        {
            List<Case> caseList = new List<Case>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = "SELECT name, type, start_date, total_charges, case_id, client_id FROM cases INNER JOIN clients ON c_client_id = clients.client_id";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            Case @case = new Case(reader.GetString(0), reader.GetInt32(1), DateTime.ParseExact(reader.GetString(2), "yyyy-MM-dd", CultureInfo.InvariantCulture), reader.GetString(3));
                            @case.SetId(reader.GetInt32(4));
                            @case.SetClientId(reader.GetInt32(5));

                            caseList.Add(@case);

                        }
                    } while (reader.NextResult());

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return caseList;
        }

        // Lawyer >>> lists personal cases
        public List<Case> GetMyCases(int id)
        {
            List<Case> caseList = new List<Case>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, type, start_date, total_charges, case_id, client_id FROM cases INNER JOIN clients ON c_client_id = clients.client_id WHERE c_lawyer_id = {id}";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    do
                    {
                        while (reader.Read())
                        {
                            Case @case = new Case(reader.GetString(0), reader.GetInt32(1), DateTime.ParseExact(reader.GetString(2), "yyyy-MM-dd", CultureInfo.InvariantCulture), reader.GetString(3));
                            @case.SetId(reader.GetInt32(4));
                            @case.SetClientId(reader.GetInt32(5));

                            caseList.Add(@case);

                        }
                    } while (reader.NextResult());

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return caseList;
        }

        // queries the id (or another field of type INTEGER) by searchword in a given column and returns it
        private int GetFieldFromTableByColumn(string fieldName , string tableName, string columnName, string searchWord)
        {
            int id = 0;

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT {fieldName} FROM {tableName} WHERE {columnName} like '%{searchWord}%'";

            dbcmd.CommandText = query;

            try
            {
                using (IDataReader reader = dbcmd.ExecuteReader())
                {
                    reader.Read();

                    id = reader.GetInt32(0);

                    reader.Close();
                }

                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }

            return id;
        }

        // Lawyer >>> adds a new case
        public void SetCase(int lawyerId, string clientName, int caseType, DateTime date, string totalCharges)
        {
            int clientId = GetFieldFromTableByColumn("client_id", "clients", "name", clientName);

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO cases('c_lawyer_id', 'c_client_id', 'type', 'start_date', 'total_charges') VALUES('{lawyerId}', '{clientId}', {caseType}, '{date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}','{totalCharges}')";

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
