using System;
namespace final_project
{
    public static class SeedDatabase
    {
        public static void CreateSeedDatabase()
        {
            DbManager dbManager = new DbManager();
            dbManager.Connect();
            dbManager.CreateSeedDatabase();
            dbManager.Disconnect();
        }
    }
}
