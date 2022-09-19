using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Utility
{
    public class BasePoco
    {
        public T ConvertTo<T>() where T : class, new()
        {
            var obj = new T();
            var pl = this.GetType().GetProperties().Where(p => p.PropertyType.Namespace == "System").ToList();
            var plo = obj.GetType().GetProperties().ToList();
            foreach (var item in pl)
                if (plo.Any(u => u.Name == item.Name))
                {
                    var v = item.GetValue(this, null);
                    if (v != null)
                    {
                        try
                        {
                            item.SetValue(obj, v, null);
                        }
                        catch
                        {

                        }
                    }
                }
            return obj;
        }
    }
}
