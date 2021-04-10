using System;
using dotrchain;

namespace examples
{
    public class SignVerify
    {
        public static void Main(string[] args)
        {
            var contract = "@0!(2)";
            //            deploy_data = create_deploy_data(key = private_key,
            //                                 term = contract, phlo_price = 1,
            //                                 phlo_limit = 100000,
            //                                 valid_after_block_no = 10,
            //                                 timestamp_millis = int(time.time() * 1000))

            //assert verify_deploy_data(public_key, deploy_data.sig, deploy_data)
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
