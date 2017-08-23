using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class ClickScoreRequest : UserIDRequest
    {
        /// <summary>
        /// 筛选IDs
        /// </summary>
        [Required(ErrorMessage = "筛选ScoreIds不能为空", AllowEmptyStrings = false)]
        public string ScoreIds { get; set; }
    }
}