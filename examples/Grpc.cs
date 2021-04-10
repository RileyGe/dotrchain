using System;
using System.Collections.Generic;
using System.Text;
using dotrchain;

namespace examples
{
    public class Grpc
    {
        public static void Main(string[] args)
        {
            var host = "observer-asia.services.mainnet.rchain.coop";
            var port = 40401;
            var client = new RClient(host, port);
            var list = client.ShowBlocks(2);
            foreach(var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            var blockHash = "50e3d594ea24fe777953c56c5e7aaf1483a118d2bc13f0cca1e113b83fb027e5";
            var blockInfo = client.ShowBlock(blockHash);
            Console.WriteLine(blockInfo);
            //var contract = "@0!(2)";
            //var privateKeyHex = "ff2ba092524bafdbc85fa0c7eddb2b41c69bc9bf066a4711a8a16f749199e5be";
            //var pk = new PrivateKey(privateKeyHex);
            //var deployData = Util.SignDeploy(pk, contract);
            //var publicKey = pk.PublicKey;
            //if (Util.VerifyDeploy(publicKey, deployData.Sig.ToByteArray(), deployData))
            //{
            //    Console.WriteLine("Right!");
            //}
            //else
            //{
            //    Console.WriteLine("Verify wrong!");
            //}
        }
    }
}
