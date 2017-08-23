using Common;
using Common.Enum;
using Common.Extend;
using Common.Result;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebApi_Health.BLL.Cache;
using WebApi_Health.BLL.Enum;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Response;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.BLL.ControllerBiz
{
    public class RecipeBiz : SingleTon<RecipeBiz>
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
        public ResultJson<GetRecipeListByDRIdResponse> RecipeListInfoByDRId(GetInfoByOtherIdRequest request)
        {
            ResultJson<GetRecipeListByDRIdResponse> result = new ResultJson<GetRecipeListByDRIdResponse>();
            result.ListData = new List<GetRecipeListByDRIdResponse>();
            var list_recipe = CacheForModelRecipe.Instance.GetRecipeListByDRId(request.id);
            var list_tag = CacheForModelTag.Instance.GetTagList();
            var list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationList();
            list_tag_relation = list_tag_relation.Where(p => Enum_SearchType.Recipe.Enum_GetString().EqualString(p.typename)).ToList();
            var list_recipe_food = CacheForModelRecipe_Foods.Instance.GetRecipe_FoodsList();
            var list_food = CacheForModelFood.Instance.GetFoodList();
            var list_food_type = CacheForModelFoodType.Instance.GetFoodTypeList();
            foreach (var item in list_recipe)
            {
                GetRecipeListByDRIdResponse response = new GetRecipeListByDRIdResponse(item, list_food, list_food_type, list_tag, list_tag_relation, list_recipe_food);
                result.ListData.Add(response);
            }
            if (false)
            {
                result.ListData = BLL.Function.Paging.Instance.PageData<GetRecipeListByDRIdResponse>(result.ListData, PageSize, 1);
            }
            if (list_recipe.Count != 0)
            {
                result.HttpCode = 200;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }

            return result;
        }

        /// <summary>
        /// 菜谱详情页面菜谱信息
        /// </summary>
        public ResultJson<GetRecipeItemResponse> RecipeItemInfo(GetRecipeItemRequest request)
        {
            ResultJson<GetRecipeItemResponse> result = new ResultJson<GetRecipeItemResponse>();
            result.ListData = new List<GetRecipeItemResponse>();
            var list_recipe = CacheForModelRecipe.Instance.GetRecipeListByRecipeId(request.RecipeId);
            var list_tag = CacheForModelTag.Instance.GetTagList();
            var list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationListByRelationId(request.RecipeId);
            list_tag_relation = list_tag_relation.Where(p => Enum_SearchType.Recipe.Enum_GetString().EqualString(p.typename)).ToList();
            var list_recipe_food = CacheForModelRecipe_Foods.Instance.GetRecipe_FoodsList();
            var list_food = CacheForModelFood.Instance.GetFoodList();
            var list_food_type = CacheForModelFoodType.Instance.GetFoodTypeList();
            var User_Model = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (User_Model == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
                return result;
            }
            var List_CustomerLike = CacheForModelCustomerLike.Instance.GetCustomLike(User_Model.id);
            foreach (var item in list_recipe)
            {
                GetRecipeItemResponse response = new GetRecipeItemResponse(item, list_food, list_food_type, list_tag, List_CustomerLike, User_Model, list_tag_relation, list_recipe_food);
                result.ListData.Add(response);

            }
            if (list_recipe.Count != 0)
            {
                result.HttpCode = 200;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 支付页面菜谱和商店信息
        /// </summary>
        public ResultJson<GetRecipePayItemResponse, GetRestaurantPayItemResponse> RecipeItemInfoForPay(GetRecipeItemRequest request)
        {
            ResultJson<GetRecipePayItemResponse, GetRestaurantPayItemResponse> result = new ResultJson<GetRecipePayItemResponse, GetRestaurantPayItemResponse>();
            result.ListData = new List<GetRecipePayItemResponse>();
            result.ListData2 = new List<GetRestaurantPayItemResponse>();
            var list_recipe = CacheForModelRecipe.Instance.GetRecipeListByRecipeId(request.RecipeId);
            var list_tag = CacheForModelTag.Instance.GetTagList();
            var list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationListByRelationId(request.RecipeId);
            list_tag_relation = list_tag_relation.Where(p => Enum_SearchType.Recipe.Enum_GetString().EqualString(p.typename)).ToList();
            var User_Model = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            var Dic = CacheForModelDataDcitionarys.Instance.GetDataDictionaryList("餐厅类型");
            if (User_Model == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
                return result;
            }
            foreach (var item in list_recipe)
            {
                GetRecipePayItemResponse response = new GetRecipePayItemResponse(item, list_tag, User_Model, list_tag_relation);
                var list_restaurant = CacheForModelRestaurant.Instance.GetRestaurantById(item.restaurantId.Value);
                if (list_restaurant != null)
                {
                    GetRestaurantPayItemResponse restaurantPayRespnose = new GetRestaurantPayItemResponse(list_restaurant[0], Dic);
                    result.ListData2.Add(restaurantPayRespnose);
                }
                result.ListData.Add(response);

            }
            if (result.ListData2.Count != 0 && result.ListData.Count != 0)
            {
                result.HttpCode = 200;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString(); ;
            }
            return result;
        }

        /// <summary>
        /// 根据GPS获取列表
        /// </summary>
        public ResultJson<GetRecipeListByGPSResponse> RecipeListByGPS(GetRecipeListByGPSRequest request)
        {
            var list_recipe = CacheForModelRecipe.Instance.GetRecipeList();
            GetRestaurantListByNameRequest Recipe_Request = new GetRestaurantListByNameRequest
            {
                CoordX = request.CoordX,
                CoordY = request.CoordY,
                PageNo = request.PageNo,
                Token = request.Token,
                UserId = request.UserId,
            };
            var result = RecipeListForGPS(list_recipe, Recipe_Request);
            if (request.GroupBy.IsNullOrEmpty() || request.GroupBy.Trim().ToLower() == Enum_SearchType.Distance.Enum_GetString())
            {
                //根据距离排序
                result.ListData = result.ListData.OrderBy(p => p.distance).ToList();
            }
            else if (request.GroupBy.Trim().ToLower() == Enum_SearchType.SalesVolume.Enum_GetString())
            {
                //根据销售量排序
                result.ListData = result.ListData.OrderByDescending(p => p.sales).ToList();
            }
            else if (request.GroupBy.Trim().ToLower() == Enum_SearchType.SuitMe.Enum_GetString())
            {
                result.ListData = result.ListData.OrderByDescending(p => p.ConstitutionPercentage).ToList();
            }
            else if (request.GroupBy.Trim().ToLower() == Enum_SearchType.Type.Enum_GetString())
            {
                //根据类型进行排序
                result.ListData = result.ListData.Where(p => p.category == request.TypeValue).ToList();
            }
            result.ListData = Function.Paging.Instance.PageData(result.ListData, PageSize, request.PageNo);
            if (result.ListData.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
            }
            return result;
        }

        /// <summary>
        /// 根据菜谱名称模糊查找菜谱信息
        /// </summary>
        /// <returns></returns>
        public ResultJson<GetRecipeListByGPSResponse> SearchVagueRecipe(GetRestaurantListByNameRequest request)
        {
            var list_recipe = CacheForModelRecipe.Instance.GetRecipeListByRecipeName(request.Name);
            var result = RecipeListForGPS(list_recipe, request);
            result.ListData = Function.Paging.Instance.PageData(result.ListData, PageSize, request.PageNo);
            if (result.ListData.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
            }
            return result;
        }

        /// <summary>
        /// 菜谱列表
        /// </summary>
        /// <param name="list_recipe">菜谱列表</param>
        /// <param name="request">请求</param>
        /// <returns></returns>
        public ResultJson<GetRecipeListByGPSResponse> RecipeListForGPS(List<Recipe> list_recipe, GetRestaurantListByNameRequest request)
        {
            ResultJson<GetRecipeListByGPSResponse> result = new ResultJson<GetRecipeListByGPSResponse>();
            var list_restaurant = CacheForModelRestaurant.Instance.RestaurantList();
            List<GetRestaurantListResponse> list_restaurant_response = new List<GetRestaurantListResponse>();
            var list_tag = CacheForModelTag.Instance.GetTagList();
            var list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationList();
            list_tag_relation = list_tag_relation.Where(p => Enum_SearchType.Recipe.Enum_GetString().EqualString(p.typename)).ToList();
            var User_Model = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (User_Model == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
                return result;
            }
            List<GetRecipeListByGPSResponse> List_Response_Option = new List<GetRecipeListByGPSResponse>();
            foreach (var item in list_restaurant)
            {
                var restaurant = new GetRestaurantListResponse(item, request.CoordX, request.CoordY, SeachRange);
                list_restaurant_response.Add(restaurant);
            }
            foreach (var item in list_recipe)
            {
                var recipe = new GetRecipeListByGPSResponse(item, list_restaurant_response, list_tag, User_Model, list_tag_relation);
                List_Response_Option.Add(recipe);
            }

            if (List_Response_Option.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.ListData = List_Response_Option;
            }

            return result;
        }
    }
}