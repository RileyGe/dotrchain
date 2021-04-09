using System;
using System.Collections.Generic;
using System.Text;

namespace dotrchain
{
    public class DeployData
    {
        public string Term;
        public long PhloPrice;
        public long PhloLimit;
        public long ValidAfterBlockNo = -1;
        public long TimestampMillis = -1;
        public DeployData(string term, long price = 1, long limit = 1000, long validAfterBlockNo = -1, long timestampMillis = -1)
        {
            Term = term;
            PhloPrice = price;
            PhloLimit = limit;
            ValidAfterBlockNo = validAfterBlockNo;
            TimestampMillis = timestampMillis;
        }

        public void SetTimestamp(DateTime dt)
        {
            var startTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            TimestampMillis = (long)(dt - startTime).TotalMilliseconds;
        }
    }
}
