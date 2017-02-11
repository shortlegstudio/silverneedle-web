namespace SilverNeedle
{
    public static class Configuration
    {
        static Configuration() 
        {
            DataPath = "./SilverNeedle/data";
        }

        public static string DataPath { get; set; }
    }
    
}