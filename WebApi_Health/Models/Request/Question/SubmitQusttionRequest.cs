using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi_Health.BLL.Attribute;

namespace WebApi_Health.Models.Request
{
    public class SubmitQusttionRequest : UserIDRequest
    {
        /// <summary>
        /// 答案
        /// </summary>
        [Required(ErrorMessage = "答案不能为空", AllowEmptyStrings = false)]
        public string Answer { get; set; }
    }
}