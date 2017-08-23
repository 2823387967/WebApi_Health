using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common.Extend;
using Common.Attribute;
using Common.Filter;
using WebApi_Health.BLL.Attribute;
using Common.Attribute.Constant;
using DbOpertion.Models;

namespace WebApi_Health.Models.Request
{
    public class GetSportListRequest : UserIDRequest
    {
        public GetSportListRequest()
        {

        }
        /// <summary>
        /// 日期类型
        /// </summary>
        [limitString(AllowEmpty = false, Limit = "year|month|day",ErrorMessage ="日期类型")]
        public string DateType { get; set; }
    }
}