using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Enumeration
{
    public class GeneralEnums
    {
        //<summary>
        //وضعیت کلی رکوردهای اطلاعاتی
        //</summary>

        public enum FinallyResultValue
        {
            [Display(Name = "خطا")]
            Error = 0,
            [Display(Name = "صحیح")]
            Accept = 1,
        }

        public enum RecordType : byte
        {
            [Display(Name = "منتظر تایید")]
            Pending = 0,
            [Display(Name = "فعال")]
            Active = 1,
            [Display(Name = "مسدود")]
            Disabled = 2,
            [Display(Name = "نهایی و قفل شده")]
            Fixed = 4,
            [Display(Name = "ثبت نام با موبایل")]
            MobileSignUp = 8
        }

        public enum Priority : byte
        {
            [Display(Name = "پایین")]
            Low = 1,
            [Display(Name = "متوسط")]
            Normal = 2,
            [Display(Name = "بالا")]
            High = 3
        }

        /// <summary>
        /// وضعیت حذف
        /// </summary>
        public enum DeleteStatus : byte
        {
            [Display(Name = "حذف نشده")]
            NotDeleted = 0,
            [Display(Name = "حذف شده")]
            Deleted = 1,
            [Display(Name = "همه")]
            All = 2
        }

        public enum Gender : byte
        {
            [Display(Name = "مرد")]
            Man = 1,
            [Display(Name = "زن")]
            Woman = 2
        }

        public enum FileType : byte
        {
            [Display(Name = "تصویر اصلی")]
            Base = 1,
            [Display(Name = "گالری تصویر")]
            Gallery = 2
        }

        public enum ProductStatus : byte
        {
            [Display(Name = "منتظر تایید")]
            Pending = 0,
            [Display(Name = "فعال")]
            Active = 1,
            [Display(Name = "مسدود")]
            Disabled = 2,
            [Display(Name = "حذف شده")]
            Deleted = 4,
            [Display(Name = "پیش نویس")]
            Cached = 8
        }

        public enum ProductModelType : byte
        {
            [Display(Name = "محصولات")]
            Products = 1,
            [Display(Name = "صفحه پرداخت")]
            CheckOut = 2,
        }

        public enum CommodityModelType : byte
        {
            [Display(Name = "قوانین و مقررات")]
            TermsAndConditions = 1,
            [Display(Name = "تماس با ما")]
            ContactMe = 2,
            [Display(Name = "بازارچه آنلاین چیست")]
            AboutMe = 3,
            [Display(Name = "مجله خبری")]
            Blog = 4,
            [Display(Name = "خبر")]
            BlogDetail = 5,
        }

        public enum AdminModelType : byte
        {
            [Display(Name = "پروفایل")]
            Profile = 1,
            [Display(Name = "سفارشات")]
            Order = 2,
            [Display(Name = "فروشندگان")]
            User = 3,
            [Display(Name = "خریداران")]
            Member = 4,
            [Display(Name = "تراکنشات")]
            Payment = 5,
            [Display(Name = "تگ و دسته بندی ها")]
            Tag = 6,
            [Display(Name = "وبلاگ")]
            Content = 7,
            [Display(Name = "مدیریت فایل")]
            FileManager = 8,
            [Display(Name = "مدیریت اعلان ها")]
            Notification = 9,
            [Display(Name = "مدیریت تیکت ها")]
            Ticket = 10
        }

        public enum SellerModelType : byte
        {
            [Display(Name = "داشبورد")]
            Dashboard = 99,
            [Display(Name = "پروفایل")]
            Profile = 1,
            [Display(Name = "محصولات")]
            Products = 2,
            [Display(Name = "سفارشات")]
            Orders = 3,
            [Display(Name = "پرداختی ها")]
            Payments = 4,
            [Display(Name = "ایجاد محصول")]
            CreateProduct = 5,
            [Display(Name = "گروه مشخصات محصول")]
            PropertyGroup = 6,
            [Display(Name = "گروه محصولات")]
            ProductGroup = 7,
            [Display(Name = "مشخصات محصول")]
            ProductProperty = 8,
            [Display(Name = "مدیریت فایل ها")]
            FileManager = 9,
            [Display(Name = "تیکت")]
            Ticket = 10
        }

        public enum BuyerModelType : byte
        {
            [Display(Name = "پروفایل")]
            Profile = 1,
            [Display(Name = "آدرس های من")]
            Address = 2,
            [Display(Name = "سفارشات")]
            Orders = 3,
            [Display(Name = "پرداختی ها")]
            Payments = 4
        }

        public enum OrderStatus : byte
        {
            [Display(Name = "در انتظار پرداخت")]
            CheckOut = 1,
            [Display(Name = "پرداخت نشده")]
            IsNotPaid = 2,
            [Display(Name = "پرداخت شده")]
            IsPaid = 3,
            [Display(Name = "در انتظار وارد شدن اطلاعات دریافت کننده")]
            UserInformation = 4,
        }

        public enum OrderPayment : byte
        {
            [Display(Name = "درگاه payir")]
            Payir = 1,
            [Display(Name = "درگاه زرین پال")]
            Zarinpal = 2,
            [Display(Name = "پرداخت درب منزل")]
            PayingatHome = 3
        }

        public enum TransactionStatus : byte
        {
            [Display(Name = "پرداخت شده")]
            IsPaid = 1,
            [Display(Name = "پرداخت نشده")]
            IsNotPaid = 2
        }

        public enum TaxonomyType : byte
        {
            [Display(Name = "برچسب")]
            ContentTag = 1,
            [Display(Name = "دسته بندی")]
            ContentCategory = 2,
        }

        public enum UserType : byte
        {
            [Display(Name = "فروشنده")]
            User = 1,
            [Display(Name = "مشتری")]
            Member = 2,
            [Display(Name = "همه")]
            All = 3,
        }

        public enum TicketStatus : byte
        {
            [Display(Name = "در صف بررسی")]
            InProgress = 1,
            [Display(Name = "پاسخ داده شده")]
            Answered = 2,
            [Display(Name = "بسته شده")]
            Closed = 2,
        }

        public enum TicketDetailAnswerType : byte
        {
            [Display(Name = "کاربر")]
            User = 1,
            [Display(Name = "مدیر سایت")]
            Manager = 2
        }

        public enum NotificationType : byte
        {
            [Display(Name = "اطلاع رسانی")]
            Info = 1,
            [Display(Name = "خطا")]
            Danger = 2,
            [Display(Name = "هشدار")]
            Warning = 3,
            [Display(Name = "اعلان موفقیت")]
            Success = 4,
        }

        public enum NotificationStatus : byte
        {
            [Display(Name = "خوانده شده")]
            Read = 1,
            [Display(Name = "خوانده نشده")]
            UnRead = 2,
        }

        public enum IsStockStatus : byte
        {
            [Display(Name = "نو")]
            New = 1,
            [Display(Name = "استوک")]
            Stock = 2,
        }

        public enum ForgetPasswordStatus : byte
        {
            [Display(Name = "تغییر یافته")]
            Success = 1,
            [Display(Name = "باز نشده")]
            NotOpen = 2,
            [Display(Name = "خطا")]
            Failer = 2,
        }

        public enum AdminFileType : byte
        {
            [Display(Name = "گالری")]
            Gallery = 1,
            [Display(Name = "تصویر پروفایل")]
            ProfileImage = 2,
        }

        public enum SaveMode : byte
        {
            [Display(Name = "رایگان")]
            Free = 1,
            [Display(Name = "غیررایگان")]
            NotFree = 2,
        }
        public enum OrderTransferStatus : byte
        {
            [Display(Name = "تحویل گرفته شده")]
            Delivered = 1,
            [Display(Name = "تحویل گرفته نشده")]
            NotDelivered = 2,
        }

        public enum ScoreType : byte
        {
            [Display(Name = "وظایف مشخص شده")]
            SpecifiedTasks = 1,
            [Display(Name = "گزارش در FMS")]
            FmsReport = 2,
            [Display(Name = "گزارش یادگیری")]
            LearningReport = 3,
            [Display(Name = "ارائه بیشتر")]
            More = 4,
        }

        public enum EventHistoryReciveType : byte
        {
            [Display(Name = "شمسی")]
            sh = 1,
            [Display(Name = "قمری")]
            ic = 2,
            [Display(Name = "میلادی")]
            wc = 3,
        }

        public enum LeaveType : byte
        {
            [Display(Name = "ساعتی")]
            LeaveHoures = 1,
            [Display(Name = "روزانه")]
            LeaveDays = 2,
            [Display(Name = "هفتگی")]
            LeaveWeeks = 3,
        }

        public enum CompanyStatus : byte
        {
            [Display(Name = "غیرفعال")]
            Disable = 1,
            [Display(Name = "معلق")]
            Suspended = 2,
            [Display(Name = "آزمایشی")]
            Trial = 3,
            [Display(Name = "فعال")]
            Active = 4,
        }

        public enum LevelStatus : byte
        {
            [Display(Name = "غیرفعال")]
            Disable = 1,
            [Display(Name = "فعال")]
            Active = 2,
        }

        public enum SalaryBase : byte
        {
            [Display(Name = "روزانه")]
            SalaryDay = 1,
            [Display(Name = "ساعتی")]
            SalaryHour = 2,
        }

        public enum TransactionType : byte
        {
            [Display(Name = "ورودی")]
            Input = 1,
            [Display(Name = "خروجی")]
            Output = 2,
            [Display(Name = "دریافت سازمانی")]
            RecivedOrganization = 3,
            [Display(Name = "ارسال سازمانی")]
            SendedOrganization = 4,
        }

        public enum CheckoutStatus : byte
        {
            [Display(Name = "درخواست پرسنل")]
            Request = 1,
            [Display(Name = "در حال بررسی")]
            Pending = 2,
            [Display(Name = "رد شده")]
            Failed = 3,
            [Display(Name = "تایید")]
            Confirm = 4,
            [Display(Name = "پرداخت شده")]
            Paid = 5,
        }

        public enum ReportAdminScore : byte
        {
            [Display(Name = "دریافت کامل حقوق")]
            Full = 1,
            [Display(Name = "کسر از حقوق")]
            Deduction = 2,
            [Display(Name = "پاداش مازاد بر حقوق")]
            Reward = 3,
        }

        public enum EventPriority : byte
        {
            [Display(Name = "فوری")]
            Immediate = 1,
            [Display(Name = "مهم")]
            Important = 2,
            [Display(Name = "متوسط")]
            Medium = 3,
        }

        public enum TicketIncoming : byte
        {
            [Display(Name = "سوال")]
            Question = 1,
            [Display(Name = "جواب")]
            Answer = 2,
        }

        public enum TicketType : byte
        {
            [Display(Name = "زیاد")]
            High = 1,
            [Display(Name = "متوسط")]
            Middle = 2,
            [Display(Name = "کم")]
            Low = 3,
        }

        public enum PositionDashboard : int
        {
            [Display(Name = "بالا راست")]
            TopRight = 1,
            [Display(Name = "بالا چپ")]
            TopLeft = 2,
            [Display(Name = "میانه راست")]
            MiddleRight = 3,
            [Display(Name = "راست وسط")]
            MiddleCenter = 4,
            [Display(Name = "میانه چپ")]
            MiddleLeft = 5,
            [Display(Name = "پایین یک")]
            OneBottom = 6,
            [Display(Name = "پایین دو")]
            TwoBottom = 7,
        }

        public enum DaysOfWeek : byte
        {
            [Display(Name = "شنبه")]
            Saturday = 1,
            [Display(Name = "یکشنبه")]
            Sunday = 2,
            [Display(Name = "دوشنبه")]
            Monday = 3,
            [Display(Name = "سه شنبه")]
            Tuesday = 4,
            [Display(Name = "چهارشنبه")]
            Wednesday = 5,
            [Display(Name = "پنجشنبه")]
            Thursday = 6,
            [Display(Name = "جمعه")]
            Friday = 7,
        }

        public enum SalaryOvertimeBase : byte
        {
            [Display(Name = "بر اساس ساعت")]
            Hourly = 1,
            [Display(Name = "بر اساس تایم")]
            Timly = 2,
        }

        public enum GamificationTimeBase : byte
        {
            [Display(Name = "روزانه")]
            DayBase = 1,
            [Display(Name = "هفتگی")]
            WeekBase = 2,
            [Display(Name = "ماهانه")]
            MonthBase = 3,
            [Display(Name = "فصلی")]
            SeasonBase = 4,
            [Display(Name = "سالانه")]
            YearBase = 5,
        }

        public enum GamificationType : byte
        {
            [Display(Name = "بر اساس امتیاز")]
            PointsBase = 1,
            [Display(Name = "حل مسئله")]
            ResolventBase = 2,
        }
    }
}
