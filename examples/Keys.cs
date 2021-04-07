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
            }
            else
            {
                Console.WriteLine("Wrong!");
            }
            Console.WriteLine(pk2.ToHex());

            //var bytes = StringToByteArray(privateKeyHex);


            //Console.WriteLine(ToHex(bytes));
        }

        //static string ToHex(byte[] data)
        //{
        //    string hex = "";
        //    foreach (var bt in data)
        //    {
        //        hex += bt.ToString("x2");
        //    }
        //    return hex;
        //}

        //public static byte[] StringToByteArray(string hex)
        //{
        //    return Enumerable.Range(0, hex.Length)
        //                     .Where(x => x % 2 == 0)
        //                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
        //                     .ToArray();
        //}
    }
}
