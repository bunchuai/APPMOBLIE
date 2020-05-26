using System;
using System.Collections.Generic;
using System.Text;

namespace APPMOBLIE.Model
{
        public class TransactionLows
        {
        public string SkuId { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
        }

    public class TransactionInOut
    {
        public string Ref { get; set; }
        public string CreateDate { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string Locations { get; set; }
        public string Username { get; set; }
    }
}
