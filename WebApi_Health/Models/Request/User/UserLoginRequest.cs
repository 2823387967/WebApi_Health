using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Attribute;
using System.Web;

namespace WebApi_Health.Models.Request
{
    //用户登入请求
    public class UserLoginRequest
    {
        /// <summary>
        /// 用户手机
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 登录模式(PassWord,WeChat,Token)
        /// </summary>
        [Required(ErrorMessage = "请填写登录模式", AllowEmptyStrings = false)]
        public string TransMode { get; set; }
        /// <summary>
        /// 用户UId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户图片
        /// </summary>
        public string UserImage { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string UserSex { get; set; }
        /// <summary>
        /// 用户Token
        /// </summary>
        public string Token { get; set; }
    }
}