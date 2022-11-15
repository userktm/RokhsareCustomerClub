using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class BusinessUnit
    {
        public BusinessUnit()
        {
            this.Branches = new List<Branch>();
            this.BusinessUnitNetworks = new List<BusinessUnitNetwork>();
            this.BusinesUnitClubPlans = new List<BusinesUnitClubPlan>();
            this.ClubFactures = new List<ClubFacture>();
            this.DefaultClubPlans = new List<DefaultClubPlan>();
            this.ProductGroups = new List<ProductGroup>();
            this.Products = new List<Product>();
            this.Users = new List<User>();
            this.ClubPlanDetails = new List<ClubPlanDetail>();
        }

        public int BusinessUnitId { get; set; }
        public string UnitName { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<long> Creator { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<long> Modifier { get; set; }
        public bool Active { get; set; }
        public bool SmsNotification { get; set; }
        public string SmsApiKey { get; set; }
        public int CurrencyId { get; set; }
        public int JobId { get; set; }
        public string InstagramId { get; set; }
        public bool IsUpdating { get; set; }
        public string Description { get; set; }
        public string AdviseNumber { get; set; }
        public Nullable<int> LimitUseCreditResort { get; set; }
        public Nullable<int> LimitUseCreditForce { get; set; }
        public Nullable<int> LimitUserCreditPercent { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<BusinessUnitNetwork> BusinessUnitNetworks { get; set; }
        public virtual ICollection<BusinesUnitClubPlan> BusinesUnitClubPlans { get; set; }
        public virtual ICollection<ClubFacture> ClubFactures { get; set; }
        public virtual ICollection<DefaultClubPlan> DefaultClubPlans { get; set; }
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ClubPlanDetail> ClubPlanDetails { get; set; }
    }
}
