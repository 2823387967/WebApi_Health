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
using WebApi_Health.BLL.Enum;
using Common.Extend;

namespace WebApi_Health.Controllers
{
    public class SportController : ApiController
    {
        /// <summary>
        /// 运动数据更新
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson UpLoadSportInfo(UpdateSportItemRequest request)
        {
            ResultJson result = new ResultJson();
            var model = request.ToModel();
            if (CacheForModelSport.Instance.InsertUserSport(model))
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 获取运动信息列表
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetSportListResponse> GetSportList(GetSportListRequest request)
        {
            ResultJson<GetSportListResponse> result = new ResultJson<GetSportListResponse>();
            List<Sport> List_Sport = new List<Sport>();
            List_Sport = CacheForModelSport.Instance.GetUserSportList(request.UserId.ToString());
            List<GetSportListResponse> List_Response = new List<GetSportListResponse>();
            if (request.DateType == Enum_SearchType.Year.Enum_GetString())
            {
                #region 年份列表
                for (int i = 6; i >= 0; i--)
                {
                    var steps = List_Sport.Where(p => p.sDate.Year == DateTime.Now.Year - i).Sum(p => p.steps);
                    GetSportListResponse response = new GetSportListResponse();
                    response.date = DateTime.Now.Year - i + "年";
                    response.steps = steps.GetValueOrDefault();
                    List_Response.Add(response);
                }
                #endregion
            }
            else if (request.DateType == Enum_SearchType.Month.Enum_GetString())
            {
                #region 月份列表
                for (int i = 1; i <= 12; i++)
                {
                    var steps = List_Sport.Where(p => p.sDate.Month == i && p.sDate.Year == DateTime.Now.Year).Sum(p => p.steps);
                    GetSportListResponse response = new GetSportListResponse();
                    response.date = i + "月";
                    response.steps = steps.GetValueOrDefault();
                    List_Response.Add(response);
                }
                #endregion
            }
            else if (request.DateType == Enum_SearchType.Day.Enum_GetString())
            {
                #region 日期列表
                ////之前7天
                //if (DateTime.Now.Day >= 7)
                //{
                //    for (int i = DateTime.Now.Day - 6; i <= DateTime.Now.Day; i++)
                //    {
                //        var steps = List_Sport.Where(p => p.sDate.Day == i && p.sDate.Year == DateTime.Now.Year && p.sDate.Month == DateTime.Now.Month).Sum(p => p.steps);
                //        GetSportListResponse response = new GetSportListResponse();
                //        response.date = DateTime.Now.Month + "." + i;
                //        response.steps = steps.GetValueOrDefault();
                //        List_Response.Add(response);
                //    }
                //}
                //else
                //{
                //    for(int i = 0; i < 7; i++)
                //    {
                //        var day = DateTime.Now.AddDays(-i).ToDate();
                //        var steps = List_Sport.Where(p => p.sDate == day).Sum(p => p.steps);
                //        GetSportListResponse response = new GetSportListResponse();
                //        response.date = day.Month + "." + day.Day;
                //        response.steps = steps.GetValueOrDefault();
                //        List_Response.Add(response);
                //    }
                //}
                for (int i = 1; i <= 7; i++)
                {
                    var Week = DateTime.Now.AddDays(i - (int)DateTime.Now.DayOfWeek).ToDate();
                    var steps = List_Sport.Where(p => p.sDate == Week).Sum(p => p.steps);
                    GetSportListResponse response = new GetSportListResponse();
                    response.date = Week.Month + "." + Week.Day;
                    response.steps = steps.GetValueOrDefault();
                    List_Response.Add(response);
                }
                #endregion
            }
            if (List_Response.Count == 0)
            {
                result.ListData = List_Response;
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.ListData = List_Response;
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            return result;
        }
    }
}