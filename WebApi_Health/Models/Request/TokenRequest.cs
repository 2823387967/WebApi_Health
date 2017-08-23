using Common.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class TokenRequest
    {
        /// <summary>
        /// 用户Token
        /// </summary>
        [TokenValid(AllowEmpty = true)]
        public string Token { get; set; }
    }
}