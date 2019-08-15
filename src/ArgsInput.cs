using System;
using System.Linq;
using System.Reflection;

namespace powerchain
{
    class ArgsInput
    {

        /// <summary>
        /// Starts the console input
        /// </summary>
        public static void Start()
        {
            Console.Write("\nPowerChain> ");

            string input = Console.ReadLine();
            string[] args = input.Split('"')
                     .Select((element, index) => index % 2 == 0
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                           : new string[] { element })
                     .SelectMany(element => element).ToArray();

            ArgsParse(args);
        }


        /// <summary>
        /// Parses given arguments
        /// </summary>
        /// <param name="args">Arguments to be parsed</param>
        public static void ArgsParse(string[] args)
        {
            if (args.Length == 0) Start();
            else
            {
                switch (args[0])
                {

                    case "/RUN":
                    case "/run":
                    case "RUN":
                    case "run":

                        // TODO
                        Util.ConsoleWrite("[WARNING] This does nothing at the moment :-(", ConsoleColor.Yellow);

                        Start();
                        break;

                    case "/INIT":
                    case "/init":
                    case "INIT":
                    case "init":

                        Exec.CreateBlockchain();
                        Exec.ValidateBlockchain();

                        Start();
                        break;

                    case "/LOAD":
                    case "/load":
                    case "LOAD":
                    case "load":

                        if (args.Length >= 2) Exec.LoadBlockchain(args[1]);
                        else Exec.LoadBlockchain(Exec.defaultFilePath);

                        Start();
                        break;

                    case "/SAVE":
                    case "/save":
                    case "SAVE":
                    case "save":

                        if (Exec.blockchain.chain.Count == 0) Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                        else
                        {
                            if (Exec.ValidateBlockchain())
                            {
                                if (args.Length >= 2) Exec.SaveBlockchain(args[1]);
                                else Exec.SaveBlockchain(Exec.defaultDirPath);
                            }

                            else Util.ConsoleWrite("[ERROR] Blockchain is invalid! Data may be corrupted. Aborting...", ConsoleColor.Red);
                        }

                        Start();
                        break;

                    case "/VALIDATE":
                    case "/validate":
                    case "VALIDATE":
                    case "validate":

                        if (Exec.blockchain.chain.Count == 0) Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                        else Exec.ValidateBlockchain();

                        Start();
                        break;

                    case "/ADD":
                    case "/add":
                    case "ADD":
                    case "add":

                        if (Exec.blockchain.chain.Count == 0) Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                        else
                        {
                            if (args.Length >= 2)
                            {
                                if (Exec.ValidateBlockchain()) Exec.AddBlockToChain(args[1]);
                                else Util.ConsoleWrite("[ERROR] Blockchain is invalid! Data may be corrupted. Aborting...", ConsoleColor.Red);
                            }

                            else Util.ConsoleWrite("[ERROR] No data passed! Please pass data with 'add [DATA]'", ConsoleColor.Red);
                        }

                        Start();
                        break;

                    case "/GET":
                    case "/get":
                    case "GET":
                    case "get":

                        if (Exec.blockchain.chain.Count == 0) Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                        else
                        {

                            if (args.Length >= 2)
                            {
                                if (Exec.ValidateBlockchain()) Exec.GetDataFromBlock(args[1]);
                                else Util.ConsoleWrite("[ERROR] Blockchain is invalid! Data may be corrupted. Aborting...", ConsoleColor.Red);
                            }

                            else Util.ConsoleWrite("[ERROR] No hash passed! Please pass a SHA256 hash with 'get [HASH]'", ConsoleColor.Red);

                        }

                        Start();
                        break;

                    case "/ABOUT":
                    case "/about":
                    case "ABOUT":
                    case "about":

                        Util.ConsoleWrite($"====================================================================================================", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"** ABOUT **", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"====================================================================================================", ConsoleColor.Cyan);
                        Util.ConsoleWrite($">>> PowerChain Blockchain Solution {Assembly.GetEntryAssembly().GetName().Version}", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> (c) Passlick Development 2019. All rights reserved.", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"----------------------------------------------------------------------------------------------------", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> Website: https://passlickdev.com", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> GitHub: https://pdev.me/powerchain-github", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"----------------------------------------------------------------------------------------------------", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> This software is licensed under GNU General Public License v3.0", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> Made with <3 in Münster", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"====================================================================================================", ConsoleColor.Cyan);

                        Start();
                        break;

                    case "/HELP":
                    case "/help":
                    case "/?":
                    case "HELP":
                    case "help":
                    case "?":

                        Util.ConsoleWrite($"====================================================================================================", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"** HELP **", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"====================================================================================================", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> RUN           Runs the standard routine", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> INIT          Initializes a new local blockchain", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> LOAD [PATH]   Loads a local blockchain file. Uses default path when no path is given", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> SAVE [PATH]   Saves the current blockchain. Uses default path when no path is given", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> VALIDATE      Validates the loaded blockchain", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> ADD [DATA]    Adds data to the loaded blockchain", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> GET [HASH]    Gets data from the loaded blockchain by SHA256 hash", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> ABOUT         Gives information about this software", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> HELP          Opens the PowerChain help", ConsoleColor.Cyan);
                        Util.ConsoleWrite(">>> EXIT          Exits the application", ConsoleColor.Cyan);
                        Util.ConsoleWrite($"====================================================================================================", ConsoleColor.Cyan);

                        Start();
                        break;

                    case "EXIT":
                    case "exit":

                        Environment.Exit(0);
                        break;

                    default:

                        Util.ConsoleWrite("[WARNING] Unknown argument. For help, use 'help' or '?'", ConsoleColor.Yellow);

                        Start();
                        break;

                }
            }
        }
    }
}

// (c) Passlick Development 2019. All rights reserved.