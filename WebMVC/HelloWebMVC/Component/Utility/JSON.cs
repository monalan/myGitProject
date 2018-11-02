using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Utility
{
    /// <summary>
    /// 解析Json
    /// </summary>
    public class JSON
    {
        /// <summary>
        /// 反序列化Json，默认UTF8编码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 序列化Json，默认UTF8编码
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string Serialize(object jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject);
        }
    }
}
