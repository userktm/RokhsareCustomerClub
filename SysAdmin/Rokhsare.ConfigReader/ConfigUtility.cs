using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rokhsare.Models;
using Rokhsare.Utility;

namespace Rokhsare.ConfigReader
{
    public class WebConfigKeys
    {
        public class ConnectionStrings
        {
            public static string RokhsareDb { get { return "RokhsareDb"; } }
        }

        public class AppSettings
        {
            public static string ApplicationName { get { return "ApplicationName"; } }
            public static string CompanyName { get { return "CompanyName"; } }
            public static string MaintenanceMode { get { return "MaintenanceMode"; } }
            public static string ProfileName { get { return "ProfileName"; } }
            public static string SaveActions { get { return "SaveActions"; } }
            public static string AdminURL { get { return "AdminURL"; } }
            public static string IsLocal { get { return "IsLocal"; } }
            public static string SmsPanel { get { return "SmsPanel"; } }
            public static string InHost { get { return "InHost"; } }
        }
    }

    public class ConfigReader
    {
        #region App Setting Config Reader
        public static bool SendSystemSMS
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == "SendSystemSMS"))
                    return ConfigurationManager.AppSettings["SendSystemSMS"] == "true";
                return false;
            }
        }

        public static bool RunUpdateEngine
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == "RunUpdateEngine"))
                    return ConfigurationManager.AppSettings["RunUpdateEngine"] == "true";
                return false;
            }
        }

        public static string ProfileName
        {
            get
            {
                return ConfigurationManager.AppSettings[WebConfigKeys.AppSettings.ProfileName];
            }
        }

        public static string AdminURL
        {
            get
            {
                return ConfigurationManager.AppSettings[WebConfigKeys.AppSettings.AdminURL];
            }
        }

        public static string ApplicationName
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == WebConfigKeys.AppSettings.ApplicationName))
                    return ConfigurationManager.AppSettings[WebConfigKeys.AppSettings.ApplicationName];
                return "Farhang";
            }
        }

        public static string CompanyName
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == WebConfigKeys.AppSettings.CompanyName))
                    return ConfigurationManager.AppSettings[WebConfigKeys.AppSettings.CompanyName];
                return "Farhang";
            }
        }

        public static string GetSmsPanel
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == WebConfigKeys.AppSettings.SmsPanel))
                    return ConfigurationManager.AppSettings[WebConfigKeys.AppSettings.SmsPanel];
                return "sms.ir";
            }
        }

        public static Int32 CurrentYear
        {
            get
            {
                var s = ConfigurationManager.AppSettings["CurrentYear"];
                if (!string.IsNullOrEmpty(s) && s.IsNumeric())
                    return Int16.Parse(s);
                return 0;
            }
        }

        public static bool SendReportSMS
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == "SendReportSMS"))
                {
                    var v = ConfigurationManager.AppSettings["SendReportSMS"];
                    return !string.IsNullOrEmpty(v) && v.ToLower() == "true";
                }
                return false;
            }
        }

        public static bool PartialMaintenanceMode
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == "PartialMaintenanceMode"))
                {
                    var v = ConfigurationManager.AppSettings["PartialMaintenanceMode"];
                    return !string.IsNullOrEmpty(v) && v.ToLower() == "true";
                }
                return false;
            }
        }

        public static bool IsLocal
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == "IsLocal"))
                {
                    var v = ConfigurationManager.AppSettings["IsLocal"];
                    return !string.IsNullOrEmpty(v) && v.ToLower() == "true";
                }
                return false;
            }
        }

        public static bool IsHost
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(u => u == "IsHost"))
                {
                    var v = ConfigurationManager.AppSettings["IsHost"];
                    return !string.IsNullOrEmpty(v) && v.ToLower() == "true";
                }
                return false;
            }
        }

        public static testRokhsarehClubDBContext GetRokhsarehClubDb
        {
            get
            {
                var cnn = ConnectionString;
                return Rokhsare.Data.EFDbContextFactory.GetWebRequestScopedDataContext<testRokhsarehClubDBContext>("rdb", cnn);
            }
        }

        #endregion



        #region Connection string Reader

        //فراخوانی کتابخانه wininet.dll ویندوز
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //بررسی اتصال به اینترنت
        static bool CheckConnection()
        {
            if (!IsHost)
            {
                bool State = false;
                int desc;

                State = InternetGetConnectedState(out desc, 0);

                return State;
            }
            else
            {
                return true;
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (CheckConnection())
                {
                    if (IsLocal == true)
                    {
                        return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                    }
                    else
                    {
                        return ConfigurationManager.ConnectionStrings["Server"].ConnectionString;
                    }
                }
                else
                {
                    return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                }
            }
        }

        #endregion

        public class FromDb
        {
            //public static string GetConnectionString(string key, string profile)
            //{

            //    key = key.ToLower();
            //    var ck = FromCache(key, profile);

            //    if (ck != null)
            //    {
            //        var pwd = BazaarcheOnline.Utility.MD5Encryption.DecryptString(ck.Password, ck.PasswordSalt);
            //        var cnn = string.Format("Data Source={0};Initial Catalog={1};uid={2};pwd={3};", ck.DbServerName, ck.DbName, ck.Username, pwd);
            //        if (!key.Contains("log"))
            //            cnn = cnn + "MultipleActiveResultSets=True";
            //        return cnn;
            //    }

            //    try
            //    {
            //        return ConfigurationManager.ConnectionStrings[key].ConnectionString;
            //    }
            //    catch
            //    {
            //        return string.Empty;
            //    }
            //}
        }
    }
}
