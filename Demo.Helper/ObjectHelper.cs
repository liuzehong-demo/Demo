using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Helper
{
    /// <summary>
    /// object类型扩展
    /// </summary>
    public static partial class ObjectHelper
    {

        /// <summary>
        /// 判断对象是否是空
        /// </summary>
        /// <param name="input">源数据</param>
        /// <returns></returns>
        public static bool IsDbNullOrNull(this object input)
        {
            return input == null || input == DBNull.Value;
        }



        /// <summary>
        /// 判断对象是否是非空
        /// </summary>
        /// <param name="input">源数据</param>
        /// <returns></returns>
        public static bool IsNotDbNullOrNull(this object input)
        {
            return !input.IsDbNullOrNull();
        }

        /// <summary>
        /// 从对象中取得字符串
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string CString(this object input, string defaultValue)
        {
            if (input.IsDbNullOrNull())
                return defaultValue;
            return input.CString("");
        }


        /// <summary>
        /// 从对象中取得int数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwEx">出现异常时是否抛出</param>
        /// <returns></returns>
        public static int CInt(this object input, int defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
                return defaultValue;
            string inputStr = input.CString("").Trim();
            if (string.IsNullOrEmpty(inputStr))
                return defaultValue;
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                if (throwEx)
                    throw;
                else
                    return defaultValue;
            }
        }


        /// <summary>
        /// 从对象中取得int数据 取不到时,返回 null
        /// </summary>
        /// <param name="input">源数据</param>
        /// <returns></returns>
        public static int? CIntOrNull(this object input)
        {
            if (input.IsDbNullOrNull())
                return null;
            string inputStr = input.CString("").Trim();
            if (string.IsNullOrEmpty(inputStr))
                return null;
            int result;
            if (int.TryParse(inputStr, out result))
                return result;
            return null;
        }


        /// <summary>
        /// 从对象中取得int数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwEx">出现异常时是否抛出</param>
        /// <returns></returns>
        public static long CLong(this object input, long defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
                return defaultValue;
            string inputStr = input.CString("").Trim();
            if (string.IsNullOrEmpty(inputStr))
                return defaultValue;
            try
            {
                return Convert.ToInt64(input);
            }
            catch
            {
                if (throwEx)
                    throw;
                else
                    return defaultValue;
            }
        }



        /// <summary>
        /// 从对象中取得decimal数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwEx">出现异常时是否抛出</param>
        /// <returns></returns>
        public static decimal CDec(this object input, decimal defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
                return defaultValue;
            string inputStr = input.ToString().Trim();
            if (string.IsNullOrEmpty(inputStr))
                return defaultValue;
            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                if (throwEx)
                    throw;
                else
                    return defaultValue;
            }
        }

        /// <summary>
        /// 从对象中取得decimal数据 取不到时,返回 null
        /// </summary>
        /// <param name="input">输入值</param>
        /// <returns></returns>
        public static decimal? CDecOrNull(this object input)
        {
            if (input.IsDbNullOrNull())
                return null;
            string inputStr = input.ToString().Trim();
            if (string.IsNullOrEmpty(inputStr))
                return null;
            decimal result;
            if (decimal.TryParse(inputStr, out result))
                return result;
            return null;
        }

        /// <summary>
        /// 将对象转为double数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwEx">出现异常时是否抛出</param>
        /// <returns></returns>
        public static double CDouble(this object input, double defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
                return defaultValue;
            string inputStr = input.ToString().Trim();
            if (string.IsNullOrEmpty(inputStr))
                return defaultValue;
            try
            {
                return Convert.ToDouble(input);
            }
            catch
            {
                if (throwEx)
                    throw;
                else
                    return defaultValue;
            }
        }


        /// <summary>
        /// 将对象转为DateTime数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwEx">出现异常时是否抛出</param>
        /// <returns></returns>
        public static DateTime CDateTime(this object input, DateTime defaultValue, bool throwEx)
        {
            try
            {
                return Convert.ToDateTime(input);
            }
            catch
            {
                if (throwEx)
                    throw;
                return defaultValue;
            }
        }


        /// <summary>
        /// 将对象转为DateTime数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <returns></returns>
        public static DateTime? CDateTimeOrNull(this object input)
        {
            if (input.IsDbNullOrNull())
                return null;
            string inputStr = input.ToString().Trim();
            if (string.IsNullOrEmpty(inputStr))
                return null;
            try
            {
                return Convert.ToDateTime(input);
            }
            catch { return null; }


        }


        /// <summary>
        /// 从对象中取得bool数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwEx">出现异常时是否抛出</param>
        /// <returns></returns>
        public static bool CBoolean(this object input, bool defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
                return defaultValue;
            string inputStr = input.ToString().Trim();
            if (string.IsNullOrEmpty(inputStr))
                return defaultValue;

            try
            {
                return Convert.ToBoolean(input);
            }
            catch (Exception ex)
            {
                if (throwEx)
                {
                    throw ex;
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 从对象中取得bool数据
        /// </summary>
        /// <param name="input">源数据</param>
        /// <returns></returns>
        public static bool? CBooleanOrNull(this object input)
        {
            if (input.IsDbNullOrNull()) return null;
            return Convert.ToBoolean(input);
        }



        /// <summary>
        /// 返回GUID或null
        /// </summary>
        /// <param name="input">源数据</param>
        /// <returns></returns>
        public static Guid? CGuidOrNull(this object input)
        {
            if (input.IsDbNullOrNull())
                return null;
            try
            {
                return new Guid(input.ToString());
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 如果对象为空,取默认值 
        /// </summary>
        /// <param name="input">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static object GetValueOrDefault(this object input, object defaultValue)
        {
            return input == null ? defaultValue : input == DBNull.Value ? defaultValue : input;
        }

        /// <summary>
        /// 序列化对象为字符串
        /// </summary>
        /// <param name="input">源数据</param>
        /// <returns></returns>
        public static string SerializeToString(this object input)
        {
            IFormatter formatter = new BinaryFormatter();
            string result = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, input);

                byte[] byt = new byte[stream.Length];
                byt = stream.ToArray();

                result = Convert.ToBase64String(byt);
                stream.Flush();
            }
            return result;
        }

    }
}
