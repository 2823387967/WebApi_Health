using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 根据OrderId获得信息
    /// </summary>
    public class GetInfoByOrderIdRequest : TokenRequest
    {
        /// <summary>
        /// 各种类的ID
        /// </summary>
        [Required(ErrorMessage = "请填写OrderId", AllowEmptyStrings = false)]
        public int OrderId { get; set; }
    }
}