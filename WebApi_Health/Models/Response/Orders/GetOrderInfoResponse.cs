using Common.Extend;
using Common.Helper;
using DbOpertion.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApi_Health.BLL.Enum;
using WebApi_Health.Models.Models.Food;
using WebApi_Health.Models.Models.Tag;

namespace WebApi_Health.Models.Response
{
    /// <summary>
    /// 获得订单详情页应答
    /// </summary>
    public class GetOrderInfoResponse
    {

        /// <summary>
        /// 获得订单详情页应答构造函数
        /// </summary>
        public GetOrderInfoResponse(Orders order, Recipe recipe, Restaurant restaurant, List<Food> List_Food, List<FoodType> List_Food_Type, List<Tag> List_Tag, List<CustomerLike> List_CustomerLike, Customer UserModel, List<Tag_Relation> List_Tag_Relation, List<Recipe_foods> list_recipe_foods, List<DataDictionary> list_dictionary, double CoordX, double CoordY)
        {
            #region 菜谱部分
            //食谱Id
            RecipeId = recipe.id;
            //食谱名称
            RecipeName = recipe.name;
            //标签与体质与体质百分比
            if (!recipe.tags.IsNullOrEmpty())
            {
                List_Tag_Relation = List_Tag_Relation.Where(p => p.relationId == recipe.id).ToList();
                List<TagModel> Listscore;
                List<Tag> ListTags = new List<Tag>();
                Tags = new ArrayList();
                foreach (var ArrayItem in List_Tag_Relation)
                {
                    var model = List_Tag.Find(p => p.id == ArrayItem.tagId);
                    if (model != null)
                    {
                        ListTags.Add(model);
                        Tags.Add(model.name);
                    }
                }
                #region 计算此菜适合体质
                Listscore = BLL.Function.StringHandle.Instance.ConstitutionCalulate(ListTags);
                foreach (var item in Listscore)
                {
                    if (item.name == UserModel.constitution)
                    {
                        ConstitutionPercentage = (int)item.score;
                    }
                }
                Constitution = new ArrayList();
                Constitution.Add(Listscore[0].name);
                Constitution.Add(Listscore[1].name);
                Constitution.Add(Listscore[2].name);
                #endregion
            }
            //食物菜谱
            if (!recipe.foodtypes.IsNullOrEmpty() && !recipe.foods.IsNullOrEmpty())
            {
                list_recipe_foods = list_recipe_foods.Where(p => p.recipeId == recipe.id).ToList();
                var list_model = BLL.Function.StringHandle.Instance.FoodListConvert(list_recipe_foods, List_Food_Type, List_Food, List_CustomerLike);
                FoodRecipe = new List<RecipeItemModel>();
                foreach (var item in list_model)
                {
                    RecipeItemModel recipeItem = new RecipeItemModel(item);
                    FoodRecipe.Add(recipeItem);
                }
            }

            #endregion

            #region 订单部分
            //订单价格
            double? Price = order.RecipePrice.ParseDouble();
            OrderPrice = String.Format("{0:F}", Price);
            //订单时间
            OrderTime = order.CreateTime;
            #endregion

            #region 餐厅部分
            //餐厅Id
            RestaurantId = restaurant.id;
            //餐厅图片
            if (!restaurant.thumbnail.IsNullOrEmpty())
            {
                RestaurantImage = AppSetting.ImageUrl + restaurant.thumbnail;
            }
            //餐厅名称
            RestaurantName = restaurant.name;
            //餐厅类型
            var dic = list_dictionary.Where(p => p.id == restaurant.category.ParseInt().Value).FirstOrDefault();
            RestaurantType = dic == null ? "" : dic.typeValue;
            //餐厅距离
            string[] coordinate = restaurant.coordinate.Split(',');
            double coordX = double.Parse(coordinate[0]);
            double coordY = double.Parse(coordinate[1]);
            var Distance = BaiduMapHelper.Instance.getDistance(CoordX, CoordY, coordX, coordY);
            if (Distance < AppSetting.SeachRange)
            {
                RestaurantDistance = Math.Round(Distance);
            }
            //餐厅地址
            RestaurantAddress = restaurant.address;
            //餐厅电话
            RestaurantPhone = restaurant.phone;
            #endregion
        }

        /// <summary>
        /// 食谱Id
        /// </summary>
        public Int32 RecipeId { get; set; }

        /// <summary>
        /// 食谱名称
        /// </summary>
        public String RecipeName { get; set; }

        /// <summary>
        /// 体质
        /// </summary>
        public ArrayList Constitution { get; set; }

        /// <summary>
        /// 体质百分比
        /// </summary>
        public int ConstitutionPercentage { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public ArrayList Tags { get; set; }

        /// <summary>
        /// 食物菜谱
        /// </summary>
        public List<RecipeItemModel> FoodRecipe { get; set; }

        /// <summary>
        /// 订单价格
        /// </summary>
        public string OrderPrice { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        ///  餐厅Id
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        ///  餐厅名称
        /// </summary>
        public string RestaurantName { get; set; }

        /// <summary>
        ///  餐厅图片
        /// </summary>
        public string RestaurantImage { get; set; }

        /// <summary>
        ///  餐厅类型
        /// </summary>
        public string RestaurantType { get; set; }

        /// <summary>
        ///  餐厅距离
        /// </summary>
        public double RestaurantDistance { get; set; }

        /// <summary>
        ///  餐厅地址
        /// </summary>
        public string RestaurantAddress { get; set; }

        /// <summary>
        ///  餐厅电话
        /// </summary>
        public string RestaurantPhone { get; set; }
    }

}