using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace powerchain
{
    class Exec
    {

        // Fields
        public static readonly string defaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Passlick Development\\data\\blockchain.json";
        public static readonly string defaultDirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Passlick Development\\data";

        // Blockchain
        public static readonly Blockchain blockchain = new Blockchain();


        /// <summary>
        /// Loads blockchain from JSON file
        /// </summary>
        /// <param name="filePath">Relative path to file</param>
        public static void LoadBlockchain(string filePath)
        {

            Console.WriteLine("[INFO] Reading local blockchain from file...");
            blockchain.chain = Parser.DeserializeToChain(filePath);
            Console.WriteLine("[INFO] Successfully read local blockchain from file");

        }


        /// <summary>
        /// Save blockchain to file
        /// </summary>
        /// <param name="filePath">Relative path to file</param>
        public static void SaveBlockchain(string filePath)
        {

            if (blockchain.chain.Count == 0) Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
            else
            {
                Console.WriteLine("[INFO] Saving blockchain to file...");
                Parser.SerializeToJSON(blockchain.chain, filePath);
                Console.WriteLine("[INFO] Successfully saved local blockchain to file");
            }

        }


        /// <summary>
        /// Validates chain of blockchain
        /// </summary>
        /// <returns>true: chain is valid; false: chain is invalid</returns>
        public static bool ValidateBlockchain()
        {

            if (blockchain.chain.Count == 0)
            {
                Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                return false;
            }

            else return blockchain.ValidateChain();

        }


        /// <summary>
        /// Checks the existence of a local blockchain file
        /// </summary>
        /// <param name="filePath">Relative path to file</param>
        public static void CheckExistence(string filePath)
        {

            Console.WriteLine("[INFO] Checking for local blockchain file...");

            if (File.Exists(filePath)) Console.WriteLine("[INFO] File check successful");
            else Util.ConsoleWrite("[WARNING] No local blockchain file found! Create a blockchain with param '/init'", ConsoleColor.Yellow);

        }


        /// <summary>
        /// Creates a new blockchain and saves it
        /// </summary>
        /// <param name="dirPath">Relative path to dir</param>
        /// <param name="filePath">Relative path to file</param>
        public static void CreateBlockchain(/*string dirPath, string filePath*/)                // TODO
        {

            //FileInfo fi = new FileInfo(@filePath);
            //DirectoryInfo di = new DirectoryInfo(@dirPath);

            Console.WriteLine("[INFO] Creating blockchain...");
            blockchain.chain = new List<Block>();

            Console.WriteLine("[INFO] Generating genesis block...");
            blockchain.GenerateGenesis();
            Console.WriteLine("[INFO] Successfully created genesis block!");
            blockchain.chain[0].PrintBlock("INFO", ConsoleColor.Cyan, 0);

            Console.WriteLine("[INFO] Successfully created blockchain!");


            //try
            //{

            //    if (!di.Exists) di.Create();
            //    if (!fi.Exists)
            //    {
            //        blockchain.GenerateGenesis();
            //        Parser.SerializeToJSON(blockchain.chain, filePath);
            //    }

            //}

            //catch
            //{
            //    Util.ConsoleWrite("[ERROR] Could not create local blockchain file! Aborting...", ConsoleColor.Red);
            //}



        }


        /// <summary>
        /// Adds a block to the blockchain
        /// </summary>
        /// <param name="string">Data to be added</param>
        public static void AddBlockToChain(string data)
        {

            if (blockchain.chain.Count == 0) Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
            else
            {
                Console.WriteLine("[INFO] Adding data to blockchain...");
                blockchain.AddBlock(new Block(blockchain.chain[blockchain.chain.Count - 1].hash, data));
                Console.WriteLine("[INFO] Successfully added data to blockchain");
            }

        }


        /// <summary>
        /// Gets data from block, searched by hash
        /// </summary>
        /// <param name="hash">Hash to be searched for</param>
        public static void GetDataFromBlock(string hash)
        {

            if (blockchain.chain.Count == 0) Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
            else
            {
                Console.WriteLine($"[INFO] Searching for hash '{hash}' in chain...");

                Block getBlock = blockchain.GetBlock(hash);
                if (getBlock != null)
                {
                    Console.WriteLine($"[INFO] Block with hash '{hash}' found!");
                    Console.WriteLine($"[INFO] ===== Data output below =====\n");
                    Util.ConsoleWrite($"'{getBlock.data}'", ConsoleColor.Cyan);
                }

                else
                {
                    Util.ConsoleWrite($"[WARNING] No block with hash '{hash}' found!", ConsoleColor.Yellow);
                }

            }

        }


        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {

            // Exception handler
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // Start output
            Console.WriteLine($"Passlick Development PowerChain {Assembly.GetEntryAssembly().GetName().Version}");
            Console.WriteLine("(c) Passlick Development 2019. All rights reserved.");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            if (args.Length != 0) Console.WriteLine("");

            // Args parser
            ArgsInput.ArgsParse(args);

        }


        /// <summary>
        /// Exception handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Exception</param>
        public static void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
        {

            Util.ConsoleWrite("\n========================================= FATAL EXCEPTION! =========================================", ConsoleColor.Red);
            Util.ConsoleWrite("\nThe application encountered a fatal error. Please report this error under 'Issues' here:\nhttp://pdev.me/powerchain-github (including the following log). The operation could not be performed!", ConsoleColor.Red);
            Util.ConsoleWrite("\n----------------------------------------------------------------------------------------------------\n", ConsoleColor.Red);
            Util.ConsoleWrite(e.ExceptionObject.ToString(), ConsoleColor.Red);
            Util.ConsoleWrite("\n=================================== ! PLEASE REPORT THIS ISSUE ! ===================================\n", ConsoleColor.Red);

            // Exit application
            Environment.Exit(0);

        }

    }
}

// (c) Passlick Development 2019. All rights reserved.