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
    public class UpdateSportItemRequest:UserIDRequest
    {
        /// <summary>
        /// 步数
        /// </summary>
        [IntValid(ErrorMessage = "请输入步数")]
        public int steps { get; set; }

    }
    public static class ExtendToModel
    {
        /// <summary>
        /// 转换成模型
        /// </summary>
        public static Sport ToModel(this UpdateSportItemRequest request)
        {
            Sport sport = new Sport();
            sport.cid = request.UserId;
            sport.steps = request.steps;
            return sport;
        }
    }
}