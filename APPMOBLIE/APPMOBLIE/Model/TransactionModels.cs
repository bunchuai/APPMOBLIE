using System;
using System.Collections.Generic;
using System.Text;

namespace APPMOBLIE.Model
{
   public class TransactionModels
    {
        public string reference { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDate { get; set; }
        public int Quantity { get; set; }
        public string Username { get; set; }
        public string Unit { get; set; }
        public string Location { get; set; }
        public string Color { get; set; }
        public string ProductCode { get; set; }
    }

}
