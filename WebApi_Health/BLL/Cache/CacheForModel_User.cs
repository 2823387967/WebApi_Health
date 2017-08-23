using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Linq;
using System.Collections.Generic;
using Common.Result;
using System;
using System.Configuration;
using Common.Extend;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelUser : SingleTon<CacheForModelUser>
    {
        /// <summary>
        /// token失效时间
        /// </summary>
        private int TokenInvalidOutTimeDays = ConfigurationManager.AppSettings["TokenInvalidOutTimeDays"].ToString().ParseInt().GetValueOrDefault();
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public Customer GetUserInfo(int UserId)
        {
            var UserInfoModel = CacheHelper.Instance.GetCache<Customer>("UserInfoModel_" + UserId);
            if (UserInfoModel == null)
            {
                int outTime = CacheHelper.Instance.UserCacheOutTime;
                Customer model = new Customer();
                model.id = UserId;
                UserInfoModel = CustomerOper.Instance.Select(model).FirstOrDefault();
                CacheHelper.Instance.SetCache("UserInfoModel_" + UserId, UserInfoModel, outTime);
            }
            return UserInfoModel;
        }

        /// <summary>
        /// 重设用户信息
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public void SetUserInfo(int UserId)
        {
            int outTime = CacheHelper.Instance.UserCacheOutTime;
            Customer model = new Customer();
            model.id = UserId;
            var UserInfoModel = CustomerOper.Instance.Select(model).FirstOrDefault();
            CacheHelper.Instance.SetCache("UserInfoModel_" + UserId, UserInfoModel, outTime);
        }

        /// <summary>
        /// 根据微信获取用户信息
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public Customer GetUserInfoByWeChat(string wechatId)
        {
            var UserInfoModel = CacheHelper.Instance.GetCache<Customer>("UserInfoForWeChatModel_" + wechatId);
            if (UserInfoModel == null)
            {
                int outTime = CacheHelper.Instance.UserCacheOutTime;
                Customer model = new Customer();
                model.wechat = wechatId;
                UserInfoModel = CustomerOper.Instance.Select(model).FirstOrDefault();
            }
            return UserInfoModel;
        }

        /// <summary>
        /// 根据用户名,密码获取用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public Customer GetUserInfoByPassWord(string UserPhone, string PassWord)
        {
            var UserInfoModel = CacheHelper.Instance.GetCache<Customer>("UserInfoModel_UserPhone=" + UserPhone);
            if (UserInfoModel == null)
            {
                int outTime = CacheHelper.Instance.UserCacheOutTime;
                Customer model = new Customer();
                model.phone = UserPhone;
                model.password = PassWord;
                UserInfoModel = CustomerOper.Instance.Select(model).FirstOrDefault();
            }
            else
            {
                if (UserInfoModel.password != PassWord)
                {
                    return null;
                }
            }
            return UserInfoModel;
        }

        /// <summary>
        /// 设置用户Token
        /// </summary>
        /// <param name="user">用户模型</param>
        /// <returns></returns>
        public Token SetUserToken(Customer user)
        {
            Token token = new Token();
            token.Payload = new Payload();
            token.Payload.exp = DateTime.Now.AddDays(TokenInvalidOutTimeDays).ToString();
            token.Payload.UserID = user.id;
            token.Payload.UserName = user.name;
            CacheHelper.Instance.SetCache("UserToken_" + token.Payload.UserID, token, TokenInvalidOutTimeDays * 24 * 60);
            return token;
        }

        /// <summary>
        /// 获取用户Token
        /// </summary>
        /// <param name="tokenString">tooken字符串</param>
        /// <returns></returns>
        public Token GetUserToken(string tokenString)
        {
            Token token = new Token();
            if (token.Validate(tokenString))
            {
                var tokenModel = CacheHelper.Instance.GetCache<Token>("UserToken_" + token.Payload.UserID);
                if (tokenModel != null && tokenString == tokenModel.GetToken())
                {
                    return tokenModel;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置用户注册验证码
        /// </summary>
        /// <param name="Phone">用户手机</param>
        /// <returns></returns>
        public string SetUserVerificationCode(string Phone)
        {
            CacheHelper.Instance.GetCache("UserVerificationCode_UserPhone=" + Phone);
            string VerificationCode = RandHelper.Instance.Number(6);
            CacheHelper.Instance.SetCache("UserVerificationCode_UserPhone=" + Phone, VerificationCode, 10);
            CacheHelper.Instance.SetCache("UserResetVerificationCode_UserPhone=" + Phone, VerificationCode, 1);
            return VerificationCode;
        }

        /// <summary>
        /// 用户注册验证码是否能重置
        /// </summary>
        /// <param name="Phone">用户手机</param>
        /// <returns></returns>
        public bool GetUserResetVerificationCode(string Phone)
        {
            var phone = CacheHelper.Instance.GetCache("UserResetVerificationCode_UserPhone=" + Phone);
            return phone == null;
        }

        /// <summary>
        /// 获得用户验证码
        /// </summary>
        /// <param name="Phone">用户手机</param>
        /// <returns></returns>
        public string GetUserVerificationCode(string Phone)
        {
            var PhoneString = CacheHelper.Instance.GetCache("UserVerificationCode_UserPhone=" + Phone);
            if (PhoneString == null)
            {
                return null;
            }
            return PhoneString.ToString();
        }


        /// <summary>
        /// 设置用户注册验证码
        /// </summary>
        /// <param name="Phone">用户手机</param>
        /// <returns></returns>
        public void ClearUserVerificationCode(string Phone)
        {
            CacheHelper.Instance.RemoveCache("UserVerificationCode_UserPhone=" + Phone);
            CacheHelper.Instance.RemoveCache("UserResetVerificationCode_UserPhone=" + Phone);
        }


    }
}