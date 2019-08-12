using System;
using System.Collections.Generic;
using System.Text;

namespace powerchain
{
    class Block
    {

        private string hash { get; }
        private DateTime timestamp { get; }
        private string data { get; }
        private string prev_hash { get; }


        public Block(string hash, DateTime timestamp, string data, string prev_hash)
        {
            this.hash = hash;
            this.timestamp = timestamp;
            this.data = data;
            this.prev_hash = prev_hash;
        }

    }
}
