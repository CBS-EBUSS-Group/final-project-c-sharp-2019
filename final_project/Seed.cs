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
            
            //Console.WriteLine("Creating Database Schema...");
            //db.CreateSchema();

            db.CreateSeed();
            
            db.Disconnect();
            Console.WriteLine("Disconnected from Database.");
        }
    }
}
