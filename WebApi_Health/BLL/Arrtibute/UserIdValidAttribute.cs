using Common.Extend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_Health.BLL.Cache;

namespace WebApi_Health.BLL.Attribute
{
    public class UserIdValidAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowEmpty { get; set; }
        public override bool IsValid(object value)
        {
            this.ErrorMessage = "该用户Id为空或用户不存在";
            if (value != null)
            {
                var id = value.ToString().ParseInt();
                if (id != null && id != 0)
                {
                    if (CacheForModelUser.Instance.GetUserInfo(id.Value) != null)
                    {
                        return true;
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
            else
            {
                return false;
            }
        }
    }
}
