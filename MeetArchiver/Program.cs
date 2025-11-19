using DR_APIs.Models;
using DR_APIs.Utils;

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
            try
            {
                CountryCode = new Locaton().GetCountryByIP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error determining location: Please make sure you have an active internet connection before trying again.", "Network connection error");
                return;
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Archiver());
        }
    }
}