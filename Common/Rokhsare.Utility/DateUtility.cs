using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Utility
{
    public struct PersianDayOfWeek
    {
        DayOfWeek _dayOfWeek;

        public PersianDayOfWeek(DayOfWeek dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
        }

        public int PersianDayOfWeekIndex
        {
            get
            {
                return _dayOfWeek.GetPersianDayOfWeekIndex();
            }
        }

        public override string ToString()
        {
            return _dayOfWeek.GetPersianDayOfWeek();
        }
    }

    public class DateModel
    {
        public string Area { get; set; }
        public DateTime? SelectedDate { get; set; }
        public int MaxYear { get; set; }
        public int MinYear { get; set; }

        public string FieldName { get; set; }
        public bool? Validate { get; set; }

        public DateModel()
        {
            Area = "";
            FieldName = "CreateDate";
            MaxYear = 1389;
            MinYear = 1350;
            SelectedDate = DateTime.Now;
        }
    }

    public class PersianDateModel
    {
        #region private fields
        DateTime _dt;
        PersianCalendar _pcal;
        #endregion

        #region Calculated Properties
        public int Day
        {
            get
            {
                return _pcal.GetDayOfMonth(_dt);
            }
            set
            {
                _dt = _pcal.ToDateTime(Year, Month, value, 0, 0, 0, 0);
            }
        }

        public int Month
        {
            get
            {
                return _pcal.GetMonth(_dt);
            }
            set
            {
                _dt = _pcal.ToDateTime(Year, value, Day, 0, 0, 0, 0);
            }
        }
        public string MonthTitle
        {
            get
            {
                return DateUtility.PersianMonth[(this.Month - 1)];
            }
        }
        public int Year
        {
            get
            {
                return _pcal.GetYear(_dt);
            }
            set
            {
                _dt = _pcal.ToDateTime(value, Month, Day, 0, 0, 0, 0);
            }
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return _dt.DayOfWeek;
            }
        }

        public PersianDayOfWeek PersianDayOfWeek
        {
            get
            {
                return new PersianDayOfWeek(this.DayOfWeek);
            }
        }
        #endregion

        #region Contructors
        public PersianDateModel()
        {
            _dt = DateTime.Now;
            _pcal = new PersianCalendar();
        }

        public PersianDateModel(DateTime dt)
        {
            _dt = dt;
            _pcal = new PersianCalendar();
        }

        public PersianDateModel(string persianDateString)
        {
            _dt = persianDateString.ToDateTime();
            _pcal = new PersianCalendar();
        }

        public PersianDateModel(int year, int month, int day)
        {
            _pcal = new PersianCalendar();
            _dt = _pcal.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
        #endregion

        public override string ToString()
        {
            if (_dt != null && _dt != DateTime.MinValue && _dt != DateTime.MaxValue)
                return string.Format("{0}/{1:00}/{2:00}", _pcal.GetYear(_dt), _pcal.GetMonth(_dt), _pcal.GetDayOfMonth(_dt));
            return string.Empty;
        }

        public DateTime ToDateTime()
        {
            return this._dt;
        }
    }

    public static class DateUtility
    {


        /// <summary>
        /// یک رشته تاریخ فارسی بدون جداکننده را گرفته و سعی در فرمت دهی آن می کند
        /// مثلا رشته زیر را گرفته و به شکل صحیح آن تبدیل می کند
        /// inputs  : 440213   , 13880701   , 139111     , 9121     , 91111    , 91611
        /// returns : 44/02/13 , 1388/07/01 , 1391/01/01 , 91/02/01 , 91/11/01 , 91/06/11
        /// </summary>
        /// <param name="persianDate"></param>
        /// <returns></returns>
        public static string ToDateFormat(this string persianDate)
        {
            int yy = 0, mm = 0, dd = 0;
            if (!string.IsNullOrEmpty(persianDate) && persianDate.Length > 3)
            {
                string md = string.Empty;
                if (persianDate.StartsWith("1"))
                {
                    yy = int.Parse(persianDate.Substring(0, 4));
                    md = persianDate.Substring(4);
                }
                else
                {
                    yy = int.Parse(persianDate.Substring(0, 2));
                    md = persianDate.Substring(2);
                }
                switch (md.Length)
                {
                    case 4:
                        mm = int.Parse(md.Substring(0, 2));
                        dd = int.Parse(md.Substring(2));
                        break;
                    case 2:
                        mm = int.Parse(md.Substring(0, 1));
                        dd = int.Parse(md.Substring(1));
                        break;
                    case 3:
                        if (md[0] < (char)50)
                        {
                            mm = int.Parse(md.Substring(0, 2));
                            dd = int.Parse(md.Substring(2));
                        }
                        else
                        {
                            mm = int.Parse(md.Substring(0, 1));
                            dd = int.Parse(md.Substring(1));
                        }
                        break;
                }
                return string.Format("{0}/{1:00}/{2:00}", yy, mm, dd);
            }
            return persianDate;
        }

        /// <summary>
        /// تبدیل یک رشته تاریخ فارسی به تاریخ میلادی
        /// yy/mm/dd
        /// </summary>
        /// <param name="persianDate">رشته تاریخ فارسی</param>
        /// <param name="time">در صورتی که بخواهیم زمان هم به تاریخ اضافه شود زمان هم وارد می کنیم وگرنه رشته خالی وارد می کنیم</param>
        /// <param name="seperator">مشخص می کنیم که بخش های تاریخ فارسی با چه جدا کننده ای جدا شده اند</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string persianDate, string time = "", char seperator = '/', bool EndDate = false)
        {
            DateTime now = DateTime.Now;
            string[] part = persianDate.Split(seperator);

            string[] partenglish = ConvertPersianToEnglishNumber(part);

            PersianCalendar pcal = new PersianCalendar();
            if (partenglish[0].Length <= 2) partenglish[0] = "13" + part[0];
            if (!string.IsNullOrEmpty(time))
                now = pcal.ToDateTime(partenglish[0].ToInt(), partenglish[1].ToInt(), partenglish[2].ToInt(), time.GetHour(), time.GetMinute(), 0, 0);
            else
            {
                if (EndDate == false)
                    now = pcal.ToDateTime(partenglish[0].ToInt(), partenglish[1].ToInt(), partenglish[2].ToInt(), 0, 0, 0, 0);
                else
                    now = pcal.ToDateTime(partenglish[0].ToInt(), partenglish[1].ToInt(), partenglish[2].ToInt(), 23, 59, 0, 0);
            }
            return now;
        }

        public static string[] ConvertPersianToEnglishNumber(string[] numberArray)
        {
            string[] v = new string[3];

            int itemArray = 0;
            foreach (var item in numberArray)
            {
                int count = 0;
                string finalitem = "";
                while (item.Length > count)
                {
                    string subitem = item.Substring(count, 1);

                    if (GetConvertNumber(subitem) != "False")
                        finalitem += GetConvertNumber(subitem);

                    count++;
                }

                v[itemArray] = finalitem;
                itemArray++;
            }

            return v;
        }

        public static string GetConvertNumber(string number)
        {
            switch (number)
            {
                case "۱":
                    return "1";
                case "۲":
                    return "2";
                case "۳":
                    return "3";
                case "۴":
                    return "4";
                case "۵":
                    return "5";
                case "۶":
                    return "6";
                case "۷":
                    return "7";
                case "۸":
                    return "8";
                case "۹":
                    return "9";
                case "۰":
                    return "0";
                case "1":
                    return "1";
                case "2":
                    return "2";
                case "3":
                    return "3";
                case "4":
                    return "4";
                case "5":
                    return "5";
                case "6":
                    return "6";
                case "7":
                    return "7";
                case "8":
                    return "8";
                case "9":
                    return "9";
                case "0":
                    return "0";
                default:
                    return "False";
            }
        }

        public static string ConvertNumber(this string number)
        {
            string final = "";

            for (int i = 0; i <= number.Count() - 1; i++)
            {
                string item = number.Substring(i, 1);
                switch (item)
                {
                    case "۱":
                        final += "1";
                        break;
                    case "۲":
                        final += "2";
                        break;
                    case "۳":
                        final += "3";
                        break;
                    case "۴":
                        final += "4";
                        break;
                    case "۵":
                        final += "5";
                        break;
                    case "۶":
                        final += "6";
                        break;
                    case "۷":
                        final += "7";
                        break;
                    case "۸":
                        final += "8";
                        break;
                    case "۹":
                        final += "9";
                        break;
                    case "۰":
                        final += "0";
                        break;
                    case "1":
                        final += "1";
                        break;
                    case "2":
                        final += "2";
                        break;
                    case "3":
                        final += "3";
                        break;
                    case "4":
                        final += "4";
                        break;
                    case "5":
                        final += "5";
                        break;
                    case "6":
                        final += "6";
                        break;
                    case "7":
                        final += "7";
                        break;
                    case "8":
                        final += "8";
                        break;
                    case "9":
                        final += "9";
                        break;
                    case "0":
                        final += "0";
                        break;
                    default:
                        final += item;
                        break;
                }
            }

            return final;
        }

        public static string ToPersianDateNumber(this string persianDate)
        {
            string final = "";

            for (int i = 0; i <= persianDate.Count() - 1; i++)
            {
                string item = persianDate.Substring(i, 1);
                switch (item)
                {
                    case "1":
                        final += "۱";
                        break;
                    case "2":
                        final += "۲";
                        break;
                    case "3":
                        final += "۳";
                        break;
                    case "4":
                        final += "۴";
                        break;
                    case "5":
                        final += "۵";
                        break;
                    case "6":
                        final += "۶";
                        break;
                    case "7":
                        final += "۷";
                        break;
                    case "8":
                        final += "۸";
                        break;
                    case "9":
                        final += "۹";
                        break;
                    case "0":
                        final += "۰";
                        break;
                    default:
                        final += item;
                        break;
                }
            }

            return final;
        }

        public static DateTime ConvertToDateTime(this String persianDate)
        {
            string temp = persianDate.ToString();
            char[] delimiterChars = { ' ', '\r', '\t', '\n', '"', '/' };
            string[] details = temp.Split(delimiterChars);
            var pCal = new System.Globalization.PersianCalendar();
            if (details.Length != 3)
                return DateTime.Now.AddYears(-10).AddMinutes(-10).AddDays(-10);
            else
            {
                try
                {
                    return pCal.ToDateTime(Convert.ToInt32(details[2]), Convert.ToInt32(details[1]), Convert.ToInt32(details[0]), 0, 0, 0, 0);
                }
                catch
                {
                    try
                    {
                        return pCal.ToDateTime(Convert.ToInt32(details[0]), Convert.ToInt32(details[1]), Convert.ToInt32(details[2]), 0, 0, 0, 0);
                    }
                    catch
                    {
                        return DateTime.MinValue;
                    }
                }
            }
        }

        /// <summary>
        /// تاریخ را به رشته تاریخ شمسی تبدیل می کند.
        /// </summary>
        /// <param name="dt">تاریخی که می خواهد فارسی شود</param>
        /// <param name="seperator">جدا کننده ای که برای نمایش تاریخ فارسی استفاده شود را مشخص می کنیم </param>
        /// <returns></returns>
        public static string ToPersianDate(this DateTime dt, string seperator = "/")
        {
            PersianCalendar pc = new PersianCalendar();
            string sy = pc.GetYear(dt).ToString();
            string sm = pc.GetMonth(dt).ToString("00");
            string sd = pc.GetDayOfMonth(dt).ToString("00");
            return sy + seperator + sm + seperator + sd;
        }

        public static string ToPersianDatePersianNumber(this DateTime dt, string seperator = "/")
        {
            PersianCalendar pc = new PersianCalendar();
            string sy = pc.GetYear(dt).ToString();
            string sm = pc.GetMonth(dt).ToString("00");
            string sd = pc.GetDayOfMonth(dt).ToString("00");
            return sy.ToPersianDateNumber() + seperator + sm.ToPersianDateNumber() + seperator + sd.ToPersianDateNumber();
        }

        public static string GetTime(this DateTime dt)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", dt.Hour, dt.Minute, dt.Second);
        }

        public static string ToPersianDayMonth(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            int sm = pc.GetMonth(dt);
            string sd = pc.GetDayOfMonth(dt).ToString("00");
            return string.Format("{0} {1}", sd, PersianMonth[(sm - 1)]);
        }

        public static string ToPersianDayMonthYear(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int sm = pc.GetMonth(dt);
            string sd = pc.GetDayOfMonth(dt).ToString("00");
            return string.Format("{0} {1} {2}", sd, PersianMonth[(sm - 1)], year);
        }

        public static int GetPersianYear(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dt);
        }

        public static int GetPersianMonth(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetMonth(dt);
        }

        public static int GetPersianDay(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetDayOfMonth(dt);
        }

        public static string ToPersinDateTimeSmartView(this DateTime dt)
        {
            var dn = DateTime.Now;
            if (dn.Year == dt.Year)
            {
                if (dn.Month == dt.Month)
                {
                    if (dn.Day == dt.Day)
                    {
                        if (dn.Minute == dt.Minute)
                            return string.Format("{0} ثانیه پیش", (dn.Second - dt.Second));
                        else
                        {
                            if (dn.Hour == dt.Hour)
                                return string.Format("{0} دقیقه پیش", (dn.Minute - dt.Minute));
                            else
                            {
                                if (dn.Hour - dt.Hour < 2)
                                    return string.Format("{0}:{1}", dt.Hour, dt.Minute);
                                else
                                    return string.Format("{0} ساعت قبل", (dn.Hour - dt.Hour));
                            }
                        }
                    }
                    else
                    {
                        switch (dn.DayOfYear - dt.DayOfYear)
                        {
                            case 1:
                                return "دیروز";
                                break;
                            case 2:
                                return "دو روز پیش";
                                break;
                            case 3:
                                return "سه روز پیش";
                                break;
                            case 4:
                                return "چهار روز پیش";
                                break;
                            case 5:
                                return "پنج روز پیش";
                                break;
                            case 6:
                                return "شش روز پیش";
                                break;
                            case 7:
                                return "یک هفته پیش";
                                break;
                            case 14:
                                return "دو هفته پیش";
                                break;
                            case 30:
                                return "یک ماه پیش";
                                break;
                            default:
                                return dt.ToPersianDayMonth();
                                break;
                        }
                    }
                }
                else
                    return dt.ToPersianDayMonthYear();
            }
            return dt.ToPersianDateTime();
        }

        public static string ToLittlePersianDate(this DateTime dt, string seperator = "/")
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{1}{0}{2}{0}{3}", seperator, pc.GetYear(dt) % 1300, pc.GetMonth(dt), pc.GetDayOfMonth(dt));
        }

        public static string[] PersianDays = { "شنبه", "یک شنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه" };

        public static DayOfWeek GetDayOfWeek(int year, int month, int day, bool showBreif = false)
        {
            PersianCalendar pcal = new PersianCalendar();
            return pcal.GetDayOfWeek(pcal.ToDateTime(year, month, day, 0, 0, 0, 0));
        }

        public static string GetPersianDayOfWeek(int year, int month, int day, bool showBreif = false)
        {
            var dw = GetDayOfWeek(year, month, day, showBreif);
            return GetPersianDayOfWeek(dw, showBreif);
        }

        public static int GetPersianDayOfWeekIndex(this DayOfWeek dw)
        {
            switch (dw)
            {
                case DayOfWeek.Saturday: return 0;
                case DayOfWeek.Sunday: return 1;
                case DayOfWeek.Monday: return 2;
                case DayOfWeek.Tuesday: return 3;
                case DayOfWeek.Wednesday: return 4;
                case DayOfWeek.Thursday: return 5;
                case DayOfWeek.Friday: return 6;
            }
            return -1;
        }

        public static string GetPersianDayOfWeek(this DayOfWeek dw, bool showBreif = false)
        {
            if (!showBreif)
                return PersianDays[dw.GetPersianDayOfWeekIndex()];
            else
                switch (dw)
                {
                    case DayOfWeek.Saturday: return "شنبه";
                    case DayOfWeek.Sunday: return "یک ";
                    case DayOfWeek.Monday: return "دو ";
                    case DayOfWeek.Tuesday: return "سه ";
                    case DayOfWeek.Wednesday: return "چهار";
                    case DayOfWeek.Thursday: return "پنج ";
                    case DayOfWeek.Friday: return "جمعه";
                }
            return "";
        }


        public static string[] PersianMonth = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                                                  "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
                                              };

        public static string ToPersianDateString(this DateTime dt, string seperator = " ")
        {
            PersianCalendar pc = new PersianCalendar();
            int m = pc.GetMonth(dt);
            return GetPersianDayOfWeek(dt.DayOfWeek) + " " + NumberToPersianText.num2str(pc.GetDayOfMonth(dt).ToString()) + seperator + PersianMonth[m - 1] + seperator + pc.GetYear(dt);
        }

        public static string ToPersianMonthDayString(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            int m = pc.GetMonth(dt);

            return pc.GetDayOfMonth(dt).ToString() + " " + PersianMonth[m - 1];
        }

        /// <summary>
        /// تاریخ شمسی را به همراه ساعت نمایش می دهد
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string ToPersianDateTime(this DateTime dt, string seperator = "/")
        {
            return dt.ToPersianDate(seperator) + " " + string.Format("{0:00}:{1:00}", dt.Hour, dt.Minute);
        }

        public static string ToPersianTime(this DateTime dt)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// محاسبه تفاوت دو تاریخ به دقیقه
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static int GetDifferenceFrom(this DateTime dt, DateTime delta)
        {
            int rs = 0;

            TimeSpan ts = delta.Subtract(dt);
            rs = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes;
            return rs;
        }

    }
}
