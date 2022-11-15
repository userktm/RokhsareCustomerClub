using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class User
    {
        public User()
        {
            this.BusinessUnits = new List<BusinessUnit>();
            this.BusinessUnits1 = new List<BusinessUnit>();
            this.Cards = new List<Card>();
            this.ClubFactures = new List<ClubFacture>();
            this.ClubFactures1 = new List<ClubFacture>();
            this.Credits = new List<Credit>();
            this.Credits1 = new List<Credit>();
            this.Credits2 = new List<Credit>();
            this.UserPlans = new List<UserPlan>();
            this.UserRoles = new List<UserRole>();
            this.Users1 = new List<User>();
            this.Users11 = new List<User>();
            this.Users12 = new List<User>();
            this.Users13 = new List<User>();
        }

        public long UserID { get; set; }
        public int BusinessUnitId { get; set; }
        public string NationalNumber { get; set; }
        public Nullable<long> CardID { get; set; }
        public string FullName { get; set; }
        public string AvatarImage { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string BankCardNumber { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> MarriageDate { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Active { get; set; }
        public int UserTypeID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<long> Creator { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<long> Modifier { get; set; }
        public string SecurityStamp { get; set; }
        public bool MobileNumberConfirmed { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string ReferrerCode { get; set; }
        public Nullable<long> ReferrerUserId { get; set; }
        public Nullable<bool> IsVerifiedAvatar { get; set; }
        public Nullable<long> VerifiedAvatarBy { get; set; }
        public virtual ICollection<BusinessUnit> BusinessUnits { get; set; }
        public virtual ICollection<BusinessUnit> BusinessUnits1 { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual Card Card { get; set; }
        public virtual ICollection<ClubFacture> ClubFactures { get; set; }
        public virtual ICollection<ClubFacture> ClubFactures1 { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<Credit> Credits1 { get; set; }
        public virtual ICollection<Credit> Credits2 { get; set; }
        public virtual ICollection<UserPlan> UserPlans { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<User> Users1 { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<User> Users11 { get; set; }
        public virtual User User2 { get; set; }
        public virtual ICollection<User> Users12 { get; set; }
        public virtual User User3 { get; set; }
        public virtual ICollection<User> Users13 { get; set; }
        public virtual User User4 { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
