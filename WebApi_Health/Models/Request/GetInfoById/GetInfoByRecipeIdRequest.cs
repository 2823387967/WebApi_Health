using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 根据菜谱Id获得信息
    /// </summary>
    public class GetInfoByRecipeIdRequest : TokenRequest
    {
        /// <summary>
        /// 菜谱Id
        /// </summary>
        [IntValid(ErrorMessage = "请填写菜谱Id")]
        public int RecipeId { get; set; }
    }
}