using NAF.Common.Utils.Extensions;
using NAF.Platform.Services.Client.Rest;
using NAF.Platform.Services.Client.Rest.Request;
using NAF.Platform.Services.Client.Rest.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TigerLookupDataChecker.Models.Factories.Base;
using TigerLookupDataChecker.Helpers;
using System.Net.Http;
using System.Diagnostics;

namespace TigerLookupDataChecker.Models.Factories
{
    public class TownFetcher : LookupBaseFetcher
    {
        public override async Task<List<CompareFileLine>> FetchAndGetComparisonData(string fileName)
        {
            List<CompareFileLine> records = new List<CompareFileLine>();
            try
            {
                var newData = await GetTownDataFromCloud();
                var oldData = GetDataFromInputFile(fileName);

                var i = 0;
                while (i <= oldData.Towns.Count - 1)
                {
                    CompareFileLine currentNewRecord = new CompareFileLine();

                    var currentSourceDataRecord = oldData.Towns.AsEnumerable().Skip(i).Take(1).FirstOrDefault();

                    if (currentSourceDataRecord != null)
                    {
                        var currentTargetDataRecord = newData.FirstOrDefault(f => f.CityCode == currentSourceDataRecord.CityCode &&
                                                                    (f.Name.ToLower().ToUpper() == currentSourceDataRecord.Name.ToLower().ToUpper() ||
                                                                      currentSourceDataRecord.Name.ToLower().ToUpper().Contains(f.Name.ToLower().ToUpper()) ||
                                                                      f.Name.ToLower().ToUpper().Contains(currentSourceDataRecord.Name.ToLower().ToUpper()) 
                                                                    )
                                                                    );

                        bool hasDifference = false;

                        //eger hedef kayit yok ise eski kayittan buldum diyerek koyacak
                        if (currentTargetDataRecord == null)
                        {
                            currentTargetDataRecord = new Town();

                            currentTargetDataRecord = currentSourceDataRecord;
                        }

                        if (currentSourceDataRecord == null)
                            currentSourceDataRecord = new Town();



                        currentNewRecord.SourceLineData = currentSourceDataRecord.ToString();
                        currentNewRecord.TargetLineData = currentTargetDataRecord.ToString();
                        currentNewRecord.HasDifference = hasDifference;
                        currentNewRecord.TargetObject = currentTargetDataRecord;


                        currentNewRecord.IsNewRecord = currentNewRecord.HasDifference;

                        records.Add(currentNewRecord);

                        if (currentTargetDataRecord != null)
                            newData.Remove(currentTargetDataRecord);
                    }
                    i++;
                }

                foreach(var curRecord in newData)
                {
                    CompareFileLine currentNewRecord = new CompareFileLine();

                    currentNewRecord.SourceLineData = "";
                    currentNewRecord.TargetLineData = curRecord.ToString();
                    currentNewRecord.HasDifference = true;
                    currentNewRecord.TargetObject = curRecord;
                    currentNewRecord.IsNewRecord = currentNewRecord.HasDifference;

                    records.Add(currentNewRecord);
                }

                records = records.Where(f => f.TargetObject != null).OrderBy(f => f.TargetObject.CityCode).ThenBy(f => f.TargetObject.Name).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return records;
        }

        public TownXml GetDataFromInputFile(string fileName)
        {
            TownXml townFile = new TownXml();
            try
            {
                townFile = Utils.DeSerialize<TownXml>(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return townFile;
        }

        public async Task<List<dynamic>> GetTownDataFromCloud()
        {
            List<dynamic> records = new List<dynamic>();

            try
            {
                //Make call and download excel

                var url = Constants.SourceLinks[Constants.TownXmlFileName];
                RestRequestParameter parameters = new RestRequestParameter(
                    url,
                    HttpMethod.Get)
                { IgnoreCertificateErrors = true };

                RestServiceClient client = new RestServiceClient();
                RestServiceCallResponse result = await client.CallRestServiceAsync<byte[]>(parameters);

                var xlsFilePath = Constants.Temp + "\\TownXls.xls";
                var csvFilePath = Constants.Temp + "\\Town.csv";

                if (result.HasSucceeded)
                {
                    //convert xls to csv
                    System.IO.File.WriteAllBytes(xlsFilePath, (byte[])result.ResultObject);

                    var fullPathOrgFileXls = System.IO.Path.GetFullPath(xlsFilePath);
                    var fullPathCsvFileXls = System.IO.Path.GetFullPath(csvFilePath);

                    if (!Utils.ConvertXlsToCsv(fullPathOrgFileXls, fullPathCsvFileXls))
                    {
                        throw new Exception(string.Format("Could not export {0} to csv file please check if excel exists or permissions", xlsFilePath));
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
                                if (parts[4] == "ETKİN DEĞİL")
                                {
                                    continue;
                                }
                                var newRecord = new Town()
                                {
                                    CityCode = parts[0].ToString().PadLeft(2, '0'),
                                    Name = parts[3].ToString(),
                                    CountryCode = "TR"
                                };

                                if (!records.Any(f => f.CityCode == newRecord.CityCode && f.Name == newRecord.Name) && !newRecord.IsEmpty())
                                    records.Add(newRecord);
                            }
                        }
                        catch (Exception ex)
                        {
                            //ignored
                        }
                    }

                    records = records.OrderBy(f => f.CityCode).ThenBy(f => f.Name).ToList();
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
                TownXml townXml = new TownXml()
                {
                    Towns = records.Where(f => f.TargetObject != null && f.IsNewRecord && !f.TargetObject.IsEmpty()).Select(f => (Town)f.TargetObject).ToList()
                };

                var file = Constants.OutputsFolder + "\\" + targetFile.Replace(".XML", "_NEWS.txt");

                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                System.IO.File.AppendAllLines(file, townXml.Towns.Select(f => f.ToXml()));
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
