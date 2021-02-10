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
        public string ProductCode { get; set; }
    }

    public class TransactionInOut
    {
        public int TransactionId { get; set; }
        public string Ref { get; set; }
        public string CreateDate { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string Locations { get; set; }
        public string Username { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }

    }

    public class AllTransactions
    {
        public string Ref { get; set; }
        public string CreateDate { get; set; }
        public string Amount { get; set; }
        public string Locations { get; set; }
        public string Username { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
    }
}
