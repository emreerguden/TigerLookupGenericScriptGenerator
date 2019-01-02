using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerLookupDataChecker.Models.Factories.Base
{
    public class LookupFetcherFactory
    {
        public static LookupBaseFetcher GetFetcher(string fileName)
        {
            switch (fileName)
            {
                case Constants.CityXmlFileName:
                    return new CityFetcher();
                case Constants.TownXmlFileName:
                    return new TownFetcher();
                case Constants.TxOfXmlFileName:
                    return new TaxOfficeFetcher();
                default:
                    return new CityFetcher();
            }
        }
    }

    public abstract class LookupBaseFetcher
    {
        public abstract Task<List<CompareFileLine>> FetchAndGetComparisonData(string fileName);
        public abstract  void SaveNewDataToOutputFile(string targetFile, List<CompareFileLine> records, bool openTargetDirectory);

        public abstract dynamic GetInputFileData(string fileName);
    }
}
