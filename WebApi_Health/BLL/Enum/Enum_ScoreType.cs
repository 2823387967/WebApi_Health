using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.BLL.Enum
{
    /// <summary>
    /// 分数类型
    /// </summary>
    public enum Enum_ScoreType
    {
        /// <summary>
        /// 运动积分
        /// </summary>
        Sport = 0,
        /// <summary>
        /// 睡眠积分
        /// </summary>
        Sleep = 1,
        /// <summary>
        /// 饮食积分
        /// </summary>
        Eat = 2
    }
    public static partial class GetString
    {
        /// <summary>
        /// 根据模型获取对应信息
        /// </summary>
        /// <param name="Enum_Model">当前枚举</param>
        /// <returns></returns>
        public static string Enum_GetString(this Enum_ScoreType Enum_Model)
        {
            string result = string.Empty;
            switch (Enum_Model)
            {
                case Enum_ScoreType.Sport:
                    result = "Sport";
                    break;
                case Enum_ScoreType.Sleep:
                    result = "Sleep";
                    break;
                case Enum_ScoreType.Eat:
                    result = "Eat";
                    break;
                default:
                    result = "";
                    break;
            }
            return result.ToLower();
        }
    }
}
