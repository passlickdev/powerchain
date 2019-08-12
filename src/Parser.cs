using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace powerchain
{
    class Parser
    {

        /// <summary>
        /// Parses the blockchain JSON into a dictionary
        /// </summary>
        /// <param name="path">Relative path to the blockchain JSON</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> parseToDict(string path)
        {
            Dictionary<string, dynamic> chain = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(File.ReadAllText(@path));
            return chain;
        }


        // TODO: Parse JSON objects to 'Block' objects

    }
}

// (c) Passlick Development 2019. All rights reserved.