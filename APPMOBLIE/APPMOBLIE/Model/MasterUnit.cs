using System;
using System.Collections.Generic;
using System.Text;

namespace APPMOBLIE.Model
{
    class MasterUnit
    {
        public int ProductUnitId { get; set; }
        public int CompanyId { get; set; }
        public string ProductUnitCode { get; set; }
        public string Name { get; set; }
    }

    class MasterLocation
    {
        public int LocationId { get; set; }
        public int CompanyId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
    }
}
