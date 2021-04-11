using dotrchain;
using System;
using System.Collections.Generic;
using System.Text;

namespace examples
{
    public class Vault
    {
        public static void Main(string[] args)
        {
            var alice = new PrivateKey("13ae0c530d4af11894910d01f770c1402d19a3ac195dede9c57caa29e6ee82ba");
            var bob = new PrivateKey(0);

            var host = "node0.testnet.rchain-dev.tk"; //test net
            var readonlyHost = "observer.testnet.rchain.coop";
            var port = 40401;

            var client = new RClient(readonlyHost, port);
            var vault = new VaultAPI(client);
            // get the balance of a vault
            // get balance can only perform in the read-only node
            var bob_balance = vault.get_balance(bob.PublicKey.RevAddress);
            Console.WriteLine($"Bob's balance is {bob_balance}");
            var alice_balance = vault.get_balance(alice.PublicKey.RevAddress);
            Console.WriteLine($"Alice's balance is {alice_balance}");
            var client2 = new RClient(host, port);
            // because transfer need a valid deploy
            // the transfer need the private to perform signing
            var vault2 = new VaultAPI(client2);
            var deployId = vault2.transfer(alice.PublicKey.RevAddress, bob.PublicKey.RevAddress, 100000, alice);
            Console.WriteLine($"The deploy id is: {deployId}");
            //while (true)
            //{
                
            //    if(!client.IsFinalized(de))
            //}
            bob_balance = vault.get_balance(bob.PublicKey.RevAddress);
            Console.WriteLine($"Bob's balance is {bob_balance}");
            alice_balance = vault.get_balance(alice.PublicKey.RevAddress);
            Console.WriteLine($"Alice's balance is {alice_balance}");
        }
    }
}
