using Rokhsare.Models;
using Rokhsare.Common.Model;
using Rokhsare.ConfigReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Rokhsare.Control.Base.Controllers
{
    public class GeneralBaseController : Controller
    {
        protected string GetIPHelper()
        {
            string ip = HttpContext.Request.UserHostAddress;
            return ip;
        }

        public string BprViewName { get { return "ProcessResult"; } }
        public string PermissionViewName { get { return "PermissionResult"; } }
        public bool PermissionType = false;
        public BatchProcessResult_Model GetBpr()
        {
            return new BatchProcessResult_Model();
        }

        protected string GetAdminURL()
        {
            return ConfigReader.ConfigReader.AdminURL;
        }

        //public void LogSave(AppUser user, string Action, string Description, DateTime date)
        //{
        //    Log log = new Log();
        //    log.LogName = Action;
        //    log.UserId = user.Id;
        //    log.Action = Description;
        //    log.CreateDate = date;
        //    log.IPAddress = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
        //    log.Browser = Request.Browser.Browser;
        //    log.UserAgent = Request.UserAgent;

        //    ConfigReader.ConfigReader.GetFarhangDb.Logs.Add(log);
        //    ConfigReader.ConfigReader.GetFarhangDb.SaveChanges();
        //}

        protected bool isLocalRequest()
        {
            return Request.Url.Host.Contains("localhost");
        }

        User _as = null;
        protected User GetCurrentUser
        {
            get
            {
                if (Request.IsAuthenticated && _as == null)
                {
                    string gid = string.Empty;
                    gid = User.Identity.Name;
                    if (!string.IsNullOrEmpty(gid))
                    {
                        try
                        {
                            _as = ConfigReader.ConfigReader.GetRokhsarehClubDb.Users.SingleOrDefault(i => i.NationalNumber == gid);
                        }
                        catch
                        {
                            FormsAuthentication.SignOut();
                        }
                    }
                    else
                        FormsAuthentication.SignOut();
                }

                return _as;
            }
        }
    }

    [Authorize]
    public class BaseController : GeneralBaseController
    {
        testRokhsarehClubDBContext _RokhsareDb = null;
        public testRokhsarehClubDBContext RokhsareDb
        {
            get
            {
                if (_RokhsareDb == null)
                    _RokhsareDb = ConfigReader.ConfigReader.GetRokhsarehClubDb;
                return RokhsareDb;
            }
        }

        public bool HasDbError
        {
            get
            {
                var hasDbError = false;
                try
                {
                    var dc = ConfigReader.ConfigReader.GetRokhsarehClubDb;
                }
                catch
                {
                    hasDbError = true;
                }
                return hasDbError;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var isChildAction = filterContext.IsChildAction;
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            string areaName = "";

            filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var routeData = filterContext.RequestContext.RouteData;
            if (routeData.DataTokens["area"] != null)
                areaName = routeData.DataTokens["area"].ToString().ToLower();
            if (string.IsNullOrEmpty(areaName))
                areaName = "root";

            var controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            base.OnActionExecuted(filterContext);
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{

        //    var area = filterContext.RouteData.DataTokens["area"];
        //    var areaName = area != null ? area.ToString() : "";
        //    var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
        //    string actionName = filterContext.ActionDescriptor.ActionName.ToLower();

        //    if (!isLocalRequest())
        //    {

        //        if (HasDbError || ConfigReader.MaintenanceMode)
        //        {
        //            string[] exceptActions = new string[] { "maintenancemode", "logout" };
        //            if (!exceptActions.Any(u => u == actionName) && ConfigReader.ProjectCode != 1)
        //            {
        //                if (!filterContext.IsChildAction)
        //                    filterContext.Result = new RedirectResult(Url.Action("MaintenanceMode", "Home", new { area = "" }));
        //                else
        //                    filterContext.Result = new PartialViewResult() { ViewName = "MaintenanceMode" };
        //            }
        //            return;
        //        }
        //    }

        //    //if (!filterContext.IsChildAction)
        //    //{
        //    //    if (!PermissionUtility.UserHasPermission(ConfigReader.ProjectCode, areaName, controllerName, actionName, User.Identity.Name))
        //    //    {
        //    //        filterContext.Result = new RedirectResult("~/Home/AccessDenied");
        //    //    }
        //    //}

        //    //if (ConfigReader.SaveActions)
        //    //    AppSaveAction(area, controllerName, actionName);

        //    base.OnActionExecuting(filterContext);
        //}
        //private void AppSaveAction(object area, string controllerName, string actionName)
        //{
        //    if (ConfigReader.SaveActions)
        //    {
        //        string getAreaText = area != null ? area.ToString() : "";
        //        var pcode = ConfigReader.ProjectCode;
        //        var ai = new Clinic.SystemBase.Data.Models.ActionInfo()
        //        {
        //            ActionName = actionName.ToLower(),
        //            ControllerName = controllerName.ToLower(),
        //            Title = actionName.ToLower(),
        //            RequireAuthorization = false,
        //            RequirePermission = false,
        //            ProjectId = (byte)pcode,
        //            AreaName = getAreaText.ToLower()
        //        };
        //        if (!ConfigReader.GetAclDb.ActionInfoes.Any(a => a.ProjectId == pcode && a.ActionName == ai.ActionName && a.ControllerName == ai.ControllerName && a.AreaName == ai.AreaName))
        //        {
        //            var bpr = new BatchProcessResult_Model();
        //            Pargoon.SND.ACL.DataAccess.ActionInfoDAL.Add(ai, bpr);
        //        }
        //    }
        //}
    }

    public class PanelBaseController : BaseController
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.User.IsInRole("Admin") && !HttpContext.User.Identity.IsAuthenticated)
            {
                string returnUrl = "";
                var isChildAction = filterContext.IsChildAction;
                var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
                string areaName = "";

                filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                var routeData = filterContext.RequestContext.RouteData;
                if (routeData.DataTokens["area"] != null)
                    areaName = routeData.DataTokens["area"].ToString().ToLower();
                if (string.IsNullOrEmpty(areaName))
                    areaName = "root";

                var controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
                returnUrl = Url.Action(actionName, controllerName, new { area = areaName });

                var url = Url.Action("Login", "Admin", new { area = "" });

                Response.Redirect(url + "?ReturnUrl=" + returnUrl);
            }
            else if (!HttpContext.User.IsInRole("Admin") && !HttpContext.User.Identity.IsAuthenticated)
            {
                string returnUrl = "";
                var isChildAction = filterContext.IsChildAction;
                var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
                string areaName = "";

                var routeData = filterContext.RequestContext.RouteData;
                if (routeData.DataTokens["area"] != null)
                    areaName = routeData.DataTokens["area"].ToString().ToLower();
                if (string.IsNullOrEmpty(areaName))
                    areaName = "root";

                var controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
                returnUrl = Url.Action(actionName, controllerName, new { area = areaName });

                var url = Url.Action("Login", "Account", new { area = "" });

                Response.Redirect(url + "?ReturnUrl=" + returnUrl);
            }
        }
    }

    public class DbBaseController : BaseController
    {
        //public string RenderActionToString(string actionName, string controllerName, object routeValues)
        //{
        //    var viewContext = new ViewContext(this.ControllerContext, new FakeView(), this.ViewData, this.TempData, TextWriter.Null);

        //    var h = new HtmlHelper(viewContext, new ViewPage());
        //    var x = h.Action(actionName, controllerName, routeValues);
        //    return x.ToString();
        //}
    }

    public class FakeView : IView
    {
        #region IView Members
        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    //[Authorize]
    //public class AuthBaseController : DbBaseController
    //{
    //    //Customer _cs = null;
    //    //protected Customer CurrentCustomer
    //    //{
    //    //    get
    //    //    {
    //    //        if (_cs == null)
    //    //        {
    //    //            setcs();
    //    //        }
    //    //        return _cs;
    //    //    }
    //    //    set
    //    //    {
    //    //        _cs = value;
    //    //    }
    //    //}

    //    //void setcs()
    //    //{
    //    //    if (Request.IsAuthenticated && _cs == null)
    //    //    {
    //    //        var ug = new Guid(User.Identity.Name);
    //    //        _cs = Pargoon.IrBookInfo.Cache.CustomerCacheReader.GetCustomer(ug);
    //    //    }
    //    //    if (_cs != null)
    //    //    {
    //    //        TempData["username"] = _cs.FullName;
    //    //        TempData["userId"] = _cs.NationalId;
    //    //    }
    //    //}
    //    public AuthBaseController()
    //    {

    //    }
    //    protected override void Initialize(System.Web.Routing.RequestContext requestContext)
    //    {
    //        base.Initialize(requestContext);
    //    }

    //    protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
    //    {

    //        return base.BeginExecute(requestContext, callback, state);
    //    }

    //    protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
    //    {
    //        return base.BeginExecuteCore(callback, state);
    //    }

    //    protected override void ExecuteCore()
    //    {
    //        base.ExecuteCore();

    //    }

    //    protected override void EndExecute(IAsyncResult asyncResult)
    //    {
    //        base.EndExecute(asyncResult);
    //    }

    //    protected override void Execute(System.Web.Routing.RequestContext requestContext)
    //    {
    //        base.Execute(requestContext);
    //    }

    //    protected override void EndExecuteCore(IAsyncResult asyncResult)
    //    {
    //        base.EndExecuteCore(asyncResult);
    //    }

    //    protected override void OnActionExecuted(ActionExecutedContext filterContext)
    //    {
    //        base.OnActionExecuted(filterContext);
    //    }

    //    //protected override void OnActionExecuting(ActionExecutingContext filterContext)
    //    //{
    //    //    if (!filterContext.IsChildAction)
    //    //    {
    //    //        setcs();
    //    //    }
    //    //    base.OnActionExecuting(filterContext);
    //    //}

    //    //protected override void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
    //    //{
    //    //    base.OnAuthentication(filterContext);
    //    //}

    //    //protected override void OnAuthenticationChallenge(System.Web.Mvc.Filters.AuthenticationChallengeContext filterContext)
    //    //{
    //    //    base.OnAuthenticationChallenge(filterContext);
    //    //}

    //    protected override void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        base.OnAuthorization(filterContext);
    //    }

    //    protected override void OnException(ExceptionContext filterContext)
    //    {
    //        base.OnException(filterContext);
    //    }
    //}

    //public class ShopBaseController : AuthBaseController
    //{
    //    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    //        base.OnActionExecuting(filterContext);

    //        TempData["basketItems"] = 0;
    //    }
    //}
}
