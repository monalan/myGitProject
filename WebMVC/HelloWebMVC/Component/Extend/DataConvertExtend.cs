using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Component.Extend
{
    /// <summary>
    /// 自定义的类型转换扩展方法，后期可以根据需要自行添加
    /// </summary>
    public static class DataConvertExtend
    {
        #region 基本类型转换

        public static bool ToBoolean(this int src)
        {
            return Convert.ToBoolean(src);
        }

        public static int ToInt32(this bool src)
        {
            return Convert.ToInt32(src);
        }

        public static int ToInt32(this string src, bool tryparse = true)
        {
            int result = 0;

            if (tryparse)
                int.TryParse(src, out result);
            else
                int.Parse(src);

            return result;
        }

        public static int ToInt32(this string src, int defval)
        {
            int result = 0;

            if (!int.TryParse(src, out result))
                result = defval;

            return result;
        }

        public static int ToInt32(this long src)
        {
            return Convert.ToInt32(src);
        }

        public static int? ToNullableInt32(this string src)
        {
            int temp = 0;
            int? result = null;

            if (int.TryParse(src, out temp))
                result = temp;

            return result;
        }

        public static long ToInt64(this string src, bool tryparse = true)
        {
            long result = 0;

            if (tryparse)
                long.TryParse(src, out result);
            else
                long.Parse(src);

            return result;
        }

        public static long? ToNullableInt64(this string src)
        {
            long temp = 0;
            long? result = null;

            if (long.TryParse(src, out temp))
                result = temp;

            return result;
        }

        public static float ToSingle(this long src)
        {
            return Convert.ToSingle(src);
        }

        public static float? ToNullableSingle(this string src)
        {
            float temp = 0;
            float? result = null;

            if (float.TryParse(src, out temp))
                result = temp;

            return result;
        }

        public static double ToDouble(this string src, bool tryparse = true)
        {
            double result = 0;

            if (tryparse)
                double.TryParse(src, out result);
            else
                double.Parse(src);

            return result;
        }

        public static double? ToNullableDouble(this string src)
        {
            double temp = 0;
            double? result = null;

            if (double.TryParse(src, out temp))
                result = temp;

            return result;
        }

        public static bool ToBoolean(this string src)
        {
            return src.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public static bool? ToBoolean(this string src, bool nullable)
        {
            bool? result = null;

            if (nullable)
            {
                if (!string.IsNullOrWhiteSpace(src))
                {
                    bool temp = false;

                    if (bool.TryParse(src, out temp))
                        result = temp;
                }
            }
            else
            {
                result = src.ToBoolean();
            }

            return result;
        }

        /// <summary>
        /// 将字符串转换为指定的枚举值，如果该枚举支持位操作的话，那么将会返回所有枚举选项进行位与操作后的组合值，否则返回所有选项进行位与后的整型值
        /// </summary>
        /// <param name="src">待转换的值</param>
        /// <returns>枚举的值</returns>
        public static T ToEnum<T>(this string src) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type.");

            var value = 0;
            var name = string.Empty;

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if (src.Contains(item.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    value = value | (int)item;
                }
            }

            return (T)((object)value);
        }

        public static DateTime ToDateTime(this string src, bool tryparse = true)
        {
            DateTime result = DateTime.Now;

            if (tryparse)
                DateTime.TryParse(src, out result);
            else
                DateTime.Parse(src);

            return result;
        }

        public static DateTime ToDateTime(this object src)
        {
            return Convert.ToDateTime(src);
        }

        #endregion

        #region 基本类型判断

        /// <summary>
        /// 判断指定类型是否为数值类型
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>是否是数值类型</returns>
        public static bool IsNumeric(this Type type)
        {
            return type == typeof(Byte)
                || type == typeof(Int16)
                || type == typeof(Int32)
                || type == typeof(Int64)
                || type == typeof(SByte)
                || type == typeof(UInt16)
                || type == typeof(UInt32)
                || type == typeof(UInt64)
                || type == typeof(Decimal)
                || type == typeof(Double)
                || type == typeof(Single);
        }

        #endregion

        #region 配置文件获取

        public static string AppSetting(this string key, string defval = "")
        {
            string result;

            try
            {
                result = ConfigurationManager.AppSettings[key];

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = defval;
                }
            }
            catch
            {
                result = defval;
            }

            return result;
        }

        public static string ConnectionStrings(this string key)
        {
            string result;

            try
            {
                result = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            catch
            {
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region 泛型类型转换

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要转化的源对象</param>
        /// <returns>转化后的指定类型的对象，转化失败返回类型的默认值</returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }

            return (T)result;
        }

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要转化的源对象</param>
        /// <param name="defaultValue">转化失败返回的指定默认值</param>
        /// <returns>转化后的指定类型对象，转化失败时返回指定的默认值</returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = defaultValue;
            }

            return (T)result;
        }

        /// <summary>
        /// 将集合向下转换，即将抽象的集合转为具体的实现集合
        /// </summary>
        /// <typeparam name="Tout">输出类型</typeparam>
        /// <typeparam name="Tin">输入类型</typeparam>
        /// <param name="sources">数据源</param>
        /// <returns>转换后的数据</returns>
        public static IList<Tout> CastToSubtype<Tin, Tout>(this IEnumerable<Tin> sources) where Tout : Tin
        {
            var result = new List<Tout>();

            foreach (var item in sources)
            {
                result.Add((Tout)item);
            }

            return result;
        }

        #endregion

        #region 反射类型相关转换

        /// <summary>
        /// 获取枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns> </returns>
        public static string ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] members = type.GetMember(enumeration.CastTo<string>());
            if (members.Length > 0)
            {
                return members[0].ToDescription();
            }
            return enumeration.CastTo<string>();
        }

        /// <summary>
        /// 获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string ToDescription(this MemberInfo member, bool inherit = false)
        {
            DescriptionAttribute desc = member.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? null : desc.Description;
        }

        /// <summary>
        /// 检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool AttributeExists<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Any(m => (m as T) != null);
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).SingleOrDefault() as T;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        #endregion

        #region 自定义类型转换

        public static JsonMessage ToJsonMessage(this OperationResult src)
        {
            return new JsonMessage()
            {
                success = src.ResultType == OperationResultType.Success,
                message = src.Message ?? src.ResultType.ToDescription(),
                append = src.AppendData
            };
        }

        #endregion

        #region 字符串操作

        public static bool Contains(this string src, string dest, StringComparison comparison)
        {
            return src.IndexOf(dest, comparison) >= 0;
        }

        #endregion
    }
}
