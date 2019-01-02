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
using System.Windows.Forms;

namespace TigerLookupDataChecker.Models.Factories
{
    public class TaxOfficeFetcher : LookupBaseFetcher
    {
        public override async Task<List<CompareFileLine>> FetchAndGetComparisonData(string fileName)
        {
            List<CompareFileLine> records = new List<CompareFileLine>();
            try
            {
                var newData = await GetTaxOfficeDataFromCloud();
                var oldData = GetDataFromInputFile(fileName);

                var i = 0;
                while (i <= oldData.TaxOffices.Count - 1)
                {
                    CompareFileLine currentNewRecord = new CompareFileLine();

                    var currentSourceDataRecord = oldData.TaxOffices.AsEnumerable().Skip(i).Take(1).FirstOrDefault();

                    if (currentSourceDataRecord != null)
                    {
                        var currentTargetDataRecord = newData.FirstOrDefault(f => f.Code == currentSourceDataRecord.Code);

                        bool hasDifference = false;

                        //eger hedef kayit yok ise eski kayittan buldum diyerek koyacak
                        if (currentTargetDataRecord == null)
                        {
                            currentTargetDataRecord = new TaxOffice();
                            currentTargetDataRecord = currentSourceDataRecord;
                        }

                        if (currentSourceDataRecord == null)
                            currentSourceDataRecord = new TaxOffice();



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

                foreach (var curRecord in newData)
                {
                    CompareFileLine currentNewRecord = new CompareFileLine();

                    currentNewRecord.SourceLineData = "";
                    currentNewRecord.TargetLineData = curRecord.ToString();
                    currentNewRecord.HasDifference = true;
                    currentNewRecord.TargetObject = curRecord;

                    currentNewRecord.IsNewRecord = currentNewRecord.HasDifference;

                    records.Add(currentNewRecord);
                }

                records = records.Where(f => f.TargetObject != null).OrderBy(f => f.TargetObject.Addr1).ThenBy(f => f.TargetObject.Code).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return records;
        }

        public TaxOfficeXml GetDataFromInputFile(string fileName)
        {
            TaxOfficeXml taxOfficeFile = new TaxOfficeXml();
            try
            {
                taxOfficeFile = Utils.DeSerialize<TaxOfficeXml>(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taxOfficeFile;
        }

        public async Task<List<dynamic>> GetTaxOfficeDataFromCloud()
        {
            List<dynamic> records = new List<dynamic>();
            List<string> exceptionItems = new List<string>()
            {
                "SIRA NO",
                "VERGİ DAİRESİ MÜDÜRLÜKLERİ",
                "İL",
                "İLÇE",
                "SAY.",
                "VERGİ DAİRESİ",
                "15.11.2017",
                "SAY. KODU"
            };
            try
            {
                //Make call and download excel

                var url = Constants.SourceLinks[Constants.TxOfXmlFileName];
                RestRequestParameter parameters = new RestRequestParameter(
                    url,
                    HttpMethod.Get)
                { IgnoreCertificateErrors = true };

                RestServiceClient client = new RestServiceClient();
                RestServiceCallResponse result = await client.CallRestServiceAsync<byte[]>(parameters);

                var pdfFilePath = Constants.Temp + "\\TaxOffice.pdf";
                var txtFilePath = Constants.Temp + "\\TaxOffice.txt";

                if (!System.IO.File.Exists(txtFilePath))
                {
                    System.IO.File.WriteAllText(txtFilePath, "");
                }

                if (result.HasSucceeded)
                {
                    System.IO.File.WriteAllBytes(pdfFilePath, (byte[])result.ResultObject);
                    Process.Start(Constants.Temp);
                    Process.Start(pdfFilePath);
                    Process.Start(txtFilePath);


                    if (MessageBox.Show("Please select all with adobe pdf reader (ctrl+a) then copy (ctrl+c) then paste that data to txt file (ctrl+v) then save txt file. Then click ok to continue?", "",
                        buttons: MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        // read data from csv file
                        var allLines = System.IO.File.ReadAllLines(txtFilePath, encoding: System.Text.Encoding.GetEncoding(Constants.FileEncoding));
                        //ilk satirda baslik var atlaniyor
                        for (var i = 0; i < allLines.Length; i = i + 5)
                        {
                            try
                            {
                                var skipLine = exceptionItems.Any(f => allLines[i].Contains(f));

                                if (skipLine)
                                {
                                    i = i + 1;
                                    continue;
                                }

                                var addrPart = allLines[i + 1].Split(' ')[1];
                                var codePart = allLines[i + 3];
                                var namePart = allLines[i + 4];

                                var newRecord = new TaxOffice()
                                {
                                    Code = codePart,
                                    Name = namePart,
                                    Addr1 = addrPart,
                                    CountryCode = "TR"
                                };

                                records.Add(newRecord);
                            }
                            catch (Exception ex)
                            {
                                //ignored
                            }
                        }
                    }

                    records = records.OrderBy(f => f.Addr1).ThenBy(f => f.Name).ToList();
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
                TaxOfficeXml taxOfficeXml = new TaxOfficeXml()
                {
                    TaxOffices = records.Where(f => f.TargetObject != null && f.IsNewRecord && !f.TargetObject.IsEmpty()).Select(f => (TaxOffice)f.TargetObject).ToList()
                };

                var file = Constants.OutputsFolder + "\\" + targetFile.Replace(".XML", "_NEWS.txt");

                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);

                System.IO.File.AppendAllLines(file, taxOfficeXml.TaxOffices.Select(f => f.ToXml()));
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
