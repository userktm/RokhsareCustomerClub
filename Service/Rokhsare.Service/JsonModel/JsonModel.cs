﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rokhsare.Service.JsonModel
{
    public class FactureViewJsonModel
    {
        public Nullable<int> UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public int FactureTypeId { get; set; }
        public int ProductTypeId { get; set; }
        public int FactureId { get; set; }
        public Nullable<System.DateTime> FactureDate { get; set; }
        public int FacturePrice { get; set; }
        public int UserPayment { get; set; }
        public int ProductPrice { get; set; }
        public string ProductId { get; set; }
        public Nullable<int> ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public Nullable<int> Creator { get; set; }
        public string CreatorName { get; set; }
    }

    public class UseCreditFactureJsonModel
    {
        public List<FactureViewJsonModel> Factures { get; set; }
        public int CreditPrice { get; set; }
    }

    public class UserAmountJsonModel
    {
        public string UserMobile { get; set; }
        public string UserCode { get; set; }
    }
}