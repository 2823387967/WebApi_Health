using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class ScoreRequest : UserIDRequest
    {
        /// <summary>
        /// 积分类型
        /// </summary>
        [limitString(ErrorMessage = "积分类型错误", Limit = "Sport|Sleep|Eat")]
        public string ScoreType { get; set; }
    }
}