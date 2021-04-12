using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Casper.V1;
using Casper;
using System.Linq;
using System.Text.RegularExpressions;

namespace dotrchain
{
    public class RClientException : Exception
    {
        public RClientException(string message) : base(message) { }
    }
    //    public class DataQueries
    //    {

    //    public static Par public_names(List<string> names)
    //        {
    //exprs = [Expr(g_string = n) for n in names]
    //        return Par(exprs= exprs)
    //        }


    //    public Par deploy_id(string deploy_id)
    //        {
    //            var g_deploy_id = new GDeployId()
    //            {
    //                Sig = Google.Protobuf.ByteString.CopyFrom(Util.HexToBytes(deploy_id))
    //            };
    //            var g_unforgeable = new GUnforgeable()
    //            {
    //                GDeployIdBody = g_deploy_id
    //            };
    //            var gun = new Google.Protobuf.Collections.RepeatedField<GUnforgeable>
    //            {
    //                g_unforgeable
    //            };

    //            var ppp = new Par()
    //            {
    //                Unforgeables = gun
    //            };
    //            return ppp;
    //        }

    //    }


    public class Transaction
    {
        public string from_addr;
        public string to_addr;
        public long amount;
        public Par ret_unforgeable;
        public bool success; //: Optional[Tuple[bool, str]]
        public string reason;
        //public DeployInfo deploy;
    }

    public class DeployWithTransaction
    {
        public DeployInfo deploy_info;
        public List<Transaction> transactions;
    }
    public class RClient
    {
        //private Regex propose_result_match = new Regex(@"Success! Block (?<block_hash>[0-9a-f]+) created and added.");
        private readonly Channel channel;
        private readonly DeployService.DeployServiceClient client;
        private Par param;

        public RClient(string host, int port, IEnumerable<ChannelOption> options = null)
        {
            if (options is null) options = new ChannelOption[] { };
            channel = new Channel(host, port, ChannelCredentials.Insecure, options);
            client = new DeployService.DeployServiceClient(channel);
        }

        public void ConfigParam(Par netParam)
        {
            this.param = netParam;
        }

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

        public LightBlockInfo FindDeploy(string deployId)
        {
            var query = new FindDeployQuery()
            {
                DeployId = Google.Protobuf.ByteString.CopyFrom(Util.HexToBytes(deployId))
            };
            var response = client.findDeploy(query);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            return response.BlockInfo;
        }

        public string Deploy(PrivateKey key, string term, long phloPrice, long phloLimit,
            long validAfterBlockNo = -1, long timestampMillis = -1)
        {
            var deployData = Util.SignDeploy(key, term, phloPrice, phloLimit, validAfterBlockNo, timestampMillis);
            return SendDeploy(deployData);
        }

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

        public string DeployWithVabnFilled(PrivateKey key, string term, long phloPrice, long phloLimit, long timestampMillis = -1)
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
        public EventInfoResponse get_event_data(string block_hash)
        {
            var query = new BlockQuery()
            {
                Hash = block_hash
            };
            var response = client.getEventByHash(query);
            if (response.Error != null)
            {
                throw new RClientException(response.Error.Messages.ToString());
            }
            return response;
        }
        public List<DeployWithTransaction> get_transaction(string block_hash)
        {
            if (param is null)
                throw new RClientException("You haven't install your network param.");
            var transactions = new List<DeployWithTransaction>();
            var event_data = get_event_data(block_hash);
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
                    //var precharge = deploy.Report[0];
                    var user = deploy.Report[1];
                    //var refund = deploy.Report[2];
                    //var report = new Report(precharge, user, refund);
                    transactions.Add(new DeployWithTransaction()
                    {
                        deploy_info = deploy.DeployInfo,
                        transactions = FindTransferComm(user, this.param)
                    });
                    //transactions.Add(findTransferComm(deploy.DeployInfo, user, this.transferTem));                        
                }

            }
            return transactions;
        }
        private List<Transaction> FindTransferComm(SingleReport report, Par transferTemplateUnforgeable)
        {
            var transfers = new List<ReportProto>();
            var transactions = new List<Transaction>();
            foreach (var item in report.Events)
            //report.eventsList.forEach((event) => 
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
                        //event.comm.producesList [0].data.parsList [3].exprsList [0].gInt;

                        var ret = item.Comm.Produces[0].Data.Pars[5];
                        transactions.Add(new Transaction()
                        {
                            from_addr = fromAddr,
                            to_addr = toAddr,
                            amount = amount,
                            ret_unforgeable = ret,
                            //deploy = deploy
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
                            transaction.success = result;
                            transaction.reason = reason;
                            walletCreated = true;
                        }
                    }
                }
                if (walletCreated == false)
                {
                    transaction.success = true;
                    transaction.reason = "Possibly the transfer toAddr wallet is not created in chain. Create the wallet to make transaction succeed.";
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
        public void Close()
        {
            channel.ShutdownAsync();
        }
    }
}
