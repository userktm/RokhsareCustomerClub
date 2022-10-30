using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class Credit
    {
        public long CreditId { get; set; }
        public int CreditAmount { get; set; }
        public Nullable<long> ClubFactureId { get; set; }
        public Nullable<System.DateTime> CreditStartDate { get; set; }
        public Nullable<System.DateTime> CreditEndDate { get; set; }
        public Nullable<System.DateTime> CreditExpireDate { get; set; }
        public int TotalCreditNow { get; set; }
        public int CreditTypeId { get; set; }
        public int CreditStatusId { get; set; }
        public string CreditComment { get; set; }
        public Nullable<long> UserId { get; set; }
        public long Creator { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<long> Modifire { get; set; }
        public Nullable<System.DateTime> ModifireDate { get; set; }
        public virtual ClubFacture ClubFacture { get; set; }
        public virtual CreditStatus CreditStatu { get; set; }
        public virtual Credit Credit1 { get; set; }
        public virtual Credit Credit2 { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
    }
}
