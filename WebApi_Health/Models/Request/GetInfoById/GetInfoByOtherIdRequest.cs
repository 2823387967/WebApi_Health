using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi_Health.BLL.Attribute;

namespace WebApi_Health.Models.Request
{
    public class GetInfoByOtherIdRequest : UserIDRequest
    {
        /// <summary>
        /// 各种类的ID
        /// </summary>
        public int id { get; set; }
    }
}