using NAF.Common.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TigerLookupDataChecker.Models
{
    [XmlRoot(ElementName = "TAXOFFICE")]
    public class TaxOffice: ModelBase
    {
        [XmlElement(ElementName = "CODE")]
        public string Code { get; set; }
        [XmlElement(ElementName = "NAME")]
        public string Name { get; set; }
        [XmlElement(ElementName = "ADDR1")]
        public string Addr1 { get; set; }
        [XmlElement(ElementName = "COUNTRYCODE")]
        public string CountryCode { get; set; }
        [XmlAttribute(AttributeName = "DBOP")]
        public string DbOp { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public TaxOffice()
        {
            CountryCode = "TR";
            DbOp = "INS";
        }

        /// <summary>
        /// To String Method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IsEmpty())
                return string.Empty;

            return string.Format("Code:{0};Name:{1};CountryCode:{2};Addr1:{3}", Code, Name, CountryCode, Addr1);
        }

        public override bool IsEmpty()
        {
            if (Code.NotAssigned() && Name.NotAssigned())
                return true;

            return false;
        }

        /// <summary>
        /// ICompare method
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public  bool Equals(TaxOffice that)
        {
            if (that == null)
                return true;

            if (this.Code != that.Code)
                return false;

            if (!this.CountryCode.Equals(that.CountryCode))
                return false;

            if (this.Name != null && that.Name != null && this.Name.ToLower().ToUpper() != that.Name.ToLower().ToUpper())
                return false;

            return true;
        }

        public string ToXml()
        {
            //dosyada buyuk harfli hepsi o nedenle buyuk harf yapildi name alani
            return string.Format(@"<TAXOFFICE DBOP=""{0}""><CODE>{1}</CODE><NAME>{2}</NAME><ADDR1>{3}</ADDR1><COUNTRYCODE>{4}</COUNTRYCODE></TAXOFFICE>", DbOp, Code, Name.ToUpper(), Addr1, CountryCode);
        }

        public string ToInsertStatement(int countryRef)
        {
            var insertStatement = @"INSERT INTO [dbo].[L_TAXOFFICE] ([CODE],[NAME],[ADDR1],[ADDR2],[CNTRNR],[SITEID],[RECSTATUS],[ORGLOGICREF]) VALUES ('{0}','{1}','{2}','',{3},0,0,0);";
            insertStatement = string.Format(insertStatement + "\n", Code, Name, Addr1, countryRef);
            return insertStatement;
        }
    }

    [XmlRoot(ElementName = "TAXOFFICES")]
    public class TaxOfficeXml
    {
        [XmlElement(ElementName = "TAXOFFICE")]
        public List<TaxOffice> TaxOffices { get; set; }

    }
}
