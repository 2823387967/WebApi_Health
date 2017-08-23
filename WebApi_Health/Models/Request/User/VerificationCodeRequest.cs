using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 验证码验证请求
    /// </summary>
    public class VerificationCodeRequest : UserIDRequest
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificaCode { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
    }
}