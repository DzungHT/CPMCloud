using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CybertronFramework.Libraries
{
    public static class StringUtil
    {
        /// <summary>
        /// chuyen doi tu doi tuong sang chuoi Form data
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua object</typeparam>
        /// <param name="obj">Doi tuong onject</param>
        /// <returns>Tra ve String dang "bien1=a&bien2=b" </returns>
        public static string ObjectToFormString<T>(T obj)
        {
            StringBuilder sb = new StringBuilder();
            var proArrayT = typeof(T).GetProperties();
            for (int i = 0; i < proArrayT.Length; i++)
            {
                if(proArrayT[i].GetValue(obj) != null)
                {
                    sb.Append(i == 0 ? "" : "&" + proArrayT[i].Name + "=" + proArrayT[i].GetValue(obj));
                }
        
            }
            return sb.ToString();
        }

        public static string NVL(string s)
        {
            return String.IsNullOrEmpty(s) ? "" : s;
        }
    }
}
