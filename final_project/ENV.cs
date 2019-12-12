namespace final_project
{
    public static class ENV
    {
        // please enter a file directory for the sqlite database
        private static readonly string SQLiteURI = "Data Source=< enter a file path for your database here >";

        // please enter the local root path of the application + /final_project/seed/seed.txt
        private static readonly string SeedData = "< enter the root directory of the application here >/final_project/seed/seed.txt";

        public static string GetURI()
        {
            return SQLiteURI;
        }

        public static string GetSeedData()
        {
            return SeedData;
        }
    }
}