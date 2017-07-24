namespace SilverNeedle
{
    public static class Configuration
    {
        static Configuration() 
        {
            DataPath = "./data";
            if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES")))
            {
                DataPath = System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES");
            }
        }

        public static string DataPath { get; set; }
    }
    
}