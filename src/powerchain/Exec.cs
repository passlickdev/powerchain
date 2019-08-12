using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace powerchain
{
    class Exec
    {
        // Default variables
        private static readonly string defaultFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Passlick Development\\data\\blockchain.json";
        private static readonly string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Passlick Development\\data";
       

        static void startupCheck()
        {

            Console.WriteLine("[INFO] Checking for local blockchain file...");


            if (File.Exists(defaultFile))
            {
                Console.WriteLine("[INFO] File check successful");
                startupRead();
            }

            else
            {
                Util.consoleWrite("[WARNING] No local blockchain file found! Creating one...", ConsoleColor.Yellow);
                createBlockchainFile();
            }


        }


        static void startupRead()
        {

            Console.WriteLine("[INFO] Reading local blockchain from file...");
            Dictionary<string, dynamic> chain = Parser.parseToDict(defaultFile);
            Console.WriteLine("[INFO] Successfully read local blockchain from file");

        }




        static void createBlockchainFile()
        {

            FileInfo fi = new FileInfo(@defaultFile);
            DirectoryInfo di = new DirectoryInfo(@defaultPath);


            try
            {
                if (!di.Exists)
                {
                    di.Create();
                }

                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                }

            }

            catch
            {
                Util.consoleWrite("[ERROR] Could not create local blockchain file! Aborting!", ConsoleColor.Red);
            }

            Console.WriteLine("[INFO] Successfully created local blockchain file");
            startupRead();

        }


















        static void Main(string[] args)
        {
            Console.WriteLine($"Passlick Development PowerChain {Assembly.GetEntryAssembly().GetName().Version}");
            Console.WriteLine("(c) Passlick Development 2019. All rights reserved.");
            Console.WriteLine("----------------------------------------------------------------------------------------------------\n");

            Console.WriteLine("[INFO] Starting blockchain service...");
            startupCheck();

            /*Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[INFO]      Reading blockchain from file...");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING]   No blockchain file found! Creating one...");

            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"[ERROR]     Creating blockchain was not successfull! Attempt {i + 1}...");
            }

            Console.WriteLine("[ERROR]     Aborting!");

            Console.ResetColor();*/


        }

    }
}
