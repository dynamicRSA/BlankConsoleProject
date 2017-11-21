using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using log4net;

namespace ImportCert
{
    internal class Program
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool m_IgnoreFirstRow = true;
        public static bool configurationLoaded;
        public static bool GLOBAL_INIT_OK;


        public static void LoadConfiguration()
        {
            try
            {
                if (!configurationLoaded)
                {
                    //load configuration

                    log.Info("-----------------------CONSOLE APP-------------------------");
                    log.Info(" <!-- Logger information for Log4Net -->");
                    log.Info(" Log Level        : " + getLogLevel(log));
                    log.Info(" Log Name         : " + log.Logger.Name);
                    log.Info("-----------------------------------------------------------------------");
                }
            }
            catch (ConfigurationException cEx)
            {
                log.Fatal("Failed to load configuration: " + cEx.Message);
            }
        }

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

        private static string getLogLevel(ILog theLogger)
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
            configurationLoaded = false;
            LoadConfiguration();
            GLOBAL_INIT_OK = configurationLoaded;
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