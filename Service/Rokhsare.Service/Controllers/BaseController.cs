using Rokhsare.ConfigReader;
using Rokhsare.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Rokhsare.Service.Controllers
{
    public class BaseController : ApiController
    {
        StaticController _StaticController = new StaticController();

        testRokhsarehClubDBContext _RokhsarehClubDb = null;
        public testRokhsarehClubDBContext RokhsarehClubDb
        {
            get
            {
                if (_RokhsarehClubDb == null)
                    _RokhsarehClubDb = ConfigReader.ConfigReader.GetRokhsarehClubDb;
                return _RokhsarehClubDb;
            }
        }

        public string GetIpAddress
        {
            get
            {
                return _StaticController.IpAddress();
            }
        }

        public string GetBrowser
        {
            get
            {
                return _StaticController.Browser();
            }
        }

        public string GetUserAgent
        {
            get
            {
                return _StaticController.UserAgent();
            }
        }
    }

    public class StaticController : Controller
    {

        public string IpAddress()
        {
            return (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
        }

        public string Browser()
        {
            return Request.Browser.Browser;
        }

        public string UserAgent()
        {
            return Request.UserAgent;
        }
    }
}