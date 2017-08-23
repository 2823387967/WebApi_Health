using Common;
using Common.Enum;
using Common.Result;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApi_Health.BLL.Cache;
using WebApi_Health.BLL.Function;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Response;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.BLL.ControllerBiz
{
    public class RestaurantBiz : SingleTon<RestaurantBiz>
    {
        /// <summary>
        /// 搜索范围
        /// </summary>
        private int SeachRange2 = int.Parse(ConfigurationManager.AppSettings["SeachRange2"].ToString());
        /// <summary>
        /// 搜索范围
        /// </summary>
        private int SeachRange = int.Parse(ConfigurationManager.AppSettings["SeachRange"].ToString());

        public ResultJson<GetRestaurantListResponse> UserPreferenceRest(GetUserPreferenceRestRequest request)
        {
            ResultJson<GetRestaurantListResponse> result = new ResultJson<GetRestaurantListResponse>();
            var List_Customer = CacheForModelCustomerLike.Instance.GetCustomLike(request.UserId);
            List_Customer = List_Customer.Where(p => p.type == UserLikeTypeVariable.RestLike).ToList();
            var List_Rest = CacheForModelRestaurant.Instance.RestaurantList();
            foreach (var item in List_Customer)
            {
                var rest = List_Rest.Where(p => p.id == item.lid).FirstOrDefault();
                if (rest != null)
                {
                    GetRestaurantListResponse response = new GetRestaurantListResponse(rest, request.CoordX, request.CoordY, SeachRange2);
                    result.ListData.Add(response);
                }
            }
            result.ListData = Paging.Instance.PageData(result.ListData, request.PageNo);
            if (result.ListData.Count != 0)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return result;
        }
    }
}