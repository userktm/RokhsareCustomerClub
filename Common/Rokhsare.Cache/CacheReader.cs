using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rokhsare.Cache
{
    public class CacheHelper
    {
        public static void SetDataToCache(object data, string key, int duration = 60)
        {
            var _context = HttpContext.Current;
            if (data != null && _context != null)
            {
                _context.Cache.Insert(key, data, null, DateTime.Now.AddMinutes(duration), TimeSpan.Zero); // .FromMinutes(ExpiresAfterMin)
            }
        }

        public static void SetDataToCacheDay(object data, string key, int duration = 60)
        {
            var _context = HttpContext.Current;
            if (data != null && _context != null)
            {
                _context.Cache.Insert(key, data, null, DateTime.Now.AddDays(duration), TimeSpan.Zero); // .FromDays(ExpiresAfterDays)
            }
        }

        public static void SetDataToCacheMonth(object data, string key, int duration = 60)
        {
            var _context = HttpContext.Current;
            if (data != null && _context != null)
            {
                _context.Cache.Insert(key, data, null, DateTime.Now.AddMonths(duration), TimeSpan.Zero); // .FromMonths(ExpiresAfterMonths)
            }
        }

        public static void SetDataToCache(object data, int duration, string formatstring, params object[] args)
        {
            var key = string.Format(formatstring, args);
            SetDataToCache(data, key, duration);
        }
        public static object GetData(string key)
        {
            var _context = HttpContext.Current;
            if (_context != null && _context.Cache[key] != null)
                return _context.Cache[key];
            return null;
        }
        public static T GetData<T>(string key)
        {
            var _context = HttpContext.Current;
            if (_context != null && _context.Cache[key] != null)
                return (T)_context.Cache[key];
            return default(T);// null;
        }
        public static T GetData<T>(string formatstring, params object[] args)
        {
            return GetData<T>(string.Format(formatstring, args));
        }
        public static object GetData(string formatstring, params object[] args)
        {
            var key = string.Format(formatstring, args);
            return GetData(key);
        }
        public static void Remove(string key)
        {
            var _context = HttpContext.Current;
            if (_context != null && _context.Cache[key] != null)
                _context.Cache.Remove(key);
        }
        public static T FindDataWithKeyPattern<T>(string prefix, string keyPointer)
        {
            var d = FindDataWithKeyPattern(prefix, keyPointer);
            if (d != null)
                return (T)d;
            return default(T);//null
        }
        public static object FindDataWithKeyPattern(string prefix, string keyPointer)
        {
            var _context = HttpContext.Current;
            var ge = _context.Cache.GetEnumerator();
            while (ge.MoveNext())
            {
                var key = ge.Key.ToString();
                if (key.StartsWith(prefix) && key.Contains(keyPointer))
                    return GetData(key);
            }
            return null;
        }
        public static void RemoveAllKeyStartWith(string formatstring, params object[] args)
        {
            RemoveAllKeyStartWith(string.Format(formatstring, args));
        }
        public static void RemoveAllKeyStartWith(string keyPrefix)
        {
            var _context = HttpContext.Current;
            var ge = _context.Cache.GetEnumerator();
            while (ge.MoveNext())
            {
                if (ge.Key.ToString().StartsWith(keyPrefix))
                    _context.Cache.Remove(ge.Key.ToString());
            }
        }
        public static void Remove(string formatstring, params object[] args)
        {
            var key = string.Format(formatstring, args);
            Remove(key);
        }
    }
    public partial class CacheReader
    {
        public CacheReader(string key)
        {
            this.CacheKey = key;
        }
        public string CacheKey { get; set; }
        public void SetDataToCache(object obj, string id, int duration = 60)
        {
            CacheHelper.SetDataToCache(obj, duration, "{0}_{1}", CacheKey, id);
        }

        public object Get(string id)
        {
            var Key = string.Format("{0}_{1}", CacheKey, id);
            return CacheHelper.GetData(Key);
        }

        public T GetData<T>(string id) where T : class
        {
            var obj = (T)Get(id);
            return obj;
        }

        public void SetData<T>(T obj, string id)
        {
            SetDataToCache(obj, id);
        }
        public void RemoveCache(string key)
        {
            CacheHelper.Remove(key);
        }
        public void RemoveCache(string formatstring, params object[] args)
        {
            var key = string.Format(formatstring, args);
            CacheHelper.Remove(key);
        }
    }
}
