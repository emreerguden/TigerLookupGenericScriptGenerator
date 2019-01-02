using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerLookupDataChecker.Models
{
    public class Firm
    {
        public bool IsSelected { get; set; }
        public string FirmNr { get; set; }
        public string FirmName { get; set; }
        public string Period { get; set; }
    }
}
