using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi_Health.BLL.Attribute;

namespace WebApi_Health.Models.Request
{
    public class ModifyUserPasswordRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [UserIdValid]
        public int UserId { get; set; }

        /// <summary>
        /// 用户老密码
        /// </summary>
        [PassWordValid(ErrorMessage = "用户密码验证不通过")]
        public string OldPassword { get; set; }

        /// <summary>
        /// 用户新密码
        /// </summary>
        [PassWordValid(ErrorMessage = "用户密码验证不通过")]
        public string NewPassword { get; set; }
    }
}