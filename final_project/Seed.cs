using System;
namespace final_project
{
    public static class Seed
    {
        public static void CreateSeedDatabase()
        {
            DbManager db = new DbManager();

            Console.WriteLine("Connecting to SQLite Database");
            db.Connect();

            Console.WriteLine("Creating Database Schema...");
            db.CreateSchema();

            Console.WriteLine("Creating Seed Database...");
            db.CreateSeed();
            Console.WriteLine("Data entries added to database");

            Console.WriteLine("Disconnecting...");
            db.Disconnect();
            Console.WriteLine("Successfully disconnected from Database.");
        }
    }
}
