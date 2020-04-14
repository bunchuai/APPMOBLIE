using System;
using System.Collections.Generic;
using System.Text;

namespace APPMOBLIE.Model
{
    class Transaction
    {
        public int TransectionId { get; set; }
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public string ReferentNumber { get; set; }
        public string TransectionType { get; set; }
        public DateTime TransectionDate { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
