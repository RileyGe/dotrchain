using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using dotrchain;

namespace examples
{
    public class Keys
    {
        public static void Main(string[] args)
        {
            var pk = PrivateKey.Generate();
            Console.WriteLine(pk.ToHex());

            var privateKeyHex = "ff2ba092524bafdbc85fa0c7eddb2b41c69bc9bf066a4711a8a16f749199e5be";
            var pk2 = new PrivateKey(privateKeyHex);
            var generatedPk = pk2.ToHex();
            if(generatedPk == privateKeyHex)
            {
                Console.WriteLine("Right!");
                Console.WriteLine(pk2.ToHex());
            }
            else
            {
                Console.WriteLine("Private key wrong!");
            }

            var publicKey = pk2.PublicKey;
            var pkHex = "04ad4793d81c5ee6c91c4baf2689c5299276c4774a8625fa87257f62ba8f3fe31f79d1351bd83af800afdaa94d40fe46c969f0ce2ac2e03e45d5a2d8a7687c39c0";
            var publicKey2 = new PublicKey(pkHex);
            if(publicKey2.ToHex() == pkHex)
            {
                Console.WriteLine("Right!");
                Console.WriteLine(publicKey2.ToHex());
            }
            else
            {
                Console.WriteLine("Public key wrong!");
            }

            var ethAddress = publicKey2.EthAddress;
            var revAddress = publicKey2.RevAddress;
            //sign example
        }

    }
}
