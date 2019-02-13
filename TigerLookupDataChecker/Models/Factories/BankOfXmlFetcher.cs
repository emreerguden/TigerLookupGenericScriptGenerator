using System;
using TigerLookupDataChecker.Models.Factories.Base;
using TigerLookupDataChecker.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TigerLookupDataChecker.Models.Factories
{
    public class BankOfXmlFetcher : LookupBaseFetcher
    {
        public override dynamic GetInputFileData(string fileName)
        {
            return GetDataFromInputFile(fileName);
        }

        public BankXml GetDataFromInputFile(string fileName)
        {
            BankXml banks = new BankXml();
            try
            {
                banks = Utils.DeSerialize<BankXml>(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return banks;
        }

        public override Task<List<CompareFileLine>> FetchAndGetComparisonData(string fileName)
        {
            throw new NotImplementedException();
        }

        public override void SaveNewDataToOutputFile(string targetFile, List<CompareFileLine> records, bool openTargetDirectory)
        {
            throw new NotImplementedException();
        }
    }
}