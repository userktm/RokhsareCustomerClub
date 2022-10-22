using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rokhsare.Service.Models
{
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

    public class SignupModel
    {
        public string Token { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string NationalId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Birthdate { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class FileResults
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Medianame { get; set; }
        public string Extention { get; set; }
        public string Address { get; set; }
    }
}