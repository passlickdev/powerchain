using System;
using System.Security.Cryptography;
using System.Text;

namespace powerchain
{
    class Block
    {

        // Fields
        public DateTime timestamp { get; }
        public string hash { get; }
        public string prevHash { get; }
        public bool genBlock { get; }
        public dynamic data { get; }


        /// <summary>
        /// Constructor of class 'Block'
        /// </summary>
        /// <param name="prevHash">Hash of previous block in chain</param>
        /// <param name="genBlock">Indicates if genesis block or not</param>
        /// <param name="data">Data content of block</param>
        public Block(string prevHash, dynamic data, bool genBlock = false)
        {
            this.timestamp = DateTime.UtcNow;
            this.prevHash = prevHash;
            this.genBlock = genBlock;
            this.data = data;
            this.hash = calcHash();
        }


        /// <summary>
        /// Calculates the SHA256 hash of the current block
        /// </summary>
        /// <returns>SHA256 hash of block (string)</returns>
        public string calcHash()
        {

            // Block data
            string blockHeader = $"{{timestamp={timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")};prevHash={prevHash};genBlock={genBlock.ToString()};data={Convert.ToString(data)}}}";

            // Hash generation
            SHA256 hashGen = SHA256.Create();
            byte[] blockHash = hashGen.ComputeHash(Encoding.UTF8.GetBytes(blockHeader));

            // String builder
            var stringBuilder = new StringBuilder();

            // String generation
            for (int i = 0; i < blockHash.Length; i++)
            {
                stringBuilder.Append(blockHash[i].ToString("x2"));
            }

            return stringBuilder.ToString();

        }

    }
}

// (c) Passlick Development 2019. All rights reserved.