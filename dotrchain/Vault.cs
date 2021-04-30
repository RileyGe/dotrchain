using System;

namespace dotrchain
{
    public class VaultAPI
    {
        public const string CREATE_VAULT_RHO_TPL = @"
new rl(`rho:registry:lookup`), RevVaultCh in {{
  rl!(`rho:rchain:revVault`, *RevVaultCh) |
  for (@(_, RevVault) <- RevVaultCh) {{
    @RevVault!(""findOrCreateVault"", ""{0}"", Nil)
  }}
}}
";

        public const string GET_BALANCE_RHO_TPL = @"
new return, rl(`rho:registry:lookup`), RevVaultCh, vaultCh, balanceCh in {{
  rl!(`rho:rchain:revVault`, *RevVaultCh) |
  for (@(_, RevVault) <- RevVaultCh) {{
    @RevVault!(""findOrCreate"", ""{0}"", *vaultCh) |
    for (@(true, vault) <- vaultCh) {{
      @vault!(""balance"", *balanceCh) |
      for (@balance <- balanceCh) {{
        return!(balance)
      }}
    }}
  }}
}}
";

        public const string TRANSFER_RHO_TPL = @"
new rl(`rho:registry:lookup`), RevVaultCh, vaultCh, revVaultKeyCh, deployerId(`rho:rchain:deployerId`), stdout(`rho:io:stdout`), resultCh in {{
  rl!(`rho:rchain:revVault`, *RevVaultCh) |
  for (@(_, RevVault) <- RevVaultCh) {{
    @RevVault!(""findOrCreate"", ""{0}"", *vaultCh) |
    @RevVault!(""deployerAuthKey"", *deployerId, *revVaultKeyCh) |
    for (@(true, vault) <- vaultCh; key <- revVaultKeyCh) {{
      @vault!(""transfer"", ""{1}"", {2}, *key, *resultCh) |
      for (_ <- resultCh) {{ Nil }}
    }}
  }}
}}
";

        public const string TRANSFER_ENSURE_TO_RHO_TPL = @"
new rl(`rho:registry:lookup`), RevVaultCh, vaultCh, toVaultCh, deployerId(`rho:rchain:deployerId`), revVaultKeyCh, resultCh in {{
  rl!(`rho:rchain:revVault`, *RevVaultCh) |
  for (@(_, RevVault) <- RevVaultCh) {{
    @RevVault!(""findOrCreate"", ""{0}"", *vaultCh) |
    @RevVault!(""findOrCreate"", ""{1}"", *toVaultCh) |
    @RevVault!(""deployerAuthKey"", *deployerId, *revVaultKeyCh) |
    for (@(true, vault) <- vaultCh; key <- revVaultKeyCh; @(true, toVault) <- toVaultCh) {{
      @vault!(""transfer"", ""{1}"", {2}, *key, *resultCh) |
      for (_ <- resultCh) {{ Nil }}
    }}
  }}
}}
";

        // these are predefined param
        public const long TRANSFER_PHLO_LIMIT = 1000000;
        public const long TRANSFER_PHLO_PRICE = 1;    

        private RClient client;
        public VaultAPI(RClient client)
        {
            this.client = client;
        }

        public long GetBalance(string revAddr, string blockHash = "")
        {
            var contract = string.Format(GET_BALANCE_RHO_TPL, revAddr);
            var result = client.ExploratoryDeploy(contract, blockHash);
            return result[0].Exprs[0].GInt;
        }

        /// <summary>
        /// Transfer from `from_addr` to `to_addr` in the chain.Just make sure the `to_addr` is created
        /// in the chain. Otherwise, the transfer would hang until the `to_addr` is created.
        /// </summary>
        /// <param name="fromAddr"></param>
        /// <param name="toAddr"></param>
        /// <param name="amount"></param>
        /// <param name="key"></param>
        /// <param name="phloPrice"></param>
        /// <param name="phloLimit"></param>
        /// <returns></returns>
        public string Transfer(string fromAddr, string toAddr, long amount, PrivateKey key, 
            long phloPrice = TRANSFER_PHLO_PRICE, long phloLimit = TRANSFER_PHLO_LIMIT)
        {
            var contract = string.Format(TRANSFER_RHO_TPL, fromAddr, toAddr, amount);
            var timestamp_mill = Util.DateTimeToUtc(DateTime.Now.ToUniversalTime());
            return client.DeployWithVABNFilled(key, contract, phloPrice, phloLimit, timestamp_mill);
        }
        /// <summary>
        /// The difference between `transfer_ensure` and `transfer` is that , if the to_addr is not created in the
        /// chain, the `transfer` would hang until the to_addr successfully created in the change and the `transfer_ensure`
        /// can be sure that if the `to_addr` is not existed in the chain the process would created the vault in the chain
        /// and make the transfer successfully.
        /// </summary>
        /// <param name="fromAddr"></param>
        /// <param name="toAddr"></param>
        /// <param name="amount"></param>
        /// <param name="key"></param>
        /// <param name="phloPrice"></param>
        /// <param name="phloLimit"></param>
        /// <returns></returns>
        public string TransferEnsure(string fromAddr, string toAddr, long amount, PrivateKey key,
                    long phloPrice = TRANSFER_PHLO_PRICE,
                    long phloLimit = TRANSFER_PHLO_LIMIT)
        {
            var contract = string.Format(TRANSFER_ENSURE_TO_RHO_TPL, fromAddr, toAddr, amount);
            var timestamp_mill = Util.DateTimeToUtc(DateTime.Now.ToUniversalTime());
            return client.DeployWithVABNFilled(key, contract, phloPrice, phloLimit, timestamp_mill);
        }

        public string CreateVault(string addr,PrivateKey key,
            long phloPrice = TRANSFER_PHLO_PRICE, long phloLimit= TRANSFER_PHLO_LIMIT)
        {
            var contract = string.Format(CREATE_VAULT_RHO_TPL, addr);
            var timestamp_mill = Util.DateTimeToUtc(DateTime.Now.ToUniversalTime());
            return client.DeployWithVABNFilled(key, contract, phloPrice, phloLimit, timestamp_mill);
        }
    }
}
