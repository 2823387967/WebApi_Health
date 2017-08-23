using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Variable
{
    public class UserLikeTypeVariable
    {
        /// <summary>
        /// 用户喜欢的餐厅
        /// </summary>
        public static readonly string RestLike = "restlike";
        /// <summary>
        /// 用户喜欢的食物
        /// </summary>
        public static readonly string FoodLike = "foodlike";
        /// <summary>
        /// 用户不喜欢的食物
        /// </summary>
        public static readonly string FoodUnLike = "foodunlike";
        /// <summary>
        /// 用户喜欢的食物
        /// </summary>
        public static readonly string ArticleLike = "articlelike";
    }
}