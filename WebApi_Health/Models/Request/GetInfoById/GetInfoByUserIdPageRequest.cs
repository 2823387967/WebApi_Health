using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common.Attribute;
using WebApi_Health.BLL.Attribute;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 根据用户Id与页码获得用户信息
    /// </summary>
    public class GetInfoByUserIdPageRequest : UserIDRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        [PageNoValid]
        public int PageNo { get; set; }
    }
}