using System;

namespace final_project
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Seed.CreateSeedDatabase(); // only creates schema and data entries, if there is no 'lawyer' table in the database == no data :)
        }
    }
}
