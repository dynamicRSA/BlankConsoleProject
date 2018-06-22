/*
 *  BLANK CONSOLE PROJECT 
 *  This is a console application template that allows you to drop in your logic into a Console App template that comes with
 * - Logging
 * - Case statement and highlighting
 * - Configurable with config reader
 * - Tasks can run asynchronusly
 *
 */




using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using log4net;

namespace BlankConsoleProject
{
    internal class Program
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool IgnoreFirstRow = true;
        public static bool ConfigurationLoaded;
        public static bool GlobalInit;

        //Load config 
        public static void LoadConfiguration()
        {
            try
            {
                if (!ConfigurationLoaded)
                {
                    log.Info("-----------------------CONSOLE APP-------------------------");
                    log.Info(" <!-- Logger information for Log4Net -->");
                    log.Info(" Log Level        : " + GetLogLevel(log));
                    log.Info(" Log Name         : " + log.Logger.Name);
                    log.Info("-----------------------------------------------------------------------");
                }
            }
            catch (ConfigurationException cEx)
            {
                log.Fatal("Failed to load configuration: " + cEx.Message);
            }
        }

        //Read values that you may require to be configure in the app.config
        private static string GetConfigValue(string key)
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];

                if (value == null)
                    throw new ConfigurationException(
                        string.Format("Could not find key {0} in the configuration file.", key));
                return value;
            }
            catch (Exception)
            {
                throw new ConfigurationException(
                    string.Format("Could not find key {0} in the configuration file.", key));
            }
        }

        //Logging levels
        private static string GetLogLevel(ILog theLogger)
        {
            if (theLogger.IsDebugEnabled)
                return "Debug";
            if (theLogger.IsInfoEnabled)
                return "Info";
            if (theLogger.IsWarnEnabled)
                return "Warn";
            if (theLogger.IsErrorEnabled)
                return "Error";
            if (theLogger.IsFatalEnabled)
                return "Fatal";
            return "None";
        }

        private static void Main(string[] args)
        {
            ConfigurationLoaded = false;
            LoadConfiguration();
            GlobalInit = ConfigurationLoaded;
            // The Main function calls an async method named RunAsync 
            // and then blocks until RunAsyncc completes.

            //This line trusts the SSL cert no matter what so you dont get SSL errors for now
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            var myName = Assembly.GetEntryAssembly().GetName();
            if (myName != null)
            {
                var iAction = 1;

                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("================================================");
                    Console.WriteLine(myName.Name + "   - v" + myName.Version);
                    Console.WriteLine("Select an option from the Menu below:");
                    Console.WriteLine("A    - Instruction 1");
                    Console.WriteLine("B    - Instruction 2");
                    Console.WriteLine("X    - Exit");
                    Console.WriteLine("================================================");
                    while (Console.KeyAvailable)
                        Console.ReadKey(true);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Action : ");
                    var key = Console.ReadKey();
                    Console.WriteLine();
                    if (key.KeyChar == 'X' || key.KeyChar == 'x')
                        iAction = -1;
                    {
                        if (key.KeyChar.ToString().ToUpper().Equals("A"))
                        {
                            //DO A LOGIC
                        }

                        if (key.KeyChar.ToString().ToUpper().Equals("B"))
                        {
                            //DO B LOGIC
                        }
                    }
                } while (iAction > 0);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                log.Info("Console APP Ended");
                log.Info("================================================");
                Console.WriteLine("XXXXXX Console Application ended XXXXX");
                Console.ReadLine();
            }
        }
    }
}