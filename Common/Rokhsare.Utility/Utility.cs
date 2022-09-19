using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Utility
{
    public class NumberUtils
    {
        public static int Round(float a)
        {
            var n = (int)a;
            var f = a - n;
            if (f >= 0.5)
            {
                n += 1;
            }
            return n;
        }

        public static int Round(double a)
        {
            var n = (int)a;
            var f = a - n;
            if (f >= 0.5)
            {
                n += 1;
            }
            return n;
        }
    }
    public class RandomUtils
    {
        static Random random = new Random();
        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
        static string randomStringRange = "abcdefghijkmnpqrstuvwxyz23456789";
        public static string RandomString(int len = 6)
        {
            string res = string.Empty;
            while (res.Length < len)
            {
                var r = RandomNumber(0, randomStringRange.Length);
                res += randomStringRange[r];
            }
            return res;
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            var dontwantChars = "iIloO0uvVU".ToArray();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                while (dontwantChars.Contains(ch))
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString().ToUpper();
        }

        public static string GenerateRandomCode(int len = 6)
        {
            string s = "";
            for (int i = 0; i < len; i++)
            {
                int tas = RandomNumber(0, 100);
                if (tas <= 50)
                    s = String.Concat(s, RandomNumber(2, 9));
                else
                    s = String.Concat(s, RandomString(1, false));
            }
            return s;
        }
    }
    public class DateUtils
    {
        public DateUtils()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool ValidateJilaliDate(string input, out DateTime jilaliDate)
        {
            jilaliDate = DateTime.MinValue;
            try
            {
                string[] dateSplits = input.Split(new char[] { '/' }, 3);
                int day, month, year;
                year = int.Parse(dateSplits[0]);
                month = int.Parse(dateSplits[1]);
                day = int.Parse(dateSplits[2]);
                if (day > 31 && year <= 31)
                {
                    int temp = year;
                    year = day;
                    day = temp;
                    string tempSplit = dateSplits[0];
                    dateSplits[0] = dateSplits[2];
                    dateSplits[2] = tempSplit;
                }
                if (dateSplits[0].Length <= 2)
                    year += 1300;

                PersianCalendar pCal = new PersianCalendar();
                DateTime date = pCal.ToDateTime(year, month, day, 0, 0, 0, 0);
                jilaliDate = date;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Gregorian2Jilali(DateTime date)
        {
            if (date != DateTime.MinValue)
            {
                PersianCalendar pCal = new PersianCalendar();
                return String.Format("{0}/{1}/{2}", pCal.GetYear(date),
                    pCal.GetMonth(date), pCal.GetDayOfMonth(date));
            }
            else
                return string.Empty;
        }
        public static string Gregorian2Jilali(DateTime dt, string seperator = "/")
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dt) + seperator + pc.GetMonth(dt) + seperator + pc.GetDayOfMonth(dt);
        }
        public static string JilaliYear(DateTime date)
        {
            if (date != DateTime.MinValue)
            {
                PersianCalendar pCal = new PersianCalendar();
                return String.Format("{0}", pCal.GetYear(date));
            }
            else
                return string.Empty;
        }

        public static int GetCurrentYear()
        {
            int year = Convert.ToInt32(DateUtils.JilaliYear(DateTime.Now));
            if (year > 100) year = year % 100;
            return year;
        }
        /// <summary>
        /// تبدیل تاریخ شمسی به میلادی
        /// </summary>
        /// <param name="date">yyyy/mm/dd</param>
        /// <param name="seperator">seperator default "/"</param>
        /// <returns></returns>
        public static DateTime Jalali2Gregorian(string date, string seperator = "/")
        {
            try
            {
                int indexFirst = date.IndexOf(seperator);
                int indexLast = date.LastIndexOf(seperator);
                string yy = date.Substring(0, indexFirst);
                string mm = date.Substring(indexFirst + 1, indexLast - (indexFirst + 1));
                string dd = date.Substring(indexLast + 1);
                PersianCalendar pc = new PersianCalendar();
                return pc.ToDateTime(int.Parse(yy), int.Parse(mm), int.Parse(dd), 0, 0, 0, 0);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime Jalali2Gregorian(int yy, int mm, int dd)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                return pc.ToDateTime(yy, mm, dd, 0, 0, 0, 0);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }
    public class FileUtils
    {

        public static string[] DangerousExtensionArray = new string[]{".386",".ADE",".ADP",".ADT",".APP",".ASP",".BAS",
                    ".BAT",".BIN",".BTM",".CBT",".CHM",".CLA",".CLASS",".CMD",".COM",".CPL",".CRT",".CSC",
                    ".CSS",".DLL",".DOT",".EML",".EMAIL",".EXE",".FON",".HLP",".HTA",".HTM",".HTML",
                    ".INF",".INI",".INS",".ISP",".JS",".JSE",".LIB",".LNK",".MDB",".MDE",".MHT",".MHTM",
                    ".MHTML",".MSO",".MSC",".MSI",".MSP",".MST",".OBJ",".OBJ",".OV?",".PCD",".PGM",".PIF",
                    ".PRC",".REG",".SCR",".SCT",".SHB",".SHS",".SMM",".ASM",".C",".CPP",".PAS",".BAS",".FOR",
                    ".ASP",".ASPX",".CS",".SYS",".URL",".VB",".VBE",".VBS",".VXD",".WSC",".WSF",".WSH",
                    ".XL?",".IATA",".IMDG",".h"};

        /// <summary>
        /// یک فایل متنی را کامل خوانده و بازمی گرداند .
        /// </summary>
        /// <param name="strFilePath">مسیر کامل فایل به همراه نام و پسوند فایل</param>
        /// <returns></returns>
        public static string ReadTextFile(string strFilePath)
        {
            StreamReader rd;
            string strOutput = "";

            rd = File.OpenText(strFilePath);

            while (rd.Peek() != -1)
                strOutput += rd.ReadLine();

            rd.Close();

            return strOutput;
        }
    }
    public class StringUtils
    {
        public static string GetSubString(string text, int length)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text) || length <= 0)
                return text;
            if (length >= text.Length)
                return text;
            string s = text.Substring(0, length);
            return s;
        }

        public static string StripTags(string HTML)
        {
            // Removes tags from passed HTML           
            if (string.IsNullOrEmpty(HTML))
                return string.Empty;
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");

            return objRegEx.Replace(HTML, "");
        }
    }
    public class NumberToPersianText
    {

        // Convert Num To String
        // http://aspcode.ir/Article.aspx?id=20

        private static string[] yekan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        //array[10..19]
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };


        private static string getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yekan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }

        public static string num2str(string snum)
        {
            string stotal = "";
            if (snum == "0")
            {
                return yekan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int L = snum.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(snum.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }
    public static class EnumHelper<T>
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
    public static class FileSize
    {
        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        private static readonly string[] SizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }
    }
}
