using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.BLL.Enum
{
    /// <summary>
    /// 支付类型
    /// </summary>
    public enum Enum_PayType
    {
        /// <summary>
        /// 到店支付
        /// </summary>
        PayAtShop = 0,
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 1,
        /// <summary>
        /// 微信
        /// </summary>
        WeChat = 2
    }

    public static partial class GetString
    {
        /// <summary>
        /// 根据模型获取对应信息
        /// </summary>
        /// <param name="Enum_Model">当前枚举</param>
        /// <returns></returns>
        public static string Enum_GetString(this Enum_PayType Enum_Model)
        {
            string result = string.Empty;
            switch (Enum_Model)
            {
                case Enum_PayType.PayAtShop:
                    result = "到店支付";
                    break;
                case Enum_PayType.Alipay:
                    result = "支付宝";
                    break;
                case Enum_PayType.WeChat:
                    result = "微信";
                    break;
                default:
                    result = "";
                    break;
            }
            return result.ToLower();
        }
    }
}