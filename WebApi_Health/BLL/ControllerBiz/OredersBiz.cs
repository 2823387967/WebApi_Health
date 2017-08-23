using Common;
using Common.Enum;
using Common.Extend;
using Common.Result;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi_Health.BLL.Cache;
using WebApi_Health.BLL.Function;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Response;

namespace WebApi_Health.BLL.ControllerBiz
{
    /// <summary>
    /// 订单算术逻辑层
    /// </summary>
    public class OrdersBiz : SingleTon<OrdersBiz>
    {
        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultJson<GetOrderListResponse> OrderList(GetInfoByUserIdPageRequest request)
        {
            ResultJson<GetOrderListResponse> result = new ResultJson<GetOrderListResponse>();
            var List_Order = CacheForModel_Order.Instance.GetOrdersByUserId(request.UserId);
            var List_Recipe = CacheForModelRecipe.Instance.GetRecipeList();
            var List_Restaurant = CacheForModelRestaurant.Instance.RestaurantList();
            var List_Recipe_Food = CacheForModelRecipe_Foods.Instance.GetRecipe_FoodsList();
            var List_Food_Type = CacheForModelFoodType.Instance.GetFoodTypeList();
            var List_Food = CacheForModelFood.Instance.GetFoodList();
            var List_Tag = CacheForModelTag.Instance.GetTagList();
            var List_Tag_Relation = CacheForModel_TagRelation.Instance.GetTagRelationList();
            if (List_Order.Count != 0)
            {
                foreach (var item in List_Order)
                {
                    var recipe = List_Recipe.Where(p => p.id.ToString() == item.RecipeId).FirstOrDefault();
                    if (recipe != null)
                    {
                        var restaurant = List_Restaurant.Where(p => p.id == recipe.restaurantId).FirstOrDefault();
                        if (restaurant != null)
                        {
                            GetOrderListResponse response = new GetOrderListResponse(item, recipe, restaurant, List_Food_Type, List_Recipe_Food, List_Tag, List_Tag_Relation, List_Food);
                            result.ListData.Add(response);
                        }
                    }
                }
                if (result.ListData.Count == 0)
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                }
                else
                {
                    result.HttpCode = 200;
                    result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                    result.ListData = Paging.Instance.PageData(result.ListData, request.PageNo);
                }
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultJsonModel<GetOrderInfoResponse> OrderInfo(GetOrderInfoRequest request)
        {
            ResultJsonModel<GetOrderInfoResponse> result = new ResultJsonModel<GetOrderInfoResponse>();
            Orders orders = CacheForModel_Order.Instance.GetOrdersByOrderId(request.OrderId);
            if (orders == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                return result;
            }
            var userModel = CacheForModelUser.Instance.GetUserInfo(orders.CustomerId);
            if (userModel == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
                return result;
            }
            var recipe = CacheForModelRecipe.Instance.GetRecipeListByRecipeId(orders.RecipeId.ParseInt().Value).FirstOrDefault();
            if (recipe == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                return result;
            }
            var restaurant = CacheForModelRestaurant.Instance.GetRestaurantById(recipe.restaurantId.Value).FirstOrDefault();
            if (restaurant == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                return result;
            }
            List<Food> list_food = CacheForModelFood.Instance.GetFoodList();
            List<FoodType> list_food_type = CacheForModelFoodType.Instance.GetFoodTypeList();
            List<Tag> list_tag = CacheForModelTag.Instance.GetTagList();
            List<Tag_Relation> list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationList();
            List<CustomerLike> list_customer_like = CacheForModelCustomerLike.Instance.GetCustomLike(orders.CustomerId);
            List<Recipe_foods> list_recipe_food = CacheForModelRecipe_Foods.Instance.GetRecipe_FoodsListByRecipeId(orders.RecipeId.ParseInt().Value);
            List<DataDictionary> list_data_dictionary = CacheForModelDataDcitionarys.Instance.GetDataDictionaryList("餐厅类型");
            GetOrderInfoResponse response = new GetOrderInfoResponse(orders, recipe, restaurant, list_food, list_food_type, list_tag, list_customer_like, userModel, list_tag_relation, list_recipe_food, list_data_dictionary, request.CoordX, request.CoordY);
            result.HttpCode = 200;
            result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            result.Model1 = response;
            return result;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultJson<GetOrderListResponse> DeleteOrder(GetInfoByOrderIdRequest request)
        {
            ResultJson<GetOrderListResponse> result = new ResultJson<GetOrderListResponse>();
            if (CacheForModel_Order.Instance.Delete_Order_ById(request.OrderId))
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
        /// 到店支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResultJson PayAtShopOrder(InsertPayAtShopRequest request)
        {
            ResultJson result = new ResultJson();
            Token token = new Token(request.Token);
            var UserModel = CacheForModelUser.Instance.GetUserInfo(token.Payload.UserID);
            if (UserModel.UserScore == null || UserModel.UserScore <= 20)
            {
                result.HttpCode = 300;
                result.Message = "用户积分未到达";
                return result;
            }
            Recipe recipe = CacheForModelRecipe.Instance.GetRecipeListByRecipeId(request.RecipeId).FirstOrDefault();
            if (recipe == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                if (CacheForModel_Order.Instance.Insert_Order_Pay_At_Shop(token.Payload.UserID, recipe, request.AtShopTime))
                {
                    CacheForModelScore.Instance.InsertEatScore(token.Payload.UserID, recipe.name);
                    result.HttpCode = 200;
                    result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
                }
            }

            return result;
        }

    }
}
