using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Utility
{
    public static class TimeUtility
    {
        /// <summary>
        /// تبدیل یک رشته حاوی ساعت به یک عدد صحیح چهار رقمی
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int ToIntTime(this string s)
        {
            return (s.GetHour() * 100) + s.GetMinute();
        }

        /// <summary>
        /// استخراج عدد ساعت از یک رشته حاوی ساعت و دقیقه
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int GetHour(this string s)
        {
            string[] sd = s.Split(':');
            if (sd.Length > 0)
                return sd[0].ToInt();
            else return 0;
        }

        /// <summary>
        /// استخراج عدد دقیقه از یک رشته حاوی ساعت و دقیقه
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int GetMinute(this string s)
        {
            string[] sd = s.Split(':');
            if (sd.Length > 1)
                return sd[1].ToInt();
            else return 0;
        }

        public static string Truetime(this string s)
        {
            string[] timed = s.Split(' ').ToArray();

            if (timed[1] == "am")
            {
                string[] subtimed = timed[0].Split(':').ToArray();
                if (subtimed[0] == "12")
                {
                    return "0:" + subtimed[1];
                }
                else
                {
                    return subtimed[0] + ":" + subtimed[1];
                }
            }
            else
            {
                string[] subtimed = timed[0].Split(':').ToArray();
                if (subtimed[0] != "12")
                {
                    int hourconverted = Convert.ToInt32(subtimed[0]) + 12;
                    return hourconverted.ToString() + ":" + subtimed[1];
                }
                else
                {
                    return subtimed[0] + ":" + subtimed[1];
                }
            }

            return "";
        }

        public static TimeSpan ToTime(this string s)
        {
            string[] timed = s.Split(':').ToArray();

            TimeSpan finaltime = new TimeSpan(Convert.ToInt32(timed[0]), Convert.ToInt32(timed[1]), 0);

            return finaltime;
        }
    }
}
