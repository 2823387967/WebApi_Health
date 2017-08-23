using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 插入到店支付请求
    /// </summary>
    public class InsertPayAtShopRequest : GetInfoByRecipeIdRequest
    {
        /// <summary>
        /// 到店时间
        /// </summary>
        [DateTimeValid(ErrorMessage = "到店时间不能为空")]
        public DateTime AtShopTime { get; set; }
    }
}