using Grpc.Core;
using System;
using System.Collections.Generic;
using Casper.V1;
using Casper;
using System.Linq;
using Google.Protobuf;

namespace dotrchain
{
    /// <summary>
    /// 
    /// </summary>
    public class RClientException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public RClientException(string message) : base(message) { }
    }  

    /// <summary>
    /// 
    /// </summary>
    public class Transaction
    {
        public string FromAddress;
        public string ToAddress;
        public long Amount;
        public Par ret_unforgeable;
        public bool Success; //: Optional[Tuple[bool, str]]
        public string Reason;
        //public DeployInfo deploy;
    }

    public class DeployWithTransaction
    {
        public DeployInfo DeployInfo;
        public List<Transaction> Transactions;
    }
    public class RClient
    {
        //private Regex propose_result_match = new Regex(@"Success! Block (?<block_hash>[0-9a-f]+) created and added.");
        private readonly Channel channel;
        private readonly DeployService.DeployServiceClient client;
        private Par param;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="options"></param>
        public RClient(string host, int port, IEnumerable<ChannelOption> options = null)
        {
            if (options is null) options = new ChannelOption[] { };
            channel = new Channel(host, port, ChannelCredentials.Insecure, options);
            client = new DeployService.DeployServiceClient(channel);
        }
        /// <summary>
        /// config the net parameter(mainnet or testnet)
        /// </summary>
        /// <param name="netParam">net parameter(main net or testnet)</param>
        public void ConfigParam(Par netParam)
        {
            param = netParam;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depth"></param>
        /// <returns></returns>
        public List<BlockInfoResponse> ShowBlocks(int depth)
        {
            var blocksQuery = new BlocksQuery
            {
                Depth = depth
            };
            var response = client.getBlocks(blocksQuery);
            var result = HandleStream(response);
            return result.ConvertAll(item => item as BlockInfoResponse);
        }
        /// <summary>
        /// get the block information using block hash
        /// </summary>
        /// <param name="blockHash">block hash</param>
        /// <returns>block information</returns>
        public BlockInfo ShowBlock(string blockHash)
        {
            var blockQuery = new BlockQuery()
            {
                Hash = blockHash
            };
            var response = client.getBlock(blockQuery);
            //CheckResponse(response);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            return response.BlockInfo;
        }
        /// <summary>
        /// get the last finalized block information
        /// </summary>
        /// <returns>the last finalized block information</returns>
        public BlockInfo LastFinalizedBlock()
        {
            var query = new LastFinalizedBlockQuery();
            LastFinalizedBlockResponse response = client.lastFinalizedBlock(query);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            //CheckResponse(response);
            return response.BlockInfo;
        }
        /// <summary>
        /// the block is finalized or not
        /// </summary>
        /// <param name="blockHash">block hash</param>
        /// <returns>return true if is finalized, else return false</returns>
        public bool IsFinalized(string blockHash)
        {
            var query = new IsFinalizedQuery()
            {
                Hash = blockHash
            };
            var response = client.isFinalized(query);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            //self._check_response(response)
            return response.IsFinalized;
        }
        /// <summary>
        /// get blocks information by heights
        /// </summary>
        /// <param name="startBlockNumber">start block number</param>
        /// <param name="endBlockNumber">end block number</param>
        /// <returns>this list of block information</returns>
        public List<BlockInfoResponse> GetBlocksByHeights(long startBlockNumber, long endBlockNumber)
        {
            var query = new BlocksQueryByHeight()
            {
                StartBlockNumber = startBlockNumber,
                EndBlockNumber = endBlockNumber
            };
            var response = client.getBlocksByHeights(query);
            var result = HandleStream(response);
            return result.ConvertAll(item => item as BlockInfoResponse);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <param name="blockHash"></param>
        /// <param name="usePreStateHash"></param>
        /// <returns></returns>
        public List<Par> ExploratoryDeploy(string term, string blockHash, bool usePreStateHash = false)
        {
            var query = new ExploratoryDeployQuery()
            {
                Term = term,
                BlockHash = blockHash,
                UsePreStateHash = usePreStateHash
            };
            var response = client.exploratoryDeploy(query);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            return response.Result.PostBlockData.AsEnumerable().ToList();
        }
        /// <summary>
        /// find deploy information by deploy id
        /// </summary>
        /// <param name="deployId">deploy id</param>
        /// <returns>the deploy information</returns>
        public LightBlockInfo FindDeploy(string deployId)
        {
            var query = new FindDeployQuery()
            {
                DeployId = ByteString.CopyFrom(Util.HexToBytes(deployId))
            };            
            var response = client.findDeploy(query);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            return response.BlockInfo;
        }
        /// <summary>
        /// deploy a contract to rchain
        /// </summary>
        /// <param name="key">private key</param>
        /// <param name="term">the contract</param>
        /// <param name="phloPrice">phlo price</param>
        /// <param name="phloLimit">phlo limit</param>
        /// <param name="validAfterBlockNo">valid after block number</param>
        /// <param name="timestampMillis">timestamp(using for sign the deploy data)</param>
        /// <returns>the deploy id</returns>
        public string Deploy(PrivateKey key, string term, long phloPrice, long phloLimit,
            long validAfterBlockNo = -1, long timestampMillis = -1)
        {
            var deployData = Util.SignDeploy(key, term, phloPrice, phloLimit, validAfterBlockNo, timestampMillis);
            return SendDeploy(deployData);
        }
        /// <summary>
        /// send deploy
        /// </summary>
        /// <param name="deploy">deploy data(you can get the deploy data using Util.SignDeploy)</param>
        /// <returns>the deploy id</returns>
        public string SendDeploy(DeployDataProto deploy)
        {
            var response = client.doDeploy(deploy);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            //sig of deploy data is deployId
            return Util.BytesToHex(deploy.Sig.ToByteArray());
        }
        /// <summary>
        /// deploy a contract to rchain with the valid after block number filled
        /// </summary>
        /// <param name="key">private key</param>
        /// <param name="term">the contract</param>
        /// <param name="phloPrice">phlo price</param>
        /// <param name="phloLimit">phlo limit</param>
        /// <param name="timestampMillis">timestamp(using for sign the deploy data)</param>
        /// <returns>the deploy id</returns>
        public string DeployWithVABNFilled(PrivateKey key, string term, long phloPrice, long phloLimit, long timestampMillis = -1)
        {
            var latestBlocks = ShowBlocks(1);
            // when the genesis block is not ready, it would be empty in show_blocks
            // it could return more than 1 block when there are multiple blocks at the same height
            if (latestBlocks.Count < 1)
            {
                throw new RClientException("No latest block found");
            }
            var latestBlock = latestBlocks[0];
            var latestBlockNum = latestBlock.BlockInfo.BlockNumber;
            return Deploy(key, term, phloPrice, phloLimit, latestBlockNum, timestampMillis);
        }
        /// <summary>
        /// get event information
        /// </summary>
        /// <param name="blockHash">block hash</param>
        /// <returns>the event information</returns>
        public EventInfoResponse GetEventData(string blockHash)
        {
            var query = new BlockQuery()
            {
                Hash = blockHash
            };
            var response = client.getEventByHash(query);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            return response;
        }
        /// <summary>
        /// get transactions by block hash
        /// </summary>
        /// <param name="blockHash">block hash</param>
        /// <returns>the DeployWithTransaction list</returns>
        public List<DeployWithTransaction> GetTransactions(string blockHash)
        {
            if (param is null)
                throw new RClientException("You haven't install your network param.");
            var transactions = new List<DeployWithTransaction>();
            var event_data = GetEventData(blockHash);
            var deploys = event_data.Result.Deploys;
            foreach (var deploy in deploys)
            {
                // it is possible that the user deploy doesn't generate
                // any comm events . So there are only two report in the response.
                if (deploy.Report.Count == 2)
                {
                    continue;
                }
                // normally there are precharge, user and refund deploy, 3 totally.
                else if (deploy.Report.Count == 3)
                {
                    var user = deploy.Report[1];
                    transactions.Add(new DeployWithTransaction()
                    {
                        DeployInfo = deploy.DeployInfo,
                        Transactions = FindTransferComm(user, this.param)
                    });
                }

            }
            return transactions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="transferTemplateUnforgeable"></param>
        /// <returns></returns>
        private List<Transaction> FindTransferComm(SingleReport report, Par transferTemplateUnforgeable)
        {
            var transfers = new List<ReportProto>();
            var transactions = new List<Transaction>();
            foreach (var item in report.Events)
            {
                if (item.Comm != null)
                {
                    var channel = item.Comm.Consume.Channels[0];
                    if (channel.Unforgeables.Count > 0 && channel.Unforgeables[0].GPrivateBody != null &&
                        channel.Unforgeables[0].GPrivateBody.Id == transferTemplateUnforgeable.Unforgeables[0].GPrivateBody.Id)
                    {
                        transfers.Add(item);
                        var fromAddr = item.Comm.Produces[0].Data.Pars[0].Exprs[0].GString;
                        var toAddr = item.Comm.Produces[0].Data.Pars[2].Exprs[0].GString;
                        var amount = item.Comm.Produces[0].Data.Pars[3].Exprs[0].GInt;

                        var ret = item.Comm.Produces[0].Data.Pars[5];
                        transactions.Add(new Transaction()
                        {
                            FromAddress = fromAddr,
                            ToAddress = toAddr,
                            Amount = amount,
                            ret_unforgeable = ret,
                        });
                    }
                }
            }

            transactions.ForEach((transaction) => {
                bool walletCreated = false;
                foreach (var item in report.Events)
                {
                    if (item.Produce != null)
                    {
                        var channel = item.Produce.Channel;
                        if (channel.Unforgeables.Count > 0 &&
                          channel.Unforgeables[0].GPrivateBody != null &&
                          channel.Unforgeables[0].GPrivateBody.Id == transaction.ret_unforgeable.Unforgeables[0].GPrivateBody.Id)
                        {
                            var data = item.Produce.Data;
                            var result = data.Pars[0].Exprs[0].ETupleBody.Ps[0].Exprs[0].GBool;
                            var reason = result ? "" : data.Pars[0].Exprs[0].ETupleBody.Ps[1].Exprs[0].GString;
                            transaction.Success = result;
                            transaction.Reason = reason;
                            walletCreated = true;
                        }
                    }
                }
                if (walletCreated == false)
                {
                    transaction.Success = true;
                    transaction.Reason = "Possibly the transfer toAddr wallet is not created in chain. Create the wallet to make transaction succeed.";
                }
            });
            return transactions;
        }
        private List<object> HandleStream(AsyncServerStreamingCall<BlockInfoResponse> response)
        {
            List<object> list = new List<object>();
            var stream = response.ResponseStream;

            while (true)
            {
                var next = stream.MoveNext();
                next.Wait();
                if (next.Result)
                    list.Add(stream.Current);
                else
                    break;
            }
            return list;
        }
        /// <summary>
        /// close the client
        /// </summary>
        public void Close()
        {
            channel.ShutdownAsync();
        }
    }
}
