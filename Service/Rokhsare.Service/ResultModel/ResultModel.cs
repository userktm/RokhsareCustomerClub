using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rokhsare.Service
{
    public class ResultModel
    {
        public object Model { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }

    public class UploadResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public long FileId { get; set; }
    }

    public class TokenResult
    {
        public string Token { get; set; }
        public System.DateTime ExpirDateTime { get; set; }
    }

    #region Login
    public class LoginResult
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PositionJob { get; set; }
        public string Proficiency { get; set; }
        public string AboutMe { get; set; }
        public Nullable<int> ImageId { get; set; }
    }
    #endregion

    #region Working
    public class StopWorkingResult
    {
        public string Time { get; set; }
        public string StartTimeFinal { get; set; }
        public string EndTimeFinal { get; set; }
        public int MinHourtime { get; set; }
        public int MinMintime { get; set; }
        public int MaxHourtime { get; set; }
        public int MaxMintime { get; set; }
        public int MinSectime { get; set; }
        public int MaxSectime { get; set; }
    }
    #endregion

    #region Salary
    public class UserSalary
    {
        public string AppLevel { get; set; }
        public string AppUser { get; set; }
        public string SalaryBase { get; set; }
        public int Salary { get; set; }
        public int WorkingHoursDay { get; set; }
    }

    public class RequestCheckout
    {
        public long CheckoutId { get; set; }
        public string AppUser { get; set; }
        public long Amount { get; set; }
        public string CreateDate { get; set; }
        public string Status { get; set; }
    }
    #endregion

    #region FileManager
    public class GetFileResult
    {
        public long FileId { get; set; }
        public long UserId { get; set; }
        public string AddressFile { get; set; }
    }
    #endregion

    #region Report
    public class InsertReportWorkByAllTimeResult
    {
        public string AllTime { get; set; }
        public string Starttime { get; set; }
        public string Endtime { get; set; }
        public int StartTimeHour { get; set; }
        public int StartTimeMin { get; set; }
        public int EndTimeHour { get; set; }
        public int EndTimeMin { get; set; }
        public int MinHourGreen { get; set; }
        public int MinMinGreen { get; set; }
        public int MaxHourGreen { get; set; }
        public int MaxMinGreen { get; set; }
        public int MinHourRed { get; set; }
        public int MinMinRed { get; set; }
        public int MaxHourRed { get; set; }
        public int MaxMinRed { get; set; }
        public string Status { get; set; }
    }
    #endregion

    #region UserEvent
    public class GetUserEventResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte Typed { get; set; }
        public int Periority { get; set; }
        public byte Action { get; set; }
        public string Calender { get; set; }
        public string ReportWork { get; set; }
    }
    #endregion
}