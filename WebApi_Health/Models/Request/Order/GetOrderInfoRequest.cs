using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 获得订单请求
    /// </summary>
    public class GetOrderInfoRequest : TokenRequest
    {
        /// <summary>
        /// 经度
        /// </summary>
        [IntValid(ErrorMessage = "经度不能为空")]
        public double CoordX { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [IntValid(ErrorMessage = "纬度不能为空")]
        public double CoordY { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        [IntValid(ErrorMessage ="订单Id不能为空")]
        public int OrderId { get; set; }
    }
}