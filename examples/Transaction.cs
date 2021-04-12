using dotrchain;
using System;
using System.Collections.Generic;
using System.Text;

namespace examples
{
    public class Transaction
    {
        public static void Main(string[] args)
        {
            var readonlyHost = "observer.testnet.rchain.coop";
            var port = 40401;
            var client = new RClient(readonlyHost, port);
            // these param are fixed when the network starts on the genesis
            // the param will never change except hard-fork
            // but different network has different param based on the genesis block
            client.ConfigParam(Params.TestnetParam);
            var block_hash = "7850712f679aa73cac5648867652d78337ace8066ece72edb195bedfa5f2eeef";
            var testnet_transactions = client.get_transaction(block_hash);
        }
    }
}
