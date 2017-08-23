using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class GetInfoByIdRequest : TokenRequest
    {
        /// <summary>
        /// 各种类的ID
        /// </summary>
        [Required(ErrorMessage = "请填写Id", AllowEmptyStrings = false)]
        public int id { get; set; }
    }
}