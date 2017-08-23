using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Response
{
    public class GetSportListResponse
    {
        public GetSportListResponse()
        {

        }

        /// <summary>
        /// 时间
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public int steps { get; set; }
    }
}