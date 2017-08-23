using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class GetUserPreferenceRequest : UserIDRequest
    {
        /// <summary>
        /// 喜欢的类型
        /// </summary>
        [limitString(ErrorMessage = "对应类型错误", Limit = "foodlike|foodunlike", AllowEmpty = false)]
        public string Type_Like { get; set; }
    }
}