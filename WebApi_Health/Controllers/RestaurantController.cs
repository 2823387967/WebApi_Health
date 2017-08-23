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
using System;
using System.Linq;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Variable;
using WebApi_Health.BLL.Function;
using Common.Enum;
using WebApi_Health.BLL.Enum;
using WebApi_Health.BLL.ControllerBiz;
using System.Text.RegularExpressions;

namespace WebApi_Health.Controllers
{
    public class RestaurantController : ApiController
    {
        /// <summary>
        /// 百度地图AK
        /// </summary>
        private string BaiDuAK = ConfigurationManager.AppSettings["BaiDuAK"].ToString();
        /// <summary>
        /// 搜索范围
        /// </summary>
        private int SeachRange = int.Parse(ConfigurationManager.AppSettings["SeachRange"].ToString());
        /// <summary>
        /// 页面大小
        /// </summary>
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
        /// <summary>
        /// 根据坐标获取餐厅信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetRestaurantListResponse> GetRestaurantListInfo(GetRestaurantListRequest request)
        {
            ResultJson<GetRestaurantListResponse> result = new ResultJson<GetRestaurantListResponse>();
            if (!request.GroupBy.IsNullOrEmpty() && request.GroupBy.Trim() == "Type" && (request.TypeValue.IsNullOrEmpty() || request.TypeValue.ParseInt() == null))
            {
                result.HttpCode = 400;
                result.Message = "由于排序类型为Type,请上传TypeValue";
                return result;
            }

            var List_Restaurant = CacheForModelRestaurant.Instance.RestaurantList();
            List<GetRestaurantListResponse> List_Response_Option = new List<GetRestaurantListResponse>();
            List<GetRestaurantListResponse> List_Response_Result = new List<GetRestaurantListResponse>();

            //计算当前位置与目的地的距离
            foreach (Restaurant item in List_Restaurant)
            {
                GetRestaurantListResponse response = new GetRestaurantListResponse(item, request.CoordX, request.CoordY, SeachRange);
                if (response.distance != null)
                {
                    List_Response_Option.Add(response);
                }
            }
            if (request.GroupBy.IsNullOrEmpty() || request.GroupBy.Trim().ToLower() == Enum_SearchType.Distance.Enum_GetString())
            {
                //根据距离排序
                List_Response_Option = List_Response_Option.OrderBy(p => p.distance).ToList();
            }
            else if (request.GroupBy.Trim().ToLower() == Enum_SearchType.SalesVolume.Enum_GetString())
            {
                //根据销售量排序
                List_Response_Option = List_Response_Option.OrderByDescending(p => p.sales).ToList();
            }
            else if (request.GroupBy.Trim().ToLower() == Enum_SearchType.SuitMe.Enum_GetString())
            {

                //ListRestaurantResponse.Sort((q, p) => q.sales.Value.CompareTo(p.sales));
            }
            else if (request.GroupBy.Trim().ToLower() == Enum_SearchType.Type.Enum_GetString())
            {
                //根据类型进行排序
                List_Response_Option = List_Response_Option.Where(p => p.category == request.TypeValue).ToList();
            }
            List_Response_Result = BLL.Function.Paging.Instance.PageData<GetRestaurantListResponse>(List_Response_Option, PageSize, request.PageNo);
            if (List_Response_Result.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.ListData = List_Response_Result;
            }
            return result;
        }

        /// <summary>
        /// 根据餐厅Id获取餐厅信息
        /// </summary>
        /// <param name="id">餐厅Id</param>
        /// <returns></returns>
        [HttpPost]
        [WebApiException]
        [ValidateModel]
        public ResultJson<GetRestaurantItemtResponse> GetRestaurantInfoById(GetRestaurantInfoByIdRequest request)
        {
            ResultJson<GetRestaurantItemtResponse> result = new ResultJson<GetRestaurantItemtResponse>();
            var list_Restaurant = CacheForModelRestaurant.Instance.GetRestaurantById(request.id);
            var custom_Like = CacheForModelCustomerLike.Instance.GetCustomLikeRest(request.UserId, request.id);
            result.ListData = new List<GetRestaurantItemtResponse>();
            var ListDic = CacheForModelDataDcitionarys.Instance.GetDataDictionaryList("餐厅类型");
            foreach (var item in list_Restaurant)
            {
                GetRestaurantItemtResponse response = new GetRestaurantItemtResponse(item, request.CoordX, request.CoordY, SeachRange, ListDic);
                result.ListData.Add(response);
                if (custom_Like != null)
                {
                    response.cusLikeOrNot = true;
                }
                else
                {
                    response.cusLikeOrNot = false;
                }
            }
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

        /// <summary>
        /// 根据餐厅名称获取餐厅信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetRestaurantListResponse, GetArticleListResponse, GetRecipeListByGPSResponse> SearchVagueRestaurant(GetRestaurantListByNameRequest request)
        {
            ResultJson<GetRestaurantListResponse, GetArticleListResponse, GetRecipeListByGPSResponse> result = new ResultJson<GetRestaurantListResponse, GetArticleListResponse, GetRecipeListByGPSResponse>();
            ResultJson<GetRestaurantListResponse, GetArticleListResponse, GetRecipeListByGPSResponse> result1 = new ResultJson<GetRestaurantListResponse, GetArticleListResponse, GetRecipeListByGPSResponse>();
            ResultJson<GetRestaurantListResponse, GetArticleListResponse, GetRecipeListByGPSResponse> result2 = new ResultJson<GetRestaurantListResponse, GetArticleListResponse, GetRecipeListByGPSResponse>();
            request.Name = Regex.Replace(request.Name.ToString(), @"\p{Cs}", "");
            if (request.SearchType.ToLower() == Enum_SearchType.Restaurant.Enum_GetString() || request.SearchType.ToLower() == Enum_SearchType.All.Enum_GetString())
            {
                List<GetRestaurantListResponse> List_Response_Option = new List<GetRestaurantListResponse>();
                List<GetRestaurantListResponse> List_Response_Result = new List<GetRestaurantListResponse>();
                var list = CacheForModelRestaurant.Instance.GetRestaurantByName(request.Name);
                foreach (var item in list)
                {
                    GetRestaurantListResponse response = new GetRestaurantListResponse(item, request.CoordX, request.CoordY, SeachRange);
                    if (response.distance != null)
                    {
                        List_Response_Option.Add(response);
                    }
                }

                List_Response_Result = Paging.Instance.PageData<GetRestaurantListResponse>(List_Response_Option, PageSize, request.PageNo);

                if (List_Response_Option.Count == 0)
                {
                    result1.HttpCode = 300;
                    result1.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                }

                else if (List_Response_Result.Count == 0)
                {
                    result1.HttpCode = 300;
                    result1.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                }
                else
                {
                    result1.HttpCode = 200;
                    result1.Message = Enum_Message.SuccessMessage.Enum_GetString();
                    result1.ListData = List_Response_Result;
                }
            }
            if (request.SearchType.ToLower() == Enum_SearchType.Recipe.Enum_GetString() || request.SearchType.ToLower() == Enum_SearchType.All.Enum_GetString())
            {
                RecipeController recipeContr = new RecipeController();
                GetRestaurantListByNameRequest Recipe_List_Request = new GetRestaurantListByNameRequest
                {
                    CoordX = request.CoordX,
                    CoordY = request.CoordY,
                    PageNo = request.PageNo,
                    Token = request.Token,
                    UserId = request.UserId,
                    Name = request.Name,
                };
                var recipe_Result = recipeContr.SearchVagueRecipe(Recipe_List_Request);
                if (recipe_Result.HttpCode != 200)
                {
                    result2.HttpCode = recipe_Result.HttpCode;
                    result2.Message = recipe_Result.Message;
                }
                else
                {
                    result2.HttpCode = 200;
                    result2.Message = Enum_Message.SuccessMessage.Enum_GetString();
                    result2.ListData3 = recipe_Result.ListData;
                }
                CacheForModelSearchRecord.Instance.InsertSearchRecord(request.Name);
            }
            var Searchresult = ArticleBiz.Instance.SearchArticleList(request.UserId, request.PageNo, request.Name);
            if (Searchresult.HttpCode != 200 & result1.HttpCode != 200 & result2.HttpCode != 200)
            {
                result.HttpCode = Searchresult.HttpCode;
                result.Message = Searchresult.Message;
            }
            else
            {
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.HttpCode = 200;
                result.ListData = result1.ListData;
                result.ListData2 = Searchresult.ListData;
                result.ListData3 = result2.ListData3;
            }
            return result;
        }

        /// <summary>
        /// 用户喜欢与否
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson CustomerLikeOrNot(CustomLikeRequest request)
        {
            ResultJson result = new ResultJson();
            CustomerLike customerLike = new CustomerLike();
            customerLike.cid = request.UserId;
            customerLike.lid = request.OtherId;
            customerLike.type = request.Type_Like.ToLower();
            bool result1 = false, result2 = false;
            if (request.Opertion.ToLower() == Enum_Opertion.Delete.Enum_GetString())
            {
                result1 = CustomerLikeOper.Instance.DeleteByModel(customerLike);
                if (result1)
                {
                    result.HttpCode = 200;
                    result.Message = Enum_Message.DataDeleteSuccessMessage.Enum_GetString();
                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                }
            }
            else if (request.Opertion.ToLower() == Enum_Opertion.Insert.Enum_GetString())
            {
                if (request.Type_Like.ToLower() == UserLikeTypeVariable.FoodLike || request.Type_Like.ToLower() == UserLikeTypeVariable.FoodUnLike)
                {
                    customerLike.type = null;
                    var customer_like = CustomerLikeOper.Instance.Select(customerLike).Where(p => p.type.ToLower() == UserLikeTypeVariable.FoodLike || p.type.ToLower() == UserLikeTypeVariable.FoodUnLike).FirstOrDefault();
                    if (customer_like != null)
                    {
                        customer_like.type = request.Type_Like.ToLower();
                        result2 = CustomerLikeOper.Instance.Update(customer_like);
                    }
                    else
                    {
                        customerLike.type = request.Type_Like.ToLower();
                        result2 = CustomerLikeOper.Instance.Insert(customerLike);
                    }
                }
                else if (request.Type_Like.ToLower() == UserLikeTypeVariable.RestLike || request.Type_Like.ToLower() == UserLikeTypeVariable.ArticleLike)
                {
                    var customer_like_list = CustomerLikeOper.Instance.Select(customerLike);
                    if (customer_like_list.Count != 0)
                    {
                        result.HttpCode = 300;
                        result.Message = Enum_Message.DataExitMessage.Enum_GetString();
                        return result;
                    }
                    else
                    {
                        result2 = CustomerLikeOper.Instance.Insert(customerLike);
                    }
                }

                if (result2)
                {
                    result.HttpCode = 200;
                    result.Message = Enum_Message.DataInsertSuccessMessage.Enum_GetString();
                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                }
            }
            return result;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetRestaurantListResponse, GetRecipeListByGPSResponse, GetArticleListResponse, GetOrderListResponse> TitlePage(GetTitlePageRequest request)
        {
            ResultJson<GetRestaurantListResponse, GetRecipeListByGPSResponse, GetArticleListResponse, GetOrderListResponse> result = new ResultJson<GetRestaurantListResponse, GetRecipeListByGPSResponse, GetArticleListResponse, GetOrderListResponse>();
            var user = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            GetRestaurantListRequest Restaurant_List_Request = new GetRestaurantListRequest
            {
                CoordX = request.CoordX,
                CoordY = request.CoordY,
                PageNo = 1,
            };
            var result_Restaurant = GetRestaurantListInfo(Restaurant_List_Request);
            GetRecipeListByGPSRequest Recipe_List_Request = new GetRecipeListByGPSRequest
            {
                CoordX = request.CoordX,
                CoordY = request.CoordY,
                PageNo = 1,
                UserId = request.UserId
            };
            RecipeController recipeContro = new RecipeController();
            var result_Recipe = recipeContro.RecipeListByGPS(Recipe_List_Request);
            GetInfoByIdPaggingRequest Article_List_Request = new GetInfoByIdPaggingRequest
            {
                PageNo = 1,
                Id = request.UserId,
                Token = request.Token
            };
            ArticleController ArticleContro = new ArticleController();
            var result_Article = ArticleContro.GetArticleListInfo(Article_List_Request);
            GetInfoByUserIdPageRequest Order_List_Request = new GetInfoByUserIdPageRequest
            {
                PageNo = 1,
                UserId = request.UserId,
                Token = request.Token
            };
            var result_Order = OrdersBiz.Instance.OrderList(Order_List_Request);
            if (result_Recipe.HttpCode == 200 && result_Restaurant.HttpCode == 200 && result_Article.HttpCode == 200)
            {
                result.HttpCode = 200;
                result.ListData = Paging.Instance.PageData(result_Restaurant.ListData, 9, 1);
                result.ListData2 = Paging.Instance.PageData(result_Recipe.ListData, 5, 1);
                result.ListData3 = Paging.Instance.PageData(result_Article.ListData, 4, 1);
                result.ListData4 = Paging.Instance.PageData(result_Order.ListData, 3, 1);
            }
            else
            {
                result.HttpCode = 500;
                result.Message = "数据筛选失败";
            }
            return result;
        }

        /// <summary>
        /// 热门搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<string> HotSearch()
        {
            ResultJson<string> result = new ResultJson<string>();
            var list_Item = CacheForModelSearchRecord.Instance.SearchRecordList();
            result.ListData = list_Item.Select(p => p.SearchKey).ToList();
            if (result.ListData.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }

            return result;
        }

        /// <summary>
        /// 用户喜欢餐厅
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetRestaurantListResponse> UserPreferenceRest(GetUserPreferenceRestRequest request)
        {
            return RestaurantBiz.Instance.UserPreferenceRest(request);
        }
    }
}