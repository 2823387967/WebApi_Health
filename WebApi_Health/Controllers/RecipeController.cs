using Common.Helper;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Common.Extend;
using Common.Filter;
using Common.Result;
using WebApi_Health.Models.Response;
using WebApi_Health.BLL.Cache;
using DbOpertion.DBoperation;
using WebApi_Health.Models.Request;
using System.Linq;
using WebApi_Health.BLL.Enum;
using WebApi_Health.BLL.ControllerBiz;

namespace WebApi_Health.Controllers
{
    public class RecipeController : ApiController
    {
        /// <summary>
        /// 搜索范围
        /// </summary>
        private int SeachRange = int.Parse(ConfigurationManager.AppSettings["SeachRange"].ToString());
        /// <summary>
        /// 页面大小
        /// </summary>
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
        /// <summary>
        /// 根据餐厅Id获得菜谱列表
        /// </summary>
        [WebApiException]
        [HttpPost]
        [ValidateModel]
        public ResultJson<GetRecipeListByDRIdResponse> RecipeListInfoByDRId(GetInfoByOtherIdRequest request)
        {
            return RecipeBiz.Instance.RecipeListInfoByDRId(request);
        }

        /// <summary>
        /// 菜谱详情页面菜谱信息
        /// </summary>
        [WebApiException]
        [ValidateModel]
        [HttpPost]
        public ResultJson<GetRecipeItemResponse> RecipeItemInfo(GetRecipeItemRequest request)
        {
            return RecipeBiz.Instance.RecipeItemInfo(request);
        }

        /// <summary>
        /// 支付页面菜谱和商店信息
        /// </summary>
        [WebApiException]
        [ValidateModel]
        [HttpPost]
        public ResultJson<GetRecipePayItemResponse, GetRestaurantPayItemResponse> RecipeItemInfoForPay(GetRecipeItemRequest request)
        {
            return RecipeBiz.Instance.RecipeItemInfoForPay(request);
        }

        /// <summary>
        /// 根据GPS获取列表
        /// </summary>
        [WebApiException]
        [ValidateModel]
        [HttpPost]
        public ResultJson<GetRecipeListByGPSResponse> RecipeListByGPS(GetRecipeListByGPSRequest request)
        {
            return RecipeBiz.Instance.RecipeListByGPS(request);
        }

        /// <summary>
        /// 根据菜谱名称模糊查找菜谱信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetRecipeListByGPSResponse> SearchVagueRecipe(GetRestaurantListByNameRequest request)
        {
            return RecipeBiz.Instance.SearchVagueRecipe(request);
        }

        /// <summary>
        /// 菜谱列表
        /// </summary>
        /// <param name="list_recipe">菜谱列表</param>
        /// <param name="request">请求</param>
        /// <returns></returns>
        public ResultJson<GetRecipeListByGPSResponse> RecipeListForGPS(List<Recipe> list_recipe, GetRestaurantListByNameRequest request)
        {
            return RecipeBiz.Instance.RecipeListForGPS(list_recipe, request);
        }
    }
}