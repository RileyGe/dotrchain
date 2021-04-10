using System;
using dotrchain;

namespace examples
{
    public class SignVerify
    {
        public static void Main(string[] args)
        {
            var contract = "@0!(2)";
            var privateKeyHex = "ff2ba092524bafdbc85fa0c7eddb2b41c69bc9bf066a4711a8a16f749199e5be";
            var pk = new PrivateKey(privateKeyHex);
            var deployData = Util.SignDeploy(pk, contract);
            var publicKey = pk.PublicKey;
            if(Util.VerifyDeploy(publicKey, deployData.Sig.ToByteArray(), deployData))
            {
                Console.WriteLine("Right!");
            }
            else
            {
                Console.WriteLine("Verify wrong!");
            }
        }
    }
}
