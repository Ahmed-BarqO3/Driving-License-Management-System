using DVLD_Buisness;
using System;
using Microsoft.Win32;
using DVLD.Classes;
using System.Diagnostics;

namespace Re_Project.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\UserRegistration";
            string valueName1 = "Username";
            string valueData1 = Username;

            string valueName2 = "Password";
            string valueData2 = Password;

            try
            {
                if (valueData1 == null)
                {
                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                        {
                            if (key != null)
                            {
                                // Delete the specified value
                                key.DeleteValue(valueName1);
                                key.DeleteValue(valueName2);

                                return true;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string sourceName = "DVLD";

                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");

                }

                // Log an information event
                EventLog.WriteEntry(sourceName, ex.Message, EventLogEntryType.Error);

            }
            try
            {

                Registry.SetValue(keyPath, valueName1, valueData1);
                Registry.SetValue(keyPath, valueName2, valueData2);
                return true;
            }
            catch (Exception ex)
            {
                string sourceName = "DVLD";

                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");

                }

                // Log an information event
                EventLog.WriteEntry(sourceName, ex.Message, EventLogEntryType.Error);

                return false;
            }
        }



        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\UserRegistration";
            string valueName1 = "Username";
            string valueName2 = "Password";


            try
            {
                // Read the value from the Registry
                string value1 = Registry.GetValue(keyPath, valueName1, null) as string;
                string value2 = Registry.GetValue(keyPath, valueName2, null) as string;


                if (value1 != null && value2 != null)
                {
                    Username = value1;
                    Password = value2;
                    return true;
                }
                else
                {
                   return false;
                }
            }
            catch (Exception ex)
            {
                string sourceName = "DVLD";

                // Create the event source if it does not exist
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");

                }

                // Log an information event
                EventLog.WriteEntry(sourceName, ex.Message, EventLogEntryType.Error);

                return false;
            }

        }
    }
}

