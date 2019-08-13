using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace powerchain
{
    class Parser
    {

        /// <summary>
        /// Deserializes a JSON to a list of 'Block's
        /// </summary>
        /// <param name="path">Relative path to the blockchain JSON</param>
        /// <returns></returns>
        public static List<Block> deserializeToChain(string path) => JsonConvert.DeserializeObject<List<Block>>(File.ReadAllText(@path));


        /// <summary>
        /// Serializes a list of 'Block's to JSON
        /// </summary>
        /// <param name="chain">Chain of 'Block's (obj)</param>
        /// <param name="path">Save path of JSON</param>
        public static void serializeToJSON(List<Block> chain, string path) => File.WriteAllText(@path, JsonConvert.SerializeObject(chain, Formatting.Indented));

    }
}

// (c) Passlick Development 2019. All rights reserved.