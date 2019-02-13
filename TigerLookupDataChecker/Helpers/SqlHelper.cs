using NAF.Common.Utils.Extensions;
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

        public static int GetCountryRef(string countryCode, List<Dictionary<string,object>> countries)
        {
            int countryRef = 0;

            if(countries.Assigned() && countries.Any())
            {
                var targetCountry = countries.FirstOrDefault(f => f["CODE"].ToString().ToUpper() == countryCode.ToUpper());
                if (targetCountry.Assigned())
                {
                    countryRef = int.Parse(targetCountry["COUNTRYNR"].ToString());
                }
            }

            return countryRef;
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
                BankXml bankXml = null;
                BankBranchXml bankBranchXml = null;

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
                    else if (currentFile == Constants.BankOfXmlFileName)
                    {
                        bankXml = LookupFetcherFactory.GetFetcher(currentFile).GetInputFileData(currentFile);
                    }
                    else if (currentFile == Constants.BankBranchOfXmlFileName)
                    {
                        bankBranchXml = LookupFetcherFactory.GetFetcher(currentFile).GetInputFileData(currentFile);
                    }
                }

                //Country bilgisi sorgulanir id si alinir 
                string countrySql = "select cc.LOGICALREF, cc.COUNTRYNR, cc.CODE from L_COUNTRY cc";
                var countryResult = GetGenericQueryResult(connString, countrySql);

                var trCountryRef = GetCountryRef("TR", countryResult);

                //City kayıtları sorgulanir   
                string citySql = "select LOGICALREF, CODE, NAME from L_CITY c where c.COUNTRY = " + trCountryRef;
                var cityResult = GetGenericQueryResult(connString, citySql);

                //Town kayıtları sorgulanir
                string townSql = "select CODE, NAME from L_TOWN t where t.CNTRNR = " + trCountryRef;
                var townResult = GetGenericQueryResult(connString, townSql);

                //taxoffice kayıtları sorgulanir
                string taxOfficeSql = "select CODE,NAME from L_TAXOFFICE t where t.CNTRNR =" + trCountryRef;
                var taxOfficeResult = GetGenericQueryResult(connString, taxOfficeSql);

                string townMaxCodeSql = "select CTYREF, max(CODE) as MAXCODE from L_TOWN group by CTYREF order by CTYREF";
                var townMaxCodeResult = GetGenericQueryResult(connString, townMaxCodeSql);

                string bankSql = "select LOGICALREF, CODE, NAME FROM L_BANKCODE";
                var bankResult = GetGenericQueryResult(connString, bankSql);

                string bankBranchSql = "select bb.CODE,bb.NAME, (SELECT b.CODE FROM L_BANKCODE b where b.LOGICALREF = bb.BANKREF) as BANKCODE FROM L_BNBRANCH bb";
                var bankBranchResult = GetGenericQueryResult(connString, bankBranchSql);

                var bankFinalResult = GetGenericQueryResult(connString, bankSql);

                #region city scripts
                //Citylerin code tutmayanlari ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (cityXml.Cities != null)
                {
                    var newRecords = cityXml.Cities.Where(f => !cityResult.Any(x => x["CODE"].ToString() == f.Code)).ToList();
                    if (newRecords != null && newRecords.Any())
                    {
                        foreach (var currentRecord in newRecords)
                        {
                            var targetCountryRef = GetCountryRef(currentRecord.CountryCode, countryResult);
                            targetInsertScripts += currentRecord.ToInsertStatement(targetCountryRef);
                        }
                    }
                }
                #endregion

                #region town scripts
                //City code uzerinden filreleme ile name tutmayanlar ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (townXml.Towns != null)
                {
                    if (townMaxCodeResult == null || !townMaxCodeResult.Any())
                    {
                        townMaxCodeResult = cityResult.Select(f => new Dictionary<string, object>()
                        {
                            { "CTYREF", f["CODE"].ToString()},
                            {"MAXCODE","00" }
                        }).ToList();
                    }
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
                                var targetCountryRef = GetCountryRef(currentNewTownRecord.CountryCode, countryResult);
                                targetInsertScripts += currentNewTownRecord.ToInsertStatement(targetCountryRef, citysTownNextNo.ToString().PadLeft(2, '0'));
                                citysTownNextNo += 1;
                            }
                        }
                    }
                }
                #endregion

                #region taxoffice scripts
                //Code tutmayan kayitlar ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (taxOfficeXml.TaxOffices != null)
                {
                    var newRecords = taxOfficeXml.TaxOffices.Where(f => !taxOfficeResult.Any(x => x["CODE"].ToString() == f.Code)).ToList();
                    if (newRecords != null && newRecords.Any())
                    {
                        foreach (var currentRecord in newRecords)
                        {
                            var targetCountryRef = GetCountryRef(currentRecord.CountryCode, countryResult);

                            targetInsertScripts += currentRecord.ToInsertStatement(trCountryRef);
                        }
                    }
                }
                #endregion

                #region bank scripts
                //bankalarin code tutmayanlari ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (bankXml.Banks != null)
                {
                    var newRecords = bankXml.Banks.Where(f => !bankResult.Any(x => x["CODE"].ToString() == f.Code)).ToList();
                    if (newRecords != null && newRecords.Any())
                    {
                        foreach (var currentRecord in newRecords)
                        {
                            var targetCountryRef = GetCountryRef(currentRecord.CountryCode, countryResult);
                            targetInsertScripts += currentRecord.ToInsertStatement(targetCountryRef);
                        }
                    }
                }
                #endregion

                #region bank branch scripts
                //bankalarin code tutmayanlari ayiklanir
                //Insert scriptleri mevcut ayiklanmis kayitlar uzerinden olusturulur
                if (bankBranchXml.BankBranchs != null)
                {
                    Dictionary<string, int> bankCodeLogicalrefRepository = new Dictionary<string, int>();

                    var newRecords = bankBranchXml.BankBranchs.Where(f => !bankBranchResult.Any(x => x["BANKCODE"].ToString() == f.BankCode && x["CODE"].ToString() == f.Code)).ToList();
                    if (newRecords != null && newRecords.Any())
                    {
                        foreach (var currentRecord in newRecords)
                        {
                            //ilgili bankanin logicalref bilgisi sorgulanir ve burada set edilir
                            var targetBankRef = 0;

                            if (!bankCodeLogicalrefRepository.Keys.Contains(currentRecord.BankCode))
                            {
                               var targetRecord = bankFinalResult.FirstOrDefault(f => f["CODE"].ToString() == currentRecord.BankCode);

                               if (targetRecord.Assigned())
                                {
                                    targetBankRef = int.Parse(targetRecord["LOGICALREF"].ToString());
                                    bankCodeLogicalrefRepository.AddOrUpdate(currentRecord.BankCode, targetBankRef);
                                }
                            }
                            else
                            {
                                targetBankRef = bankCodeLogicalrefRepository[currentRecord.BankCode];
                            }

                            if (targetBankRef != 0)
                            {
                                var targetCountryRef = GetCountryRef(currentRecord.CountryCode, countryResult);
                                targetInsertScripts += currentRecord.ToInsertStatement(targetCountryRef, targetBankRef);
                            }
                        }
                    }
                }
                #endregion
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
