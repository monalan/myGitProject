using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    /// <summary>
    /// 用于序列化成Json格式的消息，用于Ajax调用
    /// </summary>
    public class JsonMessage
    {
        #region 构造函数

        public JsonMessage() { }

        public JsonMessage(bool success)
        {
            this.success = success;
        }

        public JsonMessage(string message)
        {
            this.success = false;
            this.message = message;
        }

        public JsonMessage(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }

        #endregion

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 附加对象
        /// </summary>
        public object append { get; set; }
    }
}
