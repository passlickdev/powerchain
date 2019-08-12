using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace powerchain
{
    class Exec
    {

        // Fields
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
                Util.consoleWrite("[ERROR] Could not create local blockchain file! Aborting :-(", ConsoleColor.Red);
            }

            Console.WriteLine("[INFO] Successfully created local blockchain file");
            startupRead();

        }


        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {

            Console.WriteLine($"Passlick Development PowerChain {Assembly.GetEntryAssembly().GetName().Version}");
            Console.WriteLine("(c) Passlick Development 2019. All rights reserved.");
            Console.WriteLine("----------------------------------------------------------------------------------------------------\n");

            if (args.Length == 0)
            {
                Util.consoleWrite("[WARNING] Please provide a start parameter! For help, use 'help' or '?'", ConsoleColor.Yellow);
                Console.WriteLine("[INFO] Exiting program...");
                Environment.Exit(0);
            }

            else
            {
                switch (args[0])
                {

                    case "RUN":
                    case "run":
                        Console.WriteLine("[INFO] Starting blockchain service...");
                        startupCheck();
                        break;

                    case "INIT":
                    case "init":
                        break;

                    case "VALIDATE":
                    case "validate":
                        break;

                    case "ADD":
                    case "add":
                        break;

                    case "GET":
                    case "get":
                        break;

                    case "ABOUT":
                    case "about":
                        Util.consoleWrite($"[INFO] PowerChain Blockchain Solution {Assembly.GetEntryAssembly().GetName().Version}", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] (c) Passlick Development 2019. All rights reserved.", ConsoleColor.Cyan);
                        Console.WriteLine("");
                        Util.consoleWrite("[INFO] Website: https://passlickdev.com", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] GitHub: https://pdev.me/powerchain-github", ConsoleColor.Cyan);
                        Console.WriteLine("");
                        Util.consoleWrite("[INFO] This software is licensed under GNU General Public License v3.0", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] Made with <3 in Münster", ConsoleColor.Cyan);
                        break;          // TODO: About

                    case "HELP":
                    case "help":
                    case "?":
                        Console.WriteLine("[INFO] RUN           Starts the PowerChain routine");
                        Console.WriteLine("[INFO] ABOUT         Gives information about this software");
                        Console.WriteLine("[INFO] HELP / ?      Opens the PowerChain help");
                        break;

                    default:
                        break;

                }

            }
        }
    }
}

// (c) Passlick Development 2019. All rights reserved.