using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Health.BLL.ControllerBiz;
using WebApi_Health.Models.Models;
using WebApi_Health.Models.Request;
using Common.Extend;
using Common.Filter;
using WebApi_Health.Models.Response;

namespace WebApi_Health.Controllers
{
    /// <summary>
    /// Score控制器
    /// </summary>
    public class ScoreController : ApiController
    {
        /// <summary>
        /// 增加积分记录
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson AddScoreRecord(AddScoreRequest request)
        {
            return ScoreBiz.Instance.AddScoreRecord(request);
        }

        /// <summary>
        /// 积分点击
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson ClickScore(ClickScoreRequest request)
        {
            return ScoreBiz.Instance.ClickScore(request);
        }

        /// <summary>
        /// 获得未获取分数
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<GetScoreResponse, GetScoreResponse, GetScoreResponse> GetClickScore(UserIDRequest request)
        {
            return ScoreBiz.Instance.GetClickScore(request);
        }

        /// <summary>
        /// 获得分数列表
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetScoreListResponse> GetScoreList(GetInfoByUserIdPageRequest request)
        {
            return ScoreBiz.Instance.GetScoreList(request);
        }

    }
}