using System;
using System.Collections.Generic;
using System.Text;

namespace dotrchain
{
    /// <summary>
    /// These parameter are pre defined params which can help access the data quicklier.
    /// It is not the best way to solve the problem right now.But it is the fast way.
    /// </summary>
    public static class Params
    {
        public static Par TestnetParam = GetTestnetParam();
        private static Par GetTestnetParam()
        {
            var param = new Par();
            var testnet_transfer_unforgeable_id = "72d0f333c719323406901bca34c2935e4d92c31402fa80a2c273422e923af550";
            GUnforgeable gun = new GUnforgeable
            {
                GPrivateBody = new GPrivate()
                {
                    Id = Google.Protobuf.ByteString.CopyFrom(Util.HexToBytes(testnet_transfer_unforgeable_id))
                }
            };
            param.Unforgeables.Add(gun);
            return param;
        }

        public static Par MainnetParam = GetMainnetParam();

        private static Par GetMainnetParam()
        {
            var param = new Par();
            var testnet_transfer_unforgeable_id = "72d0f333c719323406901bca34c2935e4d92c31402fa80a2c273422e923af550";
            GUnforgeable gun = new GUnforgeable
            {
                GPrivateBody = new GPrivate()
                {
                    Id = Google.Protobuf.ByteString.CopyFrom(Util.HexToBytes(testnet_transfer_unforgeable_id))
                }
            };
            param.Unforgeables.Add(gun);
            return param;
        }
    }
}
