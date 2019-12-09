using System;

namespace final_project
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Processor processor = new Processor(); // Instantiates a Processor object, which triggers its constructor, in order to create and seed a database, if needed.
            processor.Process(); 

        }
    }
}
