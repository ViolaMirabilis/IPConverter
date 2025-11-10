using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPConverter
{
    public class IPAddress
    {
        public byte[] Octets { get; set; }  // max 4

        public IPAddress(byte[] validIP)
        {

            Octets = validIP;
        }

        public override string ToString() => string.Join(".", Octets);


    }
}
