using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TigerLookupDataChecker.Models;
using TigerLookupDataChecker.Models.Factories.Base;

namespace TigerLookupDataChecker.Helpers
{
    public static class SqlHelper
    {
        public static bool CheckConnection(string connectionString)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return false;
                }

                using (SqlConnection myConn = new SqlConnection(connectionString))
                {
                    myConn.Open();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<Dictionary<string, object>> GetGenericQueryResult(string connstring, string sqlScript)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    using (SqlCommand command = new SqlCommand(sqlScript, connection))
                    {
                        connection.Open();
                        int fieldCount = 0;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (fieldCount == 0)
                            {
                                fieldCount = reader.FieldCount;
                            }

                            while (reader.Read())
                            {
                                Dictionary<string, object> rowData = new Dictionary<string, object>();

                                int i = 0;
                                while (i < fieldCount)
                                {
                                    rowData.Add(reader.GetName(i), reader[i]);
                                    i++;
                                }

                                result.Add(rowData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static string GenerateScriptFromTemplate(List<Firm> selectedFirms, string text)
        {
            string result = "";
            try
            {
                if (selectedFirms != null && selectedFirms.Any())
                {
                    foreach (var currentFirm in selectedFirms)
                    {
                        var newScript = text.Replace("_XXX", "_" + currentFirm.FirmNr.PadLeft(3, '0'))
                            .Replace("_XX", "_" + currentFirm.Period.PadLeft(2, '0'));

                        newScript += "\n--Firm " + currentFirm.FirmNr + "--\n";
                        result += newScript;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static string ExecuteSqls(string connString, string sqlScripts)
        {
            try
            {
                bool hasError = false;
                string exceptionText = "No exception! Success";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand beginTranCommand = new SqlCommand("BEGIN TRAN", conn))
                    {
                        beginTranCommand.ExecuteNonQuery();
                    }

                    try
                    {

                        using (SqlCommand myCommand = new SqlCommand(sqlScripts, conn))
                        {
                            myCommand.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        hasError = true;
                        exceptionText += ex.Message;
                    }

                    if (hasError)
                    {
                        using (SqlCommand commitCommand = new SqlCommand("ROLLBACK", conn))
                        {
                            commitCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand commitCommand = new SqlCommand("COMMIT", conn))
                        {
                            commitCommand.ExecuteNonQuery();
                        }
                    }
                }

                return exceptionText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CheckInputLookupDataAndGenerateMissingInsertStatements(string connString)
        {
            string targetInsertScripts = "";
            string generatedScripts = "--Generated Scripts\n{0}--End Of GeneratedScripts";
            try
            {
                CityXml cityXml = null;
                TownXml townXml = null;
                TaxOfficeXml taxOfficeXml = null;

                int countryRef = 0;

                //Input dosyalar deserialize edilerek nesnelere set edilir
                foreach (var currentFile in Constants.SourceLinks.Keys)
                {
                    var filePath = Constants.InputFolder + "\\" + currentFile;

                    if (!System.IO.File.Exists(filePath))
                        throw new Exception("File is missing " + filePath);

                    if (currentFile == Constants.CityXmlFileName)
                    {
                        cityXml = LookupFetcherFactory.GetFetcher(currentFile).GetInputFileData(currentFile);
                    }
                    else if (currentFile == Constants.TownXmlFileName)
                    {
                        townXml = LookupFetcherFactory.GetFetcher(currentFile).GetInputFileData(currentFile);
                    }
                    else if (currentFile == Constants.TxOfXmlFileName)
                    {
                        taxOfficeXml = LookupFetcherFactory.GetFetcher(currentFile).GetInputFileData(currentFile);
                    }
                }

                //Country bilgisi sorgulanir id si alinir 
                string countrySql = "select cc.LOGICALREF from L_COUNTRY cc where cc.CODE = 'TR'";
                var countryResult = GetGenericQueryResult(connString, countrySql);

                countryRef = int.Parse(countryResult[0]["LOGICALREF"].ToString());

                //City kayıtları sorgulanir   
                string citySql = "select LOGICALREF, CODE, NAME from L_CITY c where c.COUNTRY = " + countryRef;
                var cityResult = GetGenericQueryResult(connString, citySql);

                //Town kayıtları sorgulanir
                string townSql = "select CODE, NAME from L_TOWN t where t.CNTRNR = " + countryRef;
                var townResult = GetGenericQueryResult(connString, townSql);

                //taxoffice kayıtları sorgulanir
                string taxOfficeSql = "select CODE,NAME from L_TAXOFFICE t where t.CNTRNR =" + countryRef;
                var taxOfficeResult = GetGenericQueryResult(connString, taxOfficeSql);

                string townMaxCodeSql = "select CTYREF, max(CODE) as MAXCODE from L_TOWN group by CTYREF order by CTYREF";
                var townMaxCodeResult = GetGenericQueryResult(connString, townMaxCodeSql);


                //Citylerin code tutmayanlari ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (cityXml.Cities != null)
                {
                    var newRecords = cityXml.Cities.Where(f => !cityResult.Any(x => x["CODE"].ToString() == f.Code)).ToList();
                    if (newRecords != null && newRecords.Any())
                    {
                        foreach (var currentRecord in newRecords)
                        {
                            targetInsertScripts += currentRecord.ToInsertStatement(countryRef);
                        }
                    }
                }

                //City code uzerinden filreleme ile name tutmayanlar ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (townXml.Towns != null)
                {
                    //Sehirler bazında donulur
                    //sehir icin max code alınır sıradan devam ettirilir
                    foreach (var currentCityRecord in townMaxCodeResult.OrderBy(f => f["CTYREF"]))
                    {
                        var currentCityCode = int.Parse(currentCityRecord["CTYREF"].ToString());
                        //ilgili il bilgisine gore max deger alinir
                        var citysTownNextNo = int.Parse(currentCityRecord["MAXCODE"].ToString()) + 1;

                        var newTownRecordsForCity = townXml.Towns.Where(f => f.CityCode == currentCityCode.ToString().PadLeft(2,'0') &&
                                                                        !townResult.Any(x=> x["NAME"].ToString().ToLower().ToUpper() == 
                                                                                    f.Name.ToLower().ToUpper())
                                                                 ).ToList();

                        if (newTownRecordsForCity != null && newTownRecordsForCity.Any())
                        {
                            foreach(var currentNewTownRecord in newTownRecordsForCity)
                            {
                                targetInsertScripts += currentNewTownRecord.ToInsertStatement(countryRef, citysTownNextNo.ToString().PadLeft(2, '0'));
                                citysTownNextNo += 1;
                            }
                        }
                    }
                }

                //Code tutmayan kayitlar ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (taxOfficeXml.TaxOffices != null)
                {
                    var newRecords = taxOfficeXml.TaxOffices.Where(f => !taxOfficeResult.Any(x => x["CODE"].ToString() == f.Code)).ToList();
                    if (newRecords != null && newRecords.Any())
                    {
                        foreach (var currentRecord in newRecords)
                        {
                            targetInsertScripts += currentRecord.ToInsertStatement(countryRef);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            generatedScripts = string.Format(generatedScripts, targetInsertScripts);
            return generatedScripts;
        }
    }
}
