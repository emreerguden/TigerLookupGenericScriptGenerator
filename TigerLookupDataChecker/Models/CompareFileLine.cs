using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerLookupDataChecker.Models
{
    public class CompareFileLine
    {
        public bool HasDifference { get; set; }
        public string SourceLineData { get; set; }
        public string TargetLineData { get; set; }

        public bool IsNewRecord { get; set; }

        public dynamic TargetObject { get; set; }
        
    }
}
