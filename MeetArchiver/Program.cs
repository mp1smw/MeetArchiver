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

            var h=PasswordHasher.Hash("f023ba24-1c4a-489a-8fe7-2ad5d2719287");

            var hs = PasswordHasher.Hash("admin");


            CountryCode = new Locaton().GetCountryByIP();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Archiver());
        }
    }
}