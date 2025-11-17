using DR_APIs.Models;

namespace MeetArchiver
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        public static String CountryCode;
        public static User CurrentUser;

        [STAThread]
        static void Main()
        {
            CountryCode = new Locaton().GetCountryByIP();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Archiver());
        }
    }
}