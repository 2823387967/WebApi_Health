using Common.Result;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Health.BLL.Cache;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Response;
using WebApi_Health.Models.Variable;
using Common.Extend;
using Common.Filter;
using WebApi_Health.BLL.ControllerBiz;

namespace WebApi_Health.Controllers
{
    public class QuestionController : ApiController
    {
        /// <summary>
        /// 简易版问题列表
        /// </summary>
        [HttpGet]
        public ResultJson<GetQuestionListResponse> GetQuestionExpressList()
        {
            return QuestionBiz.Instance.GetQuestionExpressList();
        }
        /// <summary>
        /// 专业版问题列表
        /// </summary>
        [HttpPost]
        public ResultJson<GetQuestionListResponse> GetQuestionProfessionList(GetInfoByOtherIdRequest request)
        {
            return QuestionBiz.Instance.GetQuestionProfessionList(request);
        }
        /// <summary>
        /// 专业版问题提交结果
        /// </summary>
        [HttpPost]
        public ResultJson GetSubmitQuestion(SubmitQusttionRequest request)
        {
            return QuestionBiz.Instance.GetSubmitQuestion(request);
        }
        /// <summary>
        /// 简易版问题提交结果
        /// </summary>
        [HttpPost]
        [ValidateModel]
        public ResultJsonModel<string> GetSubmitExpressQuestion(SubmitQusttionRequest request)
        {
            return QuestionBiz.Instance.GetSubmitExpressQuestion(request);
        }
    }
}