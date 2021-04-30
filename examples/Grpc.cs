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

            var host = "node0.testnet.rchain-dev.tk"; //test net
            var readonlyHost = "observer.testnet.rchain.coop";
            var port = 40401;
            var client = new RClient(readonlyHost, port);
            var list = client.ShowBlocks(2);
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            var blockHash = "7850712f679aa73cac5648867652d78337ace8066ece72edb195bedfa5f2eeef";
            var blockInfo = client.ShowBlock(blockHash);
            Console.WriteLine(blockInfo);

            var lastFinalizedBlock = client.LastFinalizedBlock();
            Console.WriteLine("Last Finalized Block:" + lastFinalizedBlock.BlockInfo_.BlockHash);

            if (client.IsFinalized(blockHash))
            {
                Console.WriteLine(string.Format("The block: {0} is finalized!", blockHash));
            }

            var blockAtHeight = client.GetBlocksByHeights(lastFinalizedBlock.BlockInfo_.BlockNumber,
                lastFinalizedBlock.BlockInfo_.BlockNumber + 10);
            Console.WriteLine(string.Format("There are {0} blocks between {1} and {2}.", blockAtHeight.Count,
                lastFinalizedBlock.BlockInfo_.BlockNumber,
                lastFinalizedBlock.BlockInfo_.BlockNumber + 10));

            // exploratory deploy can only used for read-only node
            // this method is for exploring the data in the tuple space
            string exploratoryTerm = "new return in{return!(\"a\")}";
            var result = client.ExploratoryDeploy(exploratoryTerm, lastFinalizedBlock.BlockInfo_.BlockHash);
            Console.WriteLine(string.Format("Sucessfully fetched {0} lines", result.Count));

            var depolyId = "3045022100e173e2724df85574dd9bf1f30b35a2cffd53302a1cecd417cbd27bd12e46439602204696d2f4e5695efb6478fe2537c1d4abd77399e5e46e321b619b767963c7e593";
            var deploy = client.FindDeploy(depolyId);
            Console.WriteLine("The block hash of the deploy is: " + deploy.BlockHash);

            // only valid validator can process deploy request
            // all the methods above can be processed by the validator except exploratory deploy
            client = new RClient(host, port);

            PrivateKey admin_key = new PrivateKey("13ae0c530d4af11894910d01f770c1402d19a3ac195dede9c57caa29e6ee82ba");
            var contract = "@1!(2)";
            // normal deploy
            var deploy_id = client.Deploy(admin_key, contract, 1, 1000000, 100, 1000000);
            // deploy with validate after block number argument
            // the difference between `deploy` and `deploy_with_vabn_filled` is that
            // valid after block number is not auto filled by fetching the newest block number which would be assure
            // your block number is valid for the validator
            // Strongly recommend you use this method unless you know what you are doing.
            var deploy_id2 = client.DeployWithVABNFilled(admin_key, contract, 1, 1000000, Util.DateTimeToUtc(DateTime.Now.ToUniversalTime()));
            // this will raise a exception
            try
            {
                result = client.ExploratoryDeploy(exploratoryTerm, lastFinalizedBlock.BlockInfo_.BlockHash);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Raised and exception: " + ex.Message);
            }


        }
    }
}
