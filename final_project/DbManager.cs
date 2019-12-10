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

        public Lawyer GetLawyer(string username)
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
                    return NewLawyer;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A database error occurred: {ex.Message}");
            }
            return null; // Null if the database could not find the record.
        }

        public AdminStaff GetAdmin(string username)
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
            string command = $"SELECT receptionist_id, first_name, last_name, date_joined FROM receptionists WHERE username = '{username}'";

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

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        // Receptionist >>> registers new client >>> DONE
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

        // Receptionist >>> adds a new appointment >>> DONE
        public void SetAppointment(string clientName, string lawyerName, DateTime date, int meetingRoom)
        {
            int clientId = GetFieldFromTableByColumn("client_id", "clients", "name", clientName);
            int lawyerId = GetFieldFromTableByColumn("lawyer_id", "lawyers", "last_name", lawyerName.Split()[1]);

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO appointments('a_client_id', 'a_lawyer_id', 'date_time', 'meeting_room') VALUES('{clientId}', '{lawyerId}', '{date.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}', {meetingRoom})";


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
            // check whether a meeting room is already booked or not
            // check for lawyer specialization: two options. 1.) "GetLawyersBySpecialization()" only offer a list of lawyers WHERE specialization = '<specialization>' 2.) "LawyerIsOfType(enum specialzation)" enter name > search lawyer > check specialization > return bool
            // check whether lawyer is available
        }

        // Receptionist, Lawyer, AdminStaff >>> lists all appointments >>> DONE
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointmentList = new List<Appointment>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, first_name, last_name, date_time, meeting_room, appointment_id, lawyer_id, client_id FROM appointments INNER JOIN clients ON clients.client_id = appointments.a_client_id INNER JOIN lawyers ON lawyers.lawyer_id = appointments.a_lawyer_id";

            dbcmd.CommandText = query;

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

            return appointmentList;
        }

        // Receptionist >>> lists all appointments for a selected date >>> DONE
        public List<Appointment> GetDailyAppointments(DateTime day)
        {
            List<Appointment> appointmentList = new List<Appointment>();

            string startOfDay = day.ToString("yyyy-MM-dd HH:mm:ss");
            string endOfDay = day.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd HH:mm:ss");

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT name, first_name, last_name, date_time, meeting_room, appointment_id, lawyer_id, client_id FROM appointments INNER JOIN clients ON clients.client_id = appointments.a_client_id INNER JOIN lawyers ON lawyers.lawyer_id = appointments.a_lawyer_id WHERE strftime('%Y-%m-%d %H:%M:%S', date_time) BETWEEN '{startOfDay}' AND '{endOfDay}'";

            dbcmd.CommandText = query;

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

            return appointmentList;
        }

        // Receptionist >>> lists all clients >>> DONE except enum
        public List<Client> GetAllClients()
        {
            List<Client> clientList = new List<Client>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = $"SELECT * FROM clients";

            dbcmd.CommandText = query;

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

            return clientList;
        }

        // Lawyer, AdminStaff >>> lists all cases >> DONE except enum

        public List<Case> GetAllCases()
        {
            List<Case> caseList = new List<Case>();

            IDbCommand dbcmd = Connection.CreateCommand();

            string query = "SELECT name, type, start_date, total_charges, case_id, client_id FROM cases INNER JOIN clients ON c_client_id = clients.client_id";

            dbcmd.CommandText = query;

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

            return caseList;
        }

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
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }

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

        // Lawyer >>> adds a new case >>> DONE
        public void SetCase(string clientName, int caseType, DateTime date, string totalCharges)
        {
            int clientId = GetFieldFromTableByColumn("client_id", "clients", "name", clientName);

            IDbCommand dbcmd = Connection.CreateCommand();

            string command = $"INSERT INTO cases('c_client_id', 'type', 'start_date', 'total_charges') VALUES('{clientId}', {caseType}, '{date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}','{totalCharges}')";

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
