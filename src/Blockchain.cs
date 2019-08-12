using System;
using System.Collections.Generic;

namespace powerchain
{
    class Blockchain
    {

        // Fields
        private List<Block> chain;


        /// <summary>
        /// Constructor of class 'Blockchain'
        /// </summary>
        public Blockchain()
        {
            chain = new List<Block>();
        }


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
            // TODO: Chain validation
            return false;
        }

    }
}

// (c) Passlick Development 2019. All rights reserved.