using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using OnlyEdu.Common.Dapper.Enumeration;

namespace OnlyEdu.Common.Dapper.Persistence.Data
{
    public class DataBaseHelper
    {
        public static string ReaderConnectString { get; set; }
        public static string WriterConnectString { get; set; }
        public static IConfiguration Configuration { get; set; }
        public static IConfigurationRoot _configurationRoot { get; set; }
        public DataBaseHelper(IConfigurationRoot configuration)
        {
            _configurationRoot = configuration;
        }


        public static string GetConnectionStrings(EumDBWay dbWay, string dbInfo, string typeInfo = "")
        {
            if (string.IsNullOrEmpty(dbInfo))
            {
                dbInfo = "POC";
            }
            string conKey = string.Format("ConnectionStrings:DB_{0}{1}", dbWay.GetDisplayName(), dbInfo);
            var con = _configurationRoot[conKey];
            if (dbWay == EumDBWay.Reader)
            {
                ReaderConnectString = con;
                return ReaderConnectString;
            }
            else if (dbWay == EumDBWay.Writer)
            {
                WriterConnectString = con;
                return WriterConnectString;
            }
            else
            {
                return "";
            }
        }
    }
}
