using Rokhsare.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Common.Model
{
    public static class Constants
    {
        public const int PageIndexDefault = 0;
        public const int PageSizeDefault = 15;
    }
    public class MVCActionInfo
    {
        public MVCActionInfo()
        {

        }
        public MVCActionInfo(string actionName, string controllerName, object routeValue)
        {
            this.Action = actionName;
            this.Controller = controllerName;
            this.RouteValue = routeValue;
        }
        public MVCActionInfo(string actionName, string controllerName, object routeValue, string updateTargetId)
        {
            this.Action = actionName;
            this.Controller = controllerName;
            this.RouteValue = routeValue;
            this.UpdateTargetId = updateTargetId;
        }
        public string Area { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string UpdateTargetId { get; set; }
        public object RouteValue { get; set; }
    }
    public class NavigationInfo
    {
        List<MVCActionInfo> _list = null;
        public NavigationInfo()
        {
            _list = new List<MVCActionInfo>();
            ShowApplicationTitle = true;
        }
        public string PageTitle { get; set; }
        public bool ShowApplicationTitle { get; set; }
        public List<MVCActionInfo> Items { get { return _list; } }
        public void Add(MVCActionInfo newAction)
        {
            _list.Add(newAction);
        }
    }
    public class PagingItems : MVCActionInfo
    {
        public string ModelPath { get; set; }
        public string SearchFormId { get; set; }
        public bool DoPagingWithSearchForm { get; set; }

        public string BackTo { get; set; }
        public string BackToMode { get; set; }

        [DisplayName("Page Index")]
        public int PageIndex { get; set; }

        [DisplayName("تعداد رکورد ")]
        public int PageSize { get; set; }
        public int OldPageSize { get; set; }

        public string SortExpression { get; set; }

        [DisplayName("Total Item Count")]
        public int TotalItemCount { get; set; }

        [DisplayName("Total Page Count")]
        public int TotalPageCount { get; private set; }

        public PagingItems()
        {
            PageIndex = Constants.PageIndexDefault;
            PageSize = Constants.PageSizeDefault;
            SortExpression = string.Empty;
            Area = string.Empty;
        }

        public PagingItems(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            SortExpression = string.Empty;
        }

        public PagingItems(int totalItemCount)
            : this()
        {
            this.TotalItemCount = totalItemCount;
        }

        public void Update(int pageSize, int totalItemCount)
        {
            this.PageSize = pageSize;
            this.TotalItemCount = totalItemCount;
            this.TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }

        public PagingItems(int pageIndex, int pageSize, int totalItemCount, string sortExpression)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.SortExpression = sortExpression;
            this.TotalItemCount = totalItemCount;
            this.TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }
    }
    public class PagedList<T> : List<T> where T : class
    {
        public T ModelInstance { get; set; }
        public IList<object> ExtraData { get; set; }

        public PagedList(List<T> items, PagingItems pagingItem)
        {
            this.AddRange(items);
            this.PagingItem = pagingItem;
            ExtraData = new List<object>();
        }

        public PagingItems PagingItem { get; set; }

    }

    public enum MessageViewType : byte { None = 0, Success = 1, Failed = 2, Warning = 3, Notification = 4, Information = 5 };

    [Serializable]
    public class MessageModel
    {
        public MessageModel()
        {

        }
        public MessageModel(string messageText, MessageViewType messageType)
        {
            this.Message = messageText;
            this.MessageType = messageType;
        }

        public string Message { get; set; }
        public MessageViewType MessageType { get; set; }
    }

    [Serializable]
    public class SimpleMessageModel
    {
        public SimpleMessageModel()
        {

        }
        public SimpleMessageModel(string messageText, MessageViewType messageType)
        {
            this.Message = messageText;
            this.MessageType = messageType;
        }

        public string Message { get; set; }
        public MessageViewType MessageType { get; set; }
    }

    public class BatchProcessResult_Model
    {

        public bool ShowInModal { get; set; }
        public string DefaultViewName { get { return "ProcessResult"; } }
        public int Success
        { get; set; }
        public bool CloseModalAfterShowSuccess { get; set; }

        public int Failed
        { get; set; }

        public bool IsValid { get { return Failed == 0; } }
        public List<SimpleMessageModel> Messages
        { get; set; }

        public string ErrorCss
        { get; set; }

        public string SuccessCss
        { get; set; }
        public bool RunChildAction { get; set; }
        public bool RedirectToUrl { get; set; }
        public string ReturnUrl { get; set; }
        public bool RedirectToAction { get; set; }
        public bool RunChildActionAjax { get; set; }
        public MVCActionInfo MVCActionInfo { get; set; }
        public string ClientScript { get; set; }
        public string FailedClientScript { get; set; }
        public string SuccessClientScript { get; set; }

        public string NotificationTitle { get; set; }
        public string NotificationText { get; set; }
        public string NotificationIcon { get; set; }

        public void Clear()
        {
            Messages = new List<SimpleMessageModel>();
            Failed = 0;
            Success = 0;
        }

        public void AppendMessage(string newMessage, MessageViewType messageType)
        {
            if (!string.IsNullOrEmpty(newMessage))
            {
                Messages.Add(new SimpleMessageModel(newMessage, messageType));
            }
        }

        public void AddNotification(string Title, string Text, string Icon)
        {
            NotificationTitle = Title;
            NotificationText = Text;
            NotificationIcon = Icon;
        }

        public void AddError(string newMessage)
        {
            Failed++;
            AppendMessage(newMessage, MessageViewType.Failed);
        }
        public void AddError(string ErrorType, string ErrorCode, string newMessage)
        {
            Failed++;
            AppendMessage(ErrorType + ":" + ErrorCode + " : " + newMessage, MessageViewType.Failed);
        }
        public void AddError(string ErrorCode, string newMessage)
        {
            Failed++;
            AppendMessage(ErrorCode + " : " + newMessage, MessageViewType.Failed);
        }
        public void AddException(Exception ex)
        {
            if (ex != null)
            {
                Failed++;
                AppendMessage(ex.Message, MessageViewType.Failed);
                if (ex.InnerException != null)
                {
                    AppendMessage(ex.InnerException.Message, MessageViewType.Failed);
                    if (ex.InnerException.InnerException != null)
                        AppendMessage(ex.InnerException.InnerException.Message, MessageViewType.Failed);
                }
            }
        }

        public void AddSuccess(string newMessage)
        {
            Success++;
            AppendMessage(newMessage, MessageViewType.Success);
        }
        public void AddSuccess(string newMessage, MessageViewType mt)
        {
            Success++;
            AppendMessage(newMessage, mt);
        }
        public BatchProcessResult_Model()
        {
            RedirectToUrl = false;
            RedirectToAction = false;
            RunChildAction = false;
            RunChildActionAjax = false;
            Success = 0;
            Failed = 0;
            ErrorCss = "alert alert-danger";
            SuccessCss = "alert alert-success";
            Messages = new List<SimpleMessageModel>();
            CloseModalAfterShowSuccess = false; HideResultPanel = false;
        }

        public BatchProcessResult_Model(SimpleMessageModel mm)
        {
            this.Messages.Add(mm);
        }
        public BatchProcessResult_Model(string newMessage, MessageViewType messageType)
        {
            if (!string.IsNullOrEmpty(newMessage))
            {
                Messages.Add(new SimpleMessageModel(newMessage, messageType));
            }
        }
        public bool HideResultPanel { get; set; }
        public override string ToString()
        {
            string result = string.Empty;
            foreach (var msg in this.Messages)
            {
                if (!string.IsNullOrEmpty(result))
                    result += "<br>";
                result += msg.Message;
            }
            return result;
        }
        public string ToStringStriped(string seperator = ";")
        {
            string result = string.Empty;
            foreach (var msg in this.Messages)
            {
                if (!string.IsNullOrEmpty(result))
                    result += seperator;
                result += msg.Message;
            }
            return result;
        }
    }

    public class NationalIdModel
    {
        [Required(ErrorMessage = "{0} ضروری است")]
        [RegularExpression(RegXPattern.NationalCode, ErrorMessage = "کد ملی عددی ده رقمی است")]
        [Display(Name = "کد ملی")]
        public string NationalId { get; set; }
    }
}
