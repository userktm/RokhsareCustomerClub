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

    public class GetUserAmountModel
    {
        public int TotalCredit { get; set; }
        public int UsableTotalCredit { get; set; }
        public Nullable<int> LimitUseCreditResort { get; set; }
        public Nullable<int> LimitUseCreditForce { get; set; }
        public Nullable<int> LimitUserCreditPercent { get; set; }
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
}