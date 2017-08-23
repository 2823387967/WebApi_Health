using Common.Result;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Health.BLL.Cache;
using WebApi_Health.Models.Response;
using Common.Filter;
using WebApi_Health.BLL.Function;
using WebApi_Health.Models.Request;
using Common.Enum;
using WebApi_Health.Models.Variable;
using WebApi_Health.BLL.ControllerBiz;
using WebApi_Health.Models;

namespace WebApi_Health.Controllers
{
    /// <summary>
    /// 文章控制器
    /// </summary>
    public class ArticleController : ApiController
    {
        /// <summary>
        /// 饮食文章列表
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetArticleListResponse, GetUserSuitResponse> GetArticleListInfo(GetInfoByIdPaggingRequest request)
        {
            return ArticleBiz.Instance.SearchArticleList(request.Id, request.PageNo);
        }

        /// <summary>
        /// 根据文章Id获取文章
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetArticleItemResponse> GetArticleItemInfo(GetInfoByOtherIdRequest request)
        {
            return ArticleBiz.Instance.GetArticleItemInfo(request);
        }

        /// <summary>
        /// 文章查看
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson ArticleClick(GetInfoByOtherIdRequest request)
        {
            return ArticleBiz.Instance.ArticleClick(request);
        }

        /// <summary>
        /// 文章点赞
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson ArticlePointPraise(GetArticlePointPraiseRequest request)
        {
            return ArticleBiz.Instance.ArticlePointPraise(request);
        }
    }
}