using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace powerchain
{
    class Parser
    {
        public static Dictionary<string, dynamic> parseToDict(string path)
        {

            Dictionary<string, dynamic> chain = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(File.ReadAllText(@path));
            return chain;

        }

    }
}
