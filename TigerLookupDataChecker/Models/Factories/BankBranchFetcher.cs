using System;
using TigerLookupDataChecker.Models.Factories.Base;
using TigerLookupDataChecker.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TigerLookupDataChecker.Models.Factories
{
    public class BankBranchFetcher : LookupBaseFetcher
    {

        public override dynamic GetInputFileData(string fileName)
        {
            return GetDataFromInputFile(fileName);
        }

        public BankBranchXml GetDataFromInputFile(string fileName)
        {
            BankBranchXml bankBranches = new BankBranchXml();
            try
            {
                bankBranches = Utils.DeSerialize<BankBranchXml>(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bankBranches;
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