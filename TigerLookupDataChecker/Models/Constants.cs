using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerLookupDataChecker.Models
{
    public class Constants
    {
        public const string InputFolder = @"..\..\..\Inputs";
        public const string OutputsFolder = @"..\..\..\Outputs";
        public const string Temp = @"..\..\..\Temp";
        public const string FileEncoding = "ISO-8859-9";

        public const string CityXmlFileName = "CITY-TR.XML";
        public const string TownXmlFileName = "TOWN-TR.XML";
        public const string TxOfXmlFileName = "TXOF-TR.XML";


        public const string ConnTemplate = "data source={0};initial catalog={1};persist security info=True;user id={2};Password={3}";
        public const string FirmSql = "select NR,NAME,PERNR from L_CAPIFIRM order by NR";
        public const string DatabasesSql = @"SELECT DB_NAME(database_id) AS [Database], database_id FROM sys.databases
                                                    where DB_NAME(database_id) like '{0}%'
                                                    order by DB_NAME(database_id)";

        public const string ColumnAliasDatabase = "Database";
        public const string ColumnAliasNr = "NR";
        public const string ColumnAliasName = "NAME";
        public const string ColumnAliasPerNr = "PERNR";

        //TODO belki bu linkler parametrik bir yere konabilir
        public static Dictionary<string, string> SourceLinks = new Dictionary<string, string>()
        {
            {CityXmlFileName,"https://www.nvi.gov.tr/PublishingImages/Pages/il-ilce-kod-tablosu/IL_ILCE_LISTESI.xls" },
            {TownXmlFileName,"https://www.nvi.gov.tr/PublishingImages/Pages/il-ilce-kod-tablosu/IL_ILCE_LISTESI.xls" },
            {TxOfXmlFileName,"http://www.gib.gov.tr/fileadmin/HTML/vergidairebaskanlik/vergidairelerilistesi.pdf" }
        };
    }
}
