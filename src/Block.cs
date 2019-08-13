using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace powerchain
{
    class Block
    {

        // Fields
        public string timestamp { get; }
        public string hash { get; }
        public string prevHash { get; }
        public string salt { get; }
        public bool genBlock { get; }
        public string data { get; }


        /// <summary>
        /// Constructor of class 'Block' (for new blocks)
        /// </summary>
        /// <param name="prevHash">Hash of previous block in chain</param>
        /// <param name="genBlock">Indicates if genesis block or not</param>
        /// <param name="data">Data content of block</param>
        public Block(string prevHash, string data, bool genBlock = false)
        {

            this.timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
            this.prevHash = prevHash;
            this.salt = Util.randomString();
            this.genBlock = genBlock;
            this.data = data;
            this.hash = calcHash();

        }


        /// <summary>
        /// Constructor of class 'Block' (for existing blocks)
        /// </summary>
        /// <param name="timestamp">Timestamp of current block</param>
        /// <param name="hash">Hash of current block</param>
        /// <param name="prevHash">Hash of previous block</param>
        /// <param name="salt">Salt of current block</param>
        /// <param name="genBlock">Indicates if genesis block or not</param>
        /// <param name="data">Data content of block</param>
        [JsonConstructor]
        public Block(string timestamp, string hash, string prevHash, string salt, bool genBlock, string data)
        {

            this.timestamp = timestamp;
            this.hash = hash;
            this.prevHash = prevHash;
            this.salt = salt;
            this.genBlock = genBlock;
            this.data = data;

        }


        /// <summary>
        /// Calculates the SHA256 hash of the current block
        /// </summary>
        /// <returns>SHA256 hash of block (string)</returns>
        public string calcHash()
        {

            // Block data
            string blockHeader = $"{{timestamp={timestamp};prevHash={prevHash};salt={salt};genBlock={genBlock.ToString()};data={data}}}";

            // Hash generation
            SHA256 hashGen = SHA256.Create();
            byte[] blockHash = hashGen.ComputeHash(Encoding.UTF8.GetBytes(blockHeader));

            // String builder
            var stringBuilder = new StringBuilder();

            // String generation
            for (int i = 0; i < blockHash.Length; i++)
                stringBuilder.Append(blockHash[i].ToString("x2"));

            return stringBuilder.ToString();

        }

    }
}

// (c) Passlick Development 2019. All rights reserved.