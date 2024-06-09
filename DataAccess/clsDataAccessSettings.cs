
using System.Diagnostics;

namespace DVLD_DataAccess
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString = "Server =.; Database=DVLD;Trusted_Connection=True;";

        public static void LogEx(string ex, EventLogEntryType type)
        {
            string sourceName = "DVLD";

            // Create the event source if it does not exist
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }


            // Log an information event
            EventLog.WriteEntry(sourceName, ex, type);
        }
    }
}
