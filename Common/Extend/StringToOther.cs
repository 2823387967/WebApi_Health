using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extend
{
    public static class StringConvert
    {
        /// <summary>
        /// String转换int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ParseInt(this string s)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// String转换DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime? ParseDateTime(this string s)
        {
            DateTime result;
            if (DateTime.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// String转换double
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double? ParseDouble(this string s)
        {
            double result;
            if (double.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// String转换Bool
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool? ParseBool(this string s)
        {
            bool result;
            if (bool.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                if (s == "0")
                {
                    return false;
                }
                else if (s == "1")
                {
                    return true;
                }
                return null;
            }
        }

        /// <summary>
        /// List<string>转换string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ParseString(this List<string> list)
        {
            bool flag = true;
            string result = "";
            foreach (var item in list)
            {
                if (item.IsNullOrEmpty())
                    continue;
                if (flag)
                {
                    result = item;
                    flag = false;
                }
                else
                {
                    result += "," + item;
                }
            }
            return result;
        }

        /// <summary>
        /// List<string>转换string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ParseString(this List<int> list)
        {
            bool flag = true;
            string result = "";
            foreach (var item in list)
            {
                if (flag)
                {
                    result = item.ToString();
                    flag = false;
                }
                else
                {
                    result += "," + item;
                }
            }
            return result;
        }
    }
}
