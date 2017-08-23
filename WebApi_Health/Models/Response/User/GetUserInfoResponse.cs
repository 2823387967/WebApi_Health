
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Result;
using WebApi_Health.BLL.Cache;
using System.Configuration;
using Common.Extend;

namespace WebApi_Health.Models.Response
{
    public class GetUserInfoResponse
    {
        public GetUserInfoResponse(Customer user, string host)
        {
            //用户Id
            UserId = user.id;
            //用户名
            name = user.name;
            //用户手机
            phone = user.phone;
            //用户身高
            height = user.height == null ? "160" : user.height;
            //用户体重
            weight = user.weight == null ? "60" : user.weight;
            //用户性别
            sex = user.sex.GetValueOrDefault();
            //用户生日
            BirthDay = user.birthday.GetValueOrDefault();
            //年龄
            int now = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
            int dob = int.Parse(user.birthday.GetValueOrDefault().ToString("yyyyMMdd"));
            string dif = (now - dob).ToString();
            age = "0";
            if (dif.Length > 4)
                age = dif.Substring(0, dif.Length - 4);
            //劳动强度
            labourIntensity = user.labourIntensity;
            //体质
            constitution = user.constitution;
            //分数
            score = user.score.GetValueOrDefault();
            //用户头像
            if (user.HeadImage != null)
            {
                var ArrayHead = user.HeadImage.Split('/');
                if (ArrayHead.Count() > 0 && (ArrayHead[0].ToLower() == "http:" || ArrayHead[0].ToLower() == "https:"))
                {
                    HeadImage = user.HeadImage;
                }
                else
                {
                    HeadImage = "http://" + host + "/" + user.HeadImage;
                }
            }
        }
        public GetUserInfoResponse(Customer user, Token token, string host)
        {
            //用户Id
            UserId = user.id;
            //用户名
            name = user.name;
            //用户手机
            phone = user.phone;
            //用户身高
            height = user.height;
            //用户体重
            weight = user.weight;
            //用户性别
            sex = user.sex.GetValueOrDefault();
            //用户生日
            BirthDay = user.birthday.GetValueOrDefault();
            //年龄
            int now = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
            int dob = int.Parse(user.birthday.GetValueOrDefault().ToString("yyyyMMdd"));
            string dif = (now - dob).ToString();
            age = "0";
            if (dif.Length > 4)
                age = dif.Substring(0, dif.Length - 4);
            //劳动强度
            labourIntensity = user.labourIntensity;
            //体质
            constitution = user.constitution;
            //分数
            score = user.score.GetValueOrDefault();
            //用户头像
            if (user.HeadImage != null)
            {
                var ArrayHead = user.HeadImage.Split('/');
                if (ArrayHead.Count() > 0 && (ArrayHead[0].ToLower() == "http:" || ArrayHead[0].ToLower() == "https:"))
                {
                    HeadImage = user.HeadImage;
                }
                else
                {
                    HeadImage = "http://" + host + "/" + user.HeadImage;
                }
            }

            //用户Token
            Token = token.GetToken();
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public String phone { get; set; }
        /// <summary>
        /// 用户身高
        /// </summary>
        public String height { get; set; }
        /// <summary>
        /// 用户体重
        /// </summary>
        public String weight { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public Boolean sex { get; set; }
        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 劳动强度
        /// </summary>
        public String labourIntensity { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String HeadImage { get; set; }
        /// <summary>
        /// 体质
        /// </summary>
        public String constitution { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public Int32 score { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }
        /// <summary>
        /// 用户Token
        /// </summary>
        public string Token { get; set; }
    }
}