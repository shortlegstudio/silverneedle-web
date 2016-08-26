namespace SilverNeedle
{
    public static class Configuration
    {
        static Configuration() 
        {
            DataPath = "./SilverNeedle/Data";
        }

        public static string DataPath { get; set; }
    }
    
}