using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common.Attribute.Constant;
using Common.Attribute;

namespace WebApi_Health.Models.Request
{
    public class SetUserBodyInfoRequest : UserIDRequest
    {
        /// <summary>
        /// 用户性别
        /// </summary>
        [BoolValid(ErrorMessage = "请填写用户性别", AllowEmpty = true)]
        public string UserSex { get; set; }
        /// <summary>
        /// 用户出生日期
        /// </summary>
        [DateTimeValid(ErrorMessage = "请填写用户出生日期", AllowEmpty = true)]
        public DateTime? UserBirthTime { get; set; }
        /// <summary>
        /// 用户身高
        /// </summary>
        [IntValid(ErrorMessage = "请填写用户身高", AllowEmpty = true)]
        public int? UserHeight { get; set; }
        /// <summary>
        /// 用户体重
        /// </summary>
        [IntValid(ErrorMessage = "请填写用户体重", AllowEmpty = true)]
        public int? UserWeight { get; set; }
        /// <summary>
        /// 劳动强度
        /// </summary>
        [limitString(ErrorMessage = "请输入正确的劳动强度", AllowEmpty = true)]
        public string labInten { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [limitString(ErrorMessage = "请输入正确用户头像地址", AllowEmpty = true)]
        public string HeadImage { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [limitString(ErrorMessage = "请输入正确的用户名", AllowEmpty = true)]
        public string UserName { get; set; }
    }
}