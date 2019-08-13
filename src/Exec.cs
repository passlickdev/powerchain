using System;
using System.IO;
using System.Reflection;

namespace powerchain
{
    class Exec
    {

        // Fields
        private static readonly string defaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Passlick Development\\data\\blockchain.json";
        private static readonly string defaultDirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Passlick Development\\data";

        // Blockchain
        private static Blockchain blockchain = new Blockchain();


        /// <summary>
        /// Loads blockchain from JSON file
        /// </summary>
        /// <param name="filePath">Relative path to file</param>
        static void loadBlockchain(string filePath)
        {

            Console.WriteLine("[INFO] Reading local blockchain from file...");
            blockchain.chain = Parser.deserializeToChain(filePath);
            Console.WriteLine("[INFO] Successfully read local blockchain from file");

        }


        /// <summary>
        /// Save blockchain to file
        /// </summary>
        /// <param name="filePath">Relative path to file</param>
        static void saveBlockchain(string filePath)
        {

            Console.WriteLine("[INFO] Saving blockchain to file...");
            Parser.serializeToJSON(blockchain.chain, filePath);
            Console.WriteLine("[INFO] Successfully saved local blockchain to file");

        }


        /// <summary>
        /// Validates chain of blockchain
        /// </summary>
        /// <returns>true: chain is valid; false: chain is invalid</returns>
        static bool validateBlockchain() => blockchain.validateChain();


        /// <summary>
        /// Checks the existence of a local blockchain file
        /// </summary>
        /// <param name="filePath">Relative path to file</param>
        static void checkExistence(string filePath)
        {

            Console.WriteLine("[INFO] Checking for local blockchain file...");

            if (File.Exists(filePath)) Console.WriteLine("[INFO] File check successful");
            else Util.consoleWrite("[WARNING] No local blockchain file found! Create a blockchain with param '/init'", ConsoleColor.Yellow);

        }


        /// <summary>
        /// Creates a new blockchain and saves it
        /// </summary>
        /// <param name="dirPath">Relative path to dir</param>
        /// <param name="filePath">Relative path to file</param>
        static void createBlockchain(string dirPath, string filePath)
        {

            FileInfo fi = new FileInfo(@filePath);
            DirectoryInfo di = new DirectoryInfo(@dirPath);

            Console.WriteLine("[INFO] Creating local blockchain...");

            try
            {

                if (!di.Exists) di.Create();
                if (!fi.Exists)
                {
                    blockchain.generateGenesis();
                    Parser.serializeToJSON(blockchain.chain, filePath);
                }

            }

            catch
            {
                Util.consoleWrite("[ERROR] Could not create local blockchain file! Aborting...", ConsoleColor.Red);
            }

            Console.WriteLine("[INFO] Successfully created local blockchain!");

        }


        /// <summary>
        /// Adds a block to the blockchain
        /// </summary>
        /// <param name="string">Data to be added</param>
        static void addBlockToChain(string data)
        {

            Console.WriteLine("[INFO] Adding data to blockchain...");
            blockchain.addBlock(new Block(blockchain.chain[blockchain.chain.Count - 1].hash, data));
            Console.WriteLine("[INFO] Successfully added data to blockchain");

        }


        /// <summary>
        /// Gets data from block, searched by hash
        /// </summary>
        /// <param name="hash">Hash to be searched for</param>
        static void getDataFromBlock(string hash)
        {

            Console.WriteLine($"[INFO] Searching for hash '{hash}' in chain...");

            Block getBlock = blockchain.getBlock(hash);
            if (getBlock != null)
            {
                Console.WriteLine($"[INFO] Block with hash '{hash}' found!");
                Console.WriteLine($"[INFO] Data output: {getBlock.data}");
            }

            else
            {
                Util.consoleWrite($"[WARNING] No block with hash '{hash}' found!", ConsoleColor.Yellow);
            }

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
                Util.consoleWrite("[WARNING] Please provide a start parameter! For help, use '/help' or '/?'", ConsoleColor.Yellow);
                Console.WriteLine("[INFO] Exiting program...");
                Environment.Exit(0);
            }

            else
            {

                switch (args[0])
                {

                    case "/RUN":
                    case "/run":
                        break;

                    case "/INIT":
                    case "/init":
                        checkExistence(defaultFilePath);
                        createBlockchain(defaultDirPath, defaultFilePath);
                        validateBlockchain();
                        saveBlockchain(defaultFilePath);
                        break;

                    case "/LOAD":
                    case "/load":
                        break;

                    case "/SAVE":
                    case "/save":
                        break;

                    case "/VALIDATE":
                    case "/validate":
                        Console.WriteLine("[INFO] Starting blockchain service...");
                        loadBlockchain(defaultFilePath);
                        validateBlockchain();
                        break;

                    case "/ADD":
                    case "/add":
                        Console.WriteLine("[INFO] Starting blockchain service...");
                        loadBlockchain(defaultFilePath);
                        addBlockToChain(args[1]);
                        validateBlockchain();
                        saveBlockchain(defaultFilePath);
                        break;

                    case "/GET":
                    case "/get":
                        Console.WriteLine("[INFO] Starting blockchain service...");
                        loadBlockchain(defaultFilePath);
                        validateBlockchain();
                        getDataFromBlock(args[1]);
                        break;

                    case "/ABOUT":
                    case "/about":
                        Util.consoleWrite($"[INFO] PowerChain Blockchain Solution {Assembly.GetEntryAssembly().GetName().Version}", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] (c) Passlick Development 2019. All rights reserved.", ConsoleColor.Cyan);
                        Console.WriteLine("");
                        Util.consoleWrite("[INFO] Website: https://passlickdev.com", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] GitHub: https://pdev.me/powerchain-github", ConsoleColor.Cyan);
                        Console.WriteLine("");
                        Util.consoleWrite("[INFO] This software is licensed under GNU General Public License v3.0", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] Made with <3 in Münster", ConsoleColor.Cyan);
                        break;          // TODO: About

                    case "/HELP":
                    case "/help":
                    case "/?":
                        Util.consoleWrite("[INFO] /RUN           Runs the standard routine", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /INIT          Initializes a new local blockchain", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /LOAD          Loads a local blockchain file", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /SAVE          Saves the current blockchain", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /VALIDATE      Validates the current blockchain", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /ADD [DATA]    Adds data to the current blockchain", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /GET [HASH]    Gets data in the current blockchain by hash", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /ABOUT         Gives information about this software", ConsoleColor.Cyan);
                        Util.consoleWrite("[INFO] /HELP          Opens the PowerChain help", ConsoleColor.Cyan);
                        break;

                    default:
                        Util.consoleWrite("[WARNING] Unknown parameter. For help, use '/help' or '/?'", ConsoleColor.Yellow);
                        Console.WriteLine("[INFO] Exiting program...");
                        Environment.Exit(0);
                        break;

                }

            }
        }
    }
}

// (c) Passlick Development 2019. All rights reserved.