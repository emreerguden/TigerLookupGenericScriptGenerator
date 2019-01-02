using NAF.Common.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TigerLookupDataChecker.Models
{
    [XmlRoot(ElementName = "CITY")]
    public class City: ModelBase
    {
        [XmlElement(ElementName = "CODE")]
        public string Code { get; set; }
        [XmlElement(ElementName = "NAME")]
        public string Name { get; set; }
        [XmlElement(ElementName = "COUNTRYCODE")]
        public string CountryCode { get; set; }

        [XmlAttribute(AttributeName = "DBOP")]
        public string DbOp { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public City()
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

            return string.Format("Code:{0};Name:{1};CountryCode:{2}", Code, Name, CountryCode);
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
        public  bool Equals(City that)
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
            return string.Format(@"<CITY DBOP=""{0}""><CODE>{1}</CODE><NAME>{2}</NAME><COUNTRYCODE>{3}</COUNTRYCODE></CITY>", DbOp, Code, Name,CountryCode);
        }

        public string ToInsertStatement(int countryRef)
        {
            var insertStatement = @"INSERT INTO [dbo].[L_CITY] ([COUNTRY],[NAME],[CODE],[SITEID],[RECSTATUS],[ORGLOGICREF],[NAME2],[NETFLAG]) VALUES ({0},'{1}','{2}',0,0,0,'','');";
            insertStatement = string.Format(insertStatement + "\n", countryRef, Name, Code);
            return insertStatement;
        }

    }

    [XmlRoot(ElementName = "CITIES")]
    public class CityXml
    {
        [XmlElement(ElementName = "CITY")]
        public List<City> Cities { get; set; }

    }
}
