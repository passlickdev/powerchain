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
            this.salt = Util.RandomString();
            this.genBlock = genBlock;
            this.data = data;
            this.hash = CalcHash();

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
        public string CalcHash()
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


        /// <summary>
        /// Prints the meta information of the block (except data)
        /// </summary>
        /// <param name="displayType">String to be shown in the brackets</param>
        /// <param name="color">Color of output</param>
        /// <param name="blockId">ID of block</param>
        public void PrintBlock(string displayType, ConsoleColor color, int blockId = -1)
        {

            Util.ConsoleWrite($"[{displayType.ToUpper()}]         Block#       : {((blockId < 0) ? "N/A" : blockId.ToString())}", color);
            Util.ConsoleWrite($"[{displayType.ToUpper()}]         Timestamp    : {timestamp}", color);
            Util.ConsoleWrite($"[{displayType.ToUpper()}]         cHash        : {hash}", color);
            Util.ConsoleWrite($"[{displayType.ToUpper()}]         pHash        : {prevHash}", color);
            Util.ConsoleWrite($"[{displayType.ToUpper()}]         Genesis block: {genBlock.ToString()}", color);

        }

    }
}

// (c) Passlick Development 2019. All rights reserved.