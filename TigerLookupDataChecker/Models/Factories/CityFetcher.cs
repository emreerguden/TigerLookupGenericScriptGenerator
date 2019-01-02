using NAF.Common.Utils.Extensions;
using NAF.Platform.Services.Client.Rest;
using NAF.Platform.Services.Client.Rest.Request;
using NAF.Platform.Services.Client.Rest.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TigerLookupDataChecker.Models.Factories.Base;
using TigerLookupDataChecker.Helpers;
using System.Diagnostics;

namespace TigerLookupDataChecker.Models.Factories
{
    public class CityFetcher: LookupBaseFetcher
    {
        public override async Task<List<CompareFileLine>> FetchAndGetComparisonData(string fileName)
        {
            List<CompareFileLine> records = new List<CompareFileLine>();
            try
            {
                var newData = await GetCityDataFromCloud(); 
                var oldData = GetDataFromInputFile(fileName);

                var i = 0;
                while (i < newData.Count - 1 || i <= oldData.Cities.Count - 1)
                {
                    CompareFileLine currentNewRecord = new CompareFileLine();

                    var currentSourceDataRecord = oldData.Cities.AsEnumerable().Skip(i).Take(1).FirstOrDefault();


                    var currentTargetDataRecord = newData.AsEnumerable().Skip(i).Take(1).FirstOrDefault();

                    if (currentTargetDataRecord == null)
                        currentTargetDataRecord = new City();

                    if (currentSourceDataRecord == null)
                        currentSourceDataRecord = new City();

                    currentNewRecord.SourceLineData = currentSourceDataRecord.ToString();
                    currentNewRecord.TargetLineData = currentTargetDataRecord.ToString();
                    currentNewRecord.HasDifference = !currentSourceDataRecord.Equals(currentTargetDataRecord);
                    currentNewRecord.TargetObject = currentTargetDataRecord;
                    currentNewRecord.IsNewRecord = currentNewRecord.HasDifference;
                    records.Add(currentNewRecord);

                    i++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return records;
        }

        public CityXml GetDataFromInputFile(string fileName)
        {
            CityXml cityFile = new CityXml();
            try
            {
                cityFile = Utils.DeSerialize<CityXml>(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cityFile;
        }

        public async Task<List<dynamic>> GetCityDataFromCloud()
        {
            List<dynamic> records = new List<dynamic>();

            try
            {
                //Make call and download excel

                var url = Constants.SourceLinks[Constants.CityXmlFileName];
                RestRequestParameter parameters = new RestRequestParameter(
                    url,
                    HttpMethod.Get)
                { IgnoreCertificateErrors = true };

                RestServiceClient client = new RestServiceClient();
                RestServiceCallResponse result = await client.CallRestServiceAsync<byte[]>(parameters);

                var cityFilePath = Constants.Temp + "\\CityXls.xls";
                var cityCsvFilePath = Constants.Temp + "\\City.csv";

                if (result.HasSucceeded)
                {
                    //convert xls to csv
                    System.IO.File.WriteAllBytes(cityFilePath, (byte[])result.ResultObject);

                    var fullPathOrgFileXls = System.IO.Path.GetFullPath(cityFilePath);
                    var fullPathCsvFileXls = System.IO.Path.GetFullPath(cityCsvFilePath);

                    if (!Utils.ConvertXlsToCsv(fullPathOrgFileXls, fullPathCsvFileXls))
                    {
                        throw new Exception("Could not export cityXls.xls to csv file please check if excel exists or permissions");
                    }


                    // read data from csv file
                    var allLines = System.IO.File.ReadAllLines(fullPathCsvFileXls, encoding: System.Text.Encoding.GetEncoding(Constants.FileEncoding));
                    //ilk satirda baslik var atlaniyor
                    foreach (var currentLine in allLines.Skip(1))
                    {
                        try
                        {
                            if (currentLine.Assigned())
                            {
                                var parts = currentLine.Split(';');
                                var newRecord = new City()
                                {
                                    Code = parts[0].ToString().PadLeft(2, '0'),
                                    Name = parts[1].ToString(),
                                    CountryCode = "TR"
                                };

                                if (!records.Any(f => f.Code == newRecord.Code) && !newRecord.IsEmpty())
                                    records.Add(newRecord);
                            }
                        }
                        catch (Exception ex)
                        {
                            //ignored
                        }
                    }
                }
                else
                {
                    throw new Exception("Error connecting to " + url + " please check url for xls file");
                }
            }
            catch (Exception ex)
            {
                //TODO log
                throw ex;
            }

            return records;
        }

        public override void SaveNewDataToOutputFile(string targetFile, List<CompareFileLine> records, bool openTargetDirectory)
        {
            try
            {
                CityXml cityXml = new CityXml()
                {
                    Cities = records.Where(f => f.TargetObject != null && f.IsNewRecord && !f.TargetObject.IsEmpty()).Select(f => (City)f.TargetObject).ToList()
                };

                var file = Constants.OutputsFolder + "\\" + targetFile.Replace(".XML", "_NEWS.txt");

                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                System.IO.File.AppendAllLines(file, cityXml.Cities.Select(f => f.ToXml()));
                Process.Start(file);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override dynamic GetInputFileData(string fileName)
        {
            return GetDataFromInputFile(fileName);
        }
    }
}
