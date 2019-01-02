using NAF.Common.Utils.Extensions;
using NAF.Common.Utils.Serialization;
using NAF.Platform.Services.Client.Rest;
using NAF.Platform.Services.Client.Rest.Request;
using NAF.Platform.Services.Client.Rest.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TigerLookupDataChecker.Models;
using TigerLookupDataChecker.Models.Factories.Base;

namespace TigerLookupDataChecker.Helpers
{
    public static class LookupFetchHelper
    {

        public static  async Task<List<CompareFileLine>> FetchAndGetComparisonData(string fileName)
        {
            List<CompareFileLine> records = null;
            try
            {
                var fetcher = LookupFetcherFactory.GetFetcher(fileName);
                records  = await fetcher.FetchAndGetComparisonData(fileName);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return records;
        }

        public static void SaveNewDataToOutputFile(string targetFile, List<CompareFileLine> records, bool openTargetDirectory)
        {
            try
            {
                var fetcher = LookupFetcherFactory.GetFetcher(targetFile);
                fetcher.SaveNewDataToOutputFile(targetFile, records, openTargetDirectory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Simple Helpers
        public static string GetSourceAndTargetLabels(string fileName)
        {
            if (fileName.NotAssigned())
                return string.Empty;

            return 
                string.Format("\n\nNewRecordsFetch: Source Xml Generation From Link \n{0} \nSaved as {1} In Outputs", Constants.SourceLinks[fileName], fileName) +
              string.Format("\n\nOldRecordFetch: Current Tiger Xml From File {0} In Inputs", fileName);
        }

        #endregion
        
    }
}
