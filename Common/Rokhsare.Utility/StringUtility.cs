using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rokhsare.Utility
{
    public static class StringUtility
    {

        #region Anti XSS
        /// <summary>
        /// یک متن اچ تی ام ال را گرفته و کلیه تگ های اسکریپت ان را حذف می کند
        /// </summary>
        /// <param name="htmltext"></param>
        /// <returns></returns>
        public static string RemoteXSSBlackList(this string htmltext)
        {
            XDocument html = XDocument.Parse(htmltext);
            foreach (var script in html.Descendants("script"))
            {
                script.ToString();
            }


            string temp = htmltext.Replace("<script>", " ");
            return temp;
        }
        #endregion

        #region strip html
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(this string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(this string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        public static string HtmlStrip(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            input = Regex.Replace(input, "<style>(.|\n)*?</style>", string.Empty);
            input = Regex.Replace(input, @"<xml>(.|\n)*?</xml>", string.Empty); // remove all <xml></xml> tags and anything inbetween.  
            return Regex.Replace(input, @"<(.|\n)*?>", string.Empty); // remove any tags but not there content "<p>bob<span> johnson</span></p>" becomes "bob johnson"
        }

        #endregion

        public static string ReverseString(this string txt)
        {
            char[] charArray = txt.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Adds control characters to the string for the parentheses to be shown correctly when displayed in the browser.
        /// </summary>
        /// <param name="name">The string to modify.</param>
        /// <returns></returns>
        public static string FixParentheses(string name)
        {
            const char arabicStart = (char)0x0600; // Arabic subrange beginning
            const char arabicEnd = (char)0x06FF; // Arabic subrange end
            const char arabicAStart = (char)0xFB50; // Arabic subrange beginning
            const char arabicAEnd = (char)0xFDFF; // Arabic subrange end
            const char arabicBStart = (char)0xFE70; // Arabic subrange beginning
            const char arabicBEnd = (char)0xFEFF; // Arabic subrange end
            const char LRM = (char)0x200E; // Left-to-Right mark
            const char RLM = (char)0x200F; // Right-to-Left mark
            int index = name.IndexOf("(");
            if (index > -1 && index + 1 < name.Length)
            {
                char c = name[index + 1];
                if ((c >= arabicStart && c <= arabicEnd) || (c >= arabicAStart && c <= arabicAEnd) ||
                    (c >= arabicBStart && c <= arabicBEnd))
                    return name.Replace("(", "(" + LRM);
                else
                    return name.Replace("(", "(" + RLM);

            }
            return name;
        }

        /// <summary>
        /// تبدیل یک رشته عددی به یک عدد صحیح
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }

        /// <summary>
        /// حذف کاراکترهای خطرناک در ارسال پارامتر به پایگاه داده
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveDangerousChars(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            string tmp = inputString.Replace("/", "").Replace("\\", "").Replace("--", "").Replace("=", "").Replace(">", "").Replace("<", "");
            tmp = tmp.Replace("#", "").Replace("%", "").Replace("'", "");
            tmp = tmp.Trim().TrimEnd().TrimStart();
            return tmp;
        }

        public static bool IsValidSearchString(this string inputString, int minLength = 2)
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(inputString) && inputString.Length > minLength)
            {
                string tmp = inputString.RemoveDangerousChars();
                if (inputString.Length > minLength)
                    rs = true;
            }
            return rs;
        }

        public static bool IsEnglishLetter(this string s)
        {
            string en = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (var c in s.ToCharArray())
            {
                if (!en.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static string StringNormalizer(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            var st = new StringBuilder(s.Replace("  ", " ").RemoveDangerousChars().CorrectFarsiChars());
            return st.ToString();
        }

        /// <summary>
        /// تبدیل یک سری حروف فارسی به یک کد استاندارد
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CorrectFarsiChars(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                string tmp = inputString.Replace("  ", " ").Trim();

                if (!string.IsNullOrEmpty(tmp))
                {
                    #region Correct 'ک'
                    tmp = tmp.Replace('\u0643', '\u06A9'); //ك 
                    tmp = tmp.Replace('\u06AA', '\u06A9'); //ڪ
                    tmp = tmp.Replace('\u06AB', '\u06A9'); //ګ
                    tmp = tmp.Replace('\u06AC', '\u06A9'); //ڬ
                    tmp = tmp.Replace('\u06AD', '\u06A9'); //ڭ
                    tmp = tmp.Replace('\u06AE', '\u06A9'); //ڮ
                    #endregion

                    #region Correct 'ي'
                    //tmp = tmp.Replace('\u0626', '\u064A'); //ئ
                    tmp = tmp.Replace('\u06CC', '\u064A'); //ی  
                    tmp = tmp.Replace('\u06CD', '\u064A'); //ۍ
                    tmp = tmp.Replace('\u06CE', '\u064A'); //ێ
                    tmp = tmp.Replace('\u06D0', '\u064A'); //ې
                    tmp = tmp.Replace('\u06D1', '\u064A'); //ۑ
                    #endregion

                    #region Correct 'گ'
                    tmp = tmp.Replace('\u06B0', '\u06AF'); //ڰ 
                    tmp = tmp.Replace('\u06B1', '\u06AF'); //ڱ
                    tmp = tmp.Replace('\u06B2', '\u06AF'); //ڲ
                    tmp = tmp.Replace('\u06B3', '\u06AF'); //ڳ
                    tmp = tmp.Replace('\u06B4', '\u06A9'); //ڴ
                    #endregion
                }
                return tmp;
            }
            return inputString;
        }

        /// <summary>
        /// تبدیل یک  حرف فارسی به یک کد استاندارد
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static char CorrectFarsiChar(this char inputChar)///////////////توسط ترابی
        {
            if (!string.IsNullOrEmpty(inputChar.ToString()))
            {
                string tmp = (inputChar.ToString().RemoveDangerousChars());

                if (!string.IsNullOrEmpty(tmp))
                {
                    #region Correct 'ک'
                    tmp = tmp.Replace('\u0643', '\u06A9'); //ك 
                    tmp = tmp.Replace('\u06AA', '\u06A9'); //ڪ
                    tmp = tmp.Replace('\u06AB', '\u06A9'); //ګ
                    tmp = tmp.Replace('\u06AC', '\u06A9'); //ڬ
                    tmp = tmp.Replace('\u06AD', '\u06A9'); //ڭ
                    tmp = tmp.Replace('\u06AE', '\u06A9'); //ڮ
                    #endregion

                    #region Correct 'ي'
                    //tmp = tmp.Replace('\u0626', '\u064A'); //ئ
                    tmp = tmp.Replace('\u06CC', '\u064A'); //ی
                    tmp = tmp.Replace('\u06CD', '\u064A'); //ۍ
                    tmp = tmp.Replace('\u06CE', '\u064A'); //ێ
                    tmp = tmp.Replace('\u06D0', '\u064A'); //ې
                    tmp = tmp.Replace('\u06D1', '\u064A'); //ۑ
                    #endregion

                    #region Correct 'گ'
                    tmp = tmp.Replace('\u06B0', '\u06AF'); //ڰ 
                    tmp = tmp.Replace('\u06B1', '\u06AF'); //ڱ
                    tmp = tmp.Replace('\u06B2', '\u06AF'); //ڲ
                    tmp = tmp.Replace('\u06B3', '\u06AF'); //ڳ
                    tmp = tmp.Replace('\u06B4', '\u06A9'); //ڴ
                    #endregion
                }
                return Convert.ToChar(tmp[0]);
            }
            return inputChar;
        }

        /// <summary>
        /// معتبر بودن کد ملی را چک می کند
        /// </summary>
        /// <param name="nationalCode"></param>
        /// <returns></returns>
        public static bool IsValidNationalCode(this string nationalCode)
        {

            if (!string.IsNullOrEmpty(nationalCode))
            {
                if (nationalCode[0] == 'F' || nationalCode.Length == 11)
                    return true;

                if (nationalCode.Length < 10)
                {
                    while (nationalCode.Length == 10)
                        nationalCode = "0" + nationalCode;
                }

                if (!Regex.IsMatch(nationalCode, RegXPattern.NationalCode))
                    return false;

                if (nationalCode == "1111111111" || nationalCode == "0000000000" || nationalCode == "2222222222" || nationalCode == "3333333333" || nationalCode == "4444444444" ||
                    nationalCode == "5555555555" || nationalCode == "6666666666" || nationalCode == "7777777777" || nationalCode == "8888888888" || nationalCode == "9999999999")
                {
                    return false;
                }
                int c = int.Parse((nationalCode[9]).ToString());
                int n = int.Parse(nationalCode[0].ToString()) * 10 +
                    int.Parse(nationalCode[1].ToString()) * 9 +
                     int.Parse(nationalCode[2].ToString()) * 8 +
                     int.Parse(nationalCode[3].ToString()) * 7 +
                    int.Parse(nationalCode[4].ToString()) * 6 +
                     int.Parse(nationalCode[5].ToString()) * 5 +
                     int.Parse(nationalCode[6].ToString()) * 4 +
                     int.Parse(nationalCode[7].ToString()) * 3 +
                     int.Parse(nationalCode[8].ToString()) * 2;
                int r = n - (n / 11) * 11;
                if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// معتبر بودن ایمیل را چک می کند
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string email)
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    Regex reg = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|ir|ac|biz|info|mobi|name|aero|asia|jobs|museum)\b");
                    rs = reg.IsMatch(email.ToLower());
                }
                catch (Exception)
                {
                    rs = false;
                }
            }
            return rs;
        }

        public static bool IsValidMobileNumber(this string mobile)
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(mobile))
            {
                try
                {
                    Regex reg = new Regex(RegXPattern.MobilePhone);
                    rs = reg.IsMatch(mobile);
                }
                catch (Exception)
                {
                    rs = false;
                }
            }
            return rs;
        }

        public static bool IsNumeric(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return false;
            bool rs = true;
            foreach (var c in txt.ToCharArray())
            {
                if (c < '0' || c > '9')
                    rs = false;
            }
            return rs;
        }

        public static string GetFirstLetters(this string str, int len, string addEndStr = "...")
        {
            if (str.Length > len)
                return str.Substring(0, len) + addEndStr;
            return str;
        }

        public static string FixLength(this string str, int len, bool striphtml = true)
        {
            if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrEmpty(str))
            {
                var txt = str;
                if (striphtml)
                    txt = StringUtils.StripTags(txt);
                if (txt.Length > len)
                    txt = txt.Substring(0, len) + "...";
                return txt;
            }
            return string.Empty;
        }

        public static string MergeStringArray(this string[] sa, string seperator = ",")
        {
            string rs = "";
            for (int i = 0; i < sa.Length; i++)
            {
                rs = rs + sa[i];
                if (i < (sa.Length - 1))
                    rs += seperator;
            }
            return rs;
        }

        #region MoneyUtility

        public static string RialToToman(this double price)
        {
            if (price == 0 || price == null)
                return (0).ToString();
            return (price / 10).ToString("#,###");
        }
        #endregion

        #region DoubleUtility
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
        #endregion
    }
}
