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
        /// <returns>Status</returns>
        public static bool LoadBlockchain(string filePath)
        {
            Console.WriteLine("[INFO] Reading local blockchain from file...");

            if (CheckFileExistence(filePath))
            {
                blockchain.chain = Parser.DeserializeToChain(filePath);
                Console.WriteLine("[INFO] Successfully read local blockchain from file");
                if (!ValidateBlockchain()) Util.ConsoleWrite("[WARNING] Blockchain is invalid! Data may be corrupted.", ConsoleColor.Yellow);
                return true;
            }

            else
            {
                Util.ConsoleWrite("[ERROR] File does not exist! Please check your provided path", ConsoleColor.Red);
                return false;
            }
        }


        /// <summary>
        /// Save blockchain to file
        /// </summary>
        /// <param name="dirPath">Relative path to dir</param>
        /// <returns>Status</returns>
        public static bool SaveBlockchain(string dirPath)
        {
            if (blockchain.chain.Count == 0)
            {
                Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                return false;
            }

            else
            {
                if (CheckDirExistence(dirPath))
                {
                    Console.WriteLine("[INFO] Saving blockchain to file...");
                    Parser.SerializeToJSON(blockchain.chain, dirPath + "\\blockchain.json");
                    Console.WriteLine("[INFO] Successfully saved local blockchain to file");
                    return true;
                }

                else
                {
                    Console.WriteLine("[INFO] Saving blockchain to file...");

                    DirectoryInfo di = new DirectoryInfo(@dirPath);
                    di.Create();

                    Parser.SerializeToJSON(blockchain.chain, dirPath + "\\blockchain.json");
                    Console.WriteLine("[INFO] Successfully saved local blockchain to file");
                    return true;
                }
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
        /// Checks the existence of a file
        /// </summary>
        /// <param name="filePath">Relative path to file</param>
        /// <returns>Status</returns>
        public static bool CheckFileExistence(string filePath)
        {
            if (File.Exists(filePath)) return true;
            else return false;
        }


        /// <summary>
        /// Checks the existence of a dir
        /// </summary>
        /// <param name="dirPath">Relative path to dir</param>
        /// <returns>Status</returns>
        public static bool CheckDirExistence(string dirPath)
        {
            if (Directory.Exists(@dirPath)) return true;
            else return false;
        }


        /// <summary>
        /// Creates a new blockchain and saves it
        /// </summary>
        /// <param name="dirPath">Relative path to dir</param>
        /// <param name="filePath">Relative path to file</param>
        /// <returns>Status</returns>
        public static bool CreateBlockchain()
        {
            // Clear chain
            Console.WriteLine("[INFO] Creating blockchain...");
            blockchain.chain = new List<Block>();

            // Genesis block
            Console.WriteLine("[INFO] Generating genesis block...");
            blockchain.GenerateGenesis();
            Console.WriteLine("[INFO] Successfully created genesis block!");
            blockchain.chain[0].PrintBlock("INFO", ConsoleColor.Cyan, 0);

            // Return
            Console.WriteLine("[INFO] Successfully created blockchain!");
            return true;
        }


        /// <summary>
        /// Adds a block to the blockchain
        /// </summary>
        /// <param name="string">Data to be added</param>
        /// <returns>Status</returns>
        public static bool AddBlockToChain(string data)
        {
            if (blockchain.chain.Count == 0)
            {
                Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                return false;
            }

            else
            {
                Console.WriteLine("[INFO] Adding data to blockchain...");
                blockchain.AddBlock(new Block(blockchain.chain[blockchain.chain.Count - 1].hash, data));
                Console.WriteLine("[INFO] Successfully added data to blockchain");
                return true;
            }
        }


        /// <summary>
        /// Gets data from block, searched by hash
        /// </summary>
        /// <param name="hash">Hash to be searched for</param>
        /// <returns>Status</returns>
        public static bool GetDataFromBlock(string hash)
        {
            if (blockchain.chain.Count == 0)
            {
                Util.ConsoleWrite("[ERROR] No blockchain loaded! Create a local blockchain with 'init' or load one with 'load'", ConsoleColor.Red);
                return false;
            }

            else
            {
                Console.WriteLine($"[INFO] Searching for hash '{hash}' in chain...");

                Block getBlock = blockchain.GetBlock(hash);
                if (getBlock != null)
                {
                    Console.WriteLine($"[INFO] Block with hash '{hash}' found!");
                    Console.WriteLine($"[INFO] ===== Data output below =====\n");
                    Util.ConsoleWrite($"'{getBlock.data}'", ConsoleColor.Cyan);
                    return true;
                }

                else
                {
                    Util.ConsoleWrite($"[WARNING] No block with hash '{hash}' found!", ConsoleColor.Yellow);
                    return false;
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
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
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