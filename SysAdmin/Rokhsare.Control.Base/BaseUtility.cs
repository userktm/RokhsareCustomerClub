using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Control.Base
{
    #region Enum
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            try
            {
                DisplayAttribute[] attributes =
                    (DisplayAttribute[])fi.GetCustomAttributes(
                    typeof(DisplayAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Name;
                else
                    return enumValue.ToString();
            }
            catch (Exception ex)
            {

            }
            return enumValue.ToString();
        }

        public static string GetName<T>(this Enum enumValue, byte Val)
        {
            string result = "";
            foreach (Enum item in Enum.GetValues(typeof(T)))
            {
                byte valueAsByte = Convert.ToByte(item);
                if (((Val & valueAsByte) == valueAsByte))
                {
                    result += item.GetDisplayName() + ",";
                }
            }
            return result;
        }

        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();


        }
    }
    #endregion
}
