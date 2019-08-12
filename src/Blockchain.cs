using System;
using System.Collections.Generic;

namespace powerchain
{
    class Blockchain
    {

        // Fields
        public List<Block> chain { get; } = new List<Block>();


        /// <summary>
        /// Generates the genesis block
        /// </summary>
        public void generateGenesis()
        {
            Block genesisBlock = new Block("0", "GENESIS_BLOCK", true);
            addBlock(genesisBlock);
        }


        /// <summary>
        /// Adds a block to the chain
        /// </summary>
        /// <param name="block">Block to be added</param>
        public void addBlock(Block block) => chain.Add(block);


        /// <summary>
        /// Validates the chain
        /// </summary>
        /// <returns>true: Chain is valid; false: Chain is invalid</returns>
        public bool validateChain()
        {

            Console.WriteLine("[INFO] Validating blockchain...");

            // Validate genesis block
            if (!chain[0].genBlock || chain[0].data != "GENESIS_BLOCK" || chain[0].prevHash != "0" || chain[0].hash != chain[0].calcHash())
            {
                Util.consoleWrite("[ERROR] Blockchain is invalid (Genesis block missing or corrupt)!", ConsoleColor.Red);
                return false;
            }

            // Validate non-genesis blocks
            for (int i = 1; i < chain.Count; i++)
            {

                Block curBlock = chain[i];
                Block prevBlock = chain[i - 1];

                if (curBlock.genBlock)
                {
                    Util.consoleWrite($"[ERROR] Blockchain is invalid at Block #{i} (Block is assigned as genesis block)!", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Block#       : {i}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Timestamp    : {curBlock.timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         cHash        : {curBlock.hash}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         pHash        : {curBlock.prevHash}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Genesis block: {curBlock.genBlock.ToString()}", ConsoleColor.Red);
                    return false;
                }

                if (curBlock.hash != curBlock.calcHash())
                {
                    Util.consoleWrite($"[ERROR] Blockchain is invalid at Block #{i} (Calculated hash and cHash does not match)!", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Block#       : {i}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Timestamp    : {curBlock.timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         cHash        : {curBlock.hash}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         pHash        : {curBlock.prevHash}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Genesis block: {curBlock.genBlock.ToString()}", ConsoleColor.Red);
                    return false;
                }

                if (curBlock.prevHash != prevBlock.hash || curBlock.prevHash != prevBlock.calcHash())
                {
                    Util.consoleWrite($"[ERROR] Blockchain is invalid at Block #{i} (Hash of previous block and pHash does not match)!", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Block#       : {i}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Timestamp    : {curBlock.timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         cHash        : {curBlock.hash}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         pHash        : {curBlock.prevHash}", ConsoleColor.Red);
                    Util.consoleWrite($"[ERROR]         Genesis block: {curBlock.genBlock.ToString()}", ConsoleColor.Red);
                    return false;
                }

            }

            Console.WriteLine($"[INFO] Blockchain is valid! Checked {chain.Count} blocks");
            return true;

        }

    }
}

// (c) Passlick Development 2019. All rights reserved.