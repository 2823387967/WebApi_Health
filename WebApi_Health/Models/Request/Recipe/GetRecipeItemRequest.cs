using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    /// <summary>
    /// 获取菜谱Request
    /// </summary>
    public class GetRecipeItemRequest : UserIDRequest
    {
        /// <summary>
        /// 菜谱Id
        /// </summary>
        [Required(ErrorMessage = "菜谱Id请填写", AllowEmptyStrings = false)]
        public int RecipeId { get; set; }
    }
}