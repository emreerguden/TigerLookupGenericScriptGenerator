using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using NAF.Common.Utils.Extensions;

namespace TigerLookupDataChecker.Models
{
    [XmlRoot(ElementName = "TOWN")]
    public class Town : ModelBase
    {
        [XmlElement(ElementName = "COUNTRYCODE")]
        public string CountryCode { get; set; }
        [XmlElement(ElementName = "CITYCODE")]
        public string CityCode { get; set; }
        [XmlElement(ElementName = "NAME")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "DBOP")]
        public string DbOp { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public Town()
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

            return string.Format("CityCode:{0};Name:{1};CountryCode:{2}", CityCode, Name, CountryCode);
        }

        public override bool IsEmpty()
        {
            if (CityCode.NotAssigned() || Name.NotAssigned())
                return true;

            return false;
        }

        /// <summary>
        /// ICompare method
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public bool Equals(Town that)
        {
            if (that == null)
                return true;

            if (this.CityCode != that.CityCode)
                return false;

            if (!this.CountryCode.Equals(that.CountryCode))
                return false;

            if (this.Name != null && that.Name != null && this.Name.ToLower().ToUpper() != that.Name.ToLower().ToUpper())
                return false;

            return true;
        }
        public string ToXml()
        {
            return string.Format(@"<TOWN DBOP=""{0}""><COUNTRYCODE>{1}</COUNTRYCODE><CITYCODE>{2}</CITYCODE><NAME>{3}</NAME></TOWN>", DbOp, CountryCode, CityCode, Name);
        }

        public string ToInsertStatement(int countryRef, string newCode)
        {
            var insertStatement = @"INSERT INTO [dbo].[L_TOWN] ([CTYREF],[CNTRNR],[CODE],[NAME],[SITEID],[RECSTATUS],[ORGLOGICREF],[NAME2],[NETFLAG]) VALUES ({0},{1},'{2}','{3}',0,0,0,'','');";
            insertStatement = string.Format(insertStatement + "\n", int.Parse(CityCode), countryRef, newCode, Name);
            return insertStatement;
        }
    }

    [XmlRoot(ElementName = "TOWNS")]
    public class TownXml
    {
        [XmlElement(ElementName = "TOWN")]
        public List<Town> Towns { get; set; }
    }

}
