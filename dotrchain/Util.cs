﻿using System;
using Casper;
using Google.Protobuf;

namespace dotrchain
{
    public class Util
    {
        public static DeployDataProto SignDeploy(PrivateKey key, string term, long phloPrice = 1, long phloLimit = 1000, 
            long validAfterBlockNo = -1, long timestampMillis = -1)
        {
            if (timestampMillis < 0) timestampMillis = DateTimeToUtc(DateTime.Now);            

            var data = new DeployDataProto
            {                
                Deployer = ByteString.CopyFrom(key.PublicKey.Bytes),
                Term = term,
                PhloPrice = phloPrice,
                PhloLimit = phloLimit,
                ValidAfterBlockNumber = validAfterBlockNo,
                Timestamp = timestampMillis,
                SigAlgorithm = "secp256k1",                
            };
            data.Sig = ByteString.CopyFrom(key.Sign(GenDeployDataForSig(data)));
            return data;
        }
        private static byte[] GenDeployDataForSig(DeployDataProto deployData)
        {
            var data = new DeployDataProto()
            {
                Term = deployData.Term,
                Timestamp = deployData.Timestamp,
                PhloLimit = deployData.PhloLimit,
                PhloPrice = deployData.PhloPrice,
                ValidAfterBlockNumber = deployData.ValidAfterBlockNumber,
            };                   
            return data.ToByteArray();    
        }
        public static bool VerifyDeploy(PublicKey key, byte[] sig, DeployDataProto data)
        {
            return key.Verify(sig, GenDeployDataForSig(data));
        }

        public static long DateTimeToUtc(DateTime dt)
        {
            var startTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            return (long)(dt - startTime).TotalMilliseconds;
        }
    }
}
