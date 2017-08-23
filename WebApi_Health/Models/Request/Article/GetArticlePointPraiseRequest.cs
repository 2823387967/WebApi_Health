using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class GetArticlePointPraiseRequest : UserIDRequest
    {
        /// <summary>
        /// 操作
        /// </summary>
        [OpertionValid(Delete = true, Insert = true, ErrorMessage = "请输入正确的操作")]
        public string Opertion { get; set; }
        /// <summary>
        /// 对应Id
        /// </summary>
        [Required(ErrorMessage = "请填写对应Id", AllowEmptyStrings = false)]
        public int OtherId { get; set; }
    }
}