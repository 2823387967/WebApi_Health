using Common.Attribute.Constant;
using Common.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class AddScoreRequest : ScoreRequest
    {
        /// <summary>
        /// 用户睡眠时间
        /// </summary>
        [DateTimeValid(AllowEmpty = true)]
        public DateTime SleepTime { get; set; }
    }
}