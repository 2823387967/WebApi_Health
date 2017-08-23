using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common.Extend;
using Common.Attribute;
using Common.Filter;
using WebApi_Health.BLL.Attribute;

namespace WebApi_Health.Models.Request
{
    public class UserIDRequest : TokenRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [UserIdValid]
        public int UserId { get; set; }
    }
}