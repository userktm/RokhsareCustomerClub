using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Utility
{
    public static class ArrayUtility
    {
        public static string MergeToString(this int[] a, string seperator = ",")
        {
            string res = string.Empty;
            foreach (var item in a)
            {
                if (res.Length > 0)
                    res = res + seperator;
                res += item.ToString();
            }
            return res;
        }

        public static string MergeToString(this string[] a)
        {
            string res = string.Empty;
            foreach (var item in a)
            {
                if (res.Length > 0)
                    res = res + ",";
                res += item.ToString();
            }
            return res;
        }
    }

    public static class DictionaryUtility
    {

        public static string MergeToString(this Dictionary<string, string> a)
        {
            string res = string.Empty;

            foreach (var item in a)
            {
                if (!string.IsNullOrEmpty(res))
                    res += ";";
                var k = item.Key.Replace(";", "^");
                var v = item.Value.Replace(";", "^");
                k = k.Replace(":", "&");
                v = v.Replace(":", "&");
                res += string.Format("{0}:{1}", k, v);
            }
            return res;
        }

        public static Dictionary<string, string> LoadFromString(string txt)
        {
            var d = new Dictionary<string, string>();
            var tp = txt.Split(';');
            foreach (var item in tp)
            {
                var ko = item.Split(':');
                var kk = ko[0].Replace("^", ";").Replace("&", ";");
                var kv = ko[1].Replace("^", ";").Replace("&", ";");
                d.Add(kk, kv);
            }
            return d;
        }
    }

    public static class CastUtility
    {
        public static T Cast<T>(this Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                propertyInfo.SetValue(x, value, null);
            }
            return (T)x;
        }
    }
}
