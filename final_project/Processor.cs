
namespace final_project
{
    public class Processor
    {
        private DbManager db;

        public Processor()
        {
            db = new DbManager();
            db.Connect();
            db.CreateSeed();
        }

        public void Process()
        {
            // %%%%%% code starts here %%%%%

            db.GetAllClients();

            // %%%%%% code ends here %%%%%
        }
    }
}
