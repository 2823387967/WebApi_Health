using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbOpertion.Models;

namespace WebApi_Health.Models.Response
{
    /// <summary>
    /// 获取数据字典表应答
    /// </summary>
    public class GetDataDictionaryResponse
    {
        /// <summary>
        /// 获取数据字典表应答构造函数
        /// </summary>
        public GetDataDictionaryResponse()
        {

        }
        /// <summary>
        /// 获取数据字典表应答构造函数
        /// </summary>
        public GetDataDictionaryResponse(DataDictionary datadic)
        {
            id = datadic.id;
            typeValue = datadic.typeValue;
        }
        /// <summary>
        /// 字典Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 类型值
        /// </summary>
        public string typeValue { get; set; }
    }
}