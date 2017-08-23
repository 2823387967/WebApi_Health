using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common.Attribute;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 通过页码和Id获取信息
    /// </summary>
    public class GetInfoByIdPaggingRequest : TokenRequest
    {
        /// <summary>
        /// 各种类的ID
        /// </summary>
        [Required(ErrorMessage = "请填写Id", AllowEmptyStrings = false)]
        public int Id { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        [PageNoValid]
        public int PageNo { get; set; }
    }
}