using System;
using System.Collections.Generic;
using System.Linq;

namespace powerchain
{
    class Blockchain
    {

        // Fields
        public List<Block> chain { get; set; } = new List<Block>();


        /// <summary>
        /// Generates the genesis block
        /// </summary>
        /// <returns>Status</returns>
        public bool GenerateGenesis()
        {
            Block genesisBlock = new Block("0", "GENESIS_BLOCK", true);
            AddBlock(genesisBlock);
            return true;
        }


        /// <summary>
        /// Adds a block to the chain
        /// </summary>
        /// <param name="block">Block to be added</param>
        /// <returns>Status</returns>
        public bool AddBlock(Block block)
        {
            chain.Add(block);
            return true;
        }


        /// <summary>
        /// Searches for block by hash
        /// </summary>
        /// <param name="hash">Hash to search for</param>
        /// <returns>'Block' with matching hash</returns>
        public Block GetBlock(string hash) => chain.FirstOrDefault(x => x.hash == hash);


        /// <summary>
        /// Validates the chain
        /// </summary>
        /// <returns>true: Chain is valid; false: Chain is invalid</returns>
        public bool ValidateChain()
        {
            Console.WriteLine("[INFO] Validating blockchain...");

            // Check if chain is empty
            if (chain.Count == 0 || chain == null)
            {
                Util.ConsoleWrite("[ERROR] Blockchain is invalid (No chain found or chain is empty)!", ConsoleColor.Red);
                return false;
            }

            // Validate genesis block
            if (!chain[0].genBlock || chain[0].data != "GENESIS_BLOCK" || chain[0].prevHash != "0" || chain[0].hash != chain[0].CalcHash())
            {
                Util.ConsoleWrite("[ERROR] Blockchain is invalid (Genesis block missing or corrupt)!", ConsoleColor.Red);
                return false;
            }

            // Validate non-genesis blocks
            for (int i = 1; i < chain.Count; i++)
            {

                Block curBlock = chain[i];
                Block prevBlock = chain[i - 1];

                if (curBlock.genBlock)
                {
                    Util.ConsoleWrite($"[ERROR] Blockchain is invalid at Block #{i} (Block is assigned as genesis block)!", ConsoleColor.Red);
                    curBlock.PrintBlock("ERROR", ConsoleColor.Red, i);
                    return false;
                }

                if (curBlock.timestamp == null || curBlock.timestamp == "" || curBlock.hash == null || curBlock.hash == "" || curBlock.prevHash == null || curBlock.prevHash == "" || curBlock.salt == null || curBlock.salt == "")
                {
                    Util.ConsoleWrite($"[ERROR] Blockchain is invalid at Block #{i} (Attributes are corrupt)!", ConsoleColor.Red);
                    curBlock.PrintBlock("ERROR", ConsoleColor.Red, i);
                    return false;
                }

                if (curBlock.hash != curBlock.CalcHash())
                {
                    Util.ConsoleWrite($"[ERROR] Blockchain is invalid at Block #{i} (Calculated hash and cHash does not match)!", ConsoleColor.Red);
                    curBlock.PrintBlock("ERROR", ConsoleColor.Red, i);
                    return false;
                }

                if (curBlock.prevHash != prevBlock.hash || curBlock.prevHash != prevBlock.CalcHash())
                {
                    Util.ConsoleWrite($"[ERROR] Blockchain is invalid at Block #{i} (Hash of previous block and pHash does not match)!", ConsoleColor.Red);
                    curBlock.PrintBlock("ERROR", ConsoleColor.Red, i);
                    return false;
                }
            }

            Console.WriteLine($"[INFO] Blockchain is valid! Checked {chain.Count} block(s)");
            return true;
        }

    }
}

// (c) Passlick Development 2019. All rights reserved.