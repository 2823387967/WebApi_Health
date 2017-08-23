using Common.Extend;
using Common.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_Health.BLL.Cache;
using WebApi_Health.BLL.Enum;
using Common.Enum;

namespace Common.Attribute
{
    public class TokenValidAttribute : ValidationAttribute
    {

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowEmpty { get; set; }
        public override bool IsValid(object value)
        {
            this.ErrorMessage = Enum_Message.TokenInvalidMessage.Enum_GetString();
            if (AllowEmpty == true)
            {
                return true;
            }
            if (value != null)
            {
                Token token = CacheForModelUser.Instance.GetUserToken(value.ToString());
                if (token != null)
                {
                    var usermodel = CacheForModelUser.Instance.GetUserInfo(token.Payload.UserID);
                    if (usermodel == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
