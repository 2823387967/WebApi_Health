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
    public class GetOrderListResponse
    {
        /// <summary>
        /// 获得订单详情页应答构造函数
        /// </summary>
        public GetOrderListResponse()
        {

        }
        /// <summary>
        /// 获得订单详情页应答构造函数
        /// </summary>
        public GetOrderListResponse(Orders order, Recipe recipe, Restaurant restaurant, List<FoodType> List_Food_Type, List<Recipe_foods> list_recipe_foods, List<Tag> List_Tag, List<Tag_Relation> List_Tag_Relation, List<Food> List_Food)
        {
            //订单Id
            OrderId = order.Id.ToString();
            //食谱Id
            RecipeId = order.RecipeId;
            //视频名称
            RecipeName = recipe.name;
            //食谱图片
            if (!recipe.images.IsNullOrEmpty())
            {
                var ArrayImage = recipe.images.Split('|');
                RecipeImage = AppSetting.ImageUrl + ArrayImage[0];
            }
            //标签与体质与体质百分比
            if (!recipe.tags.IsNullOrEmpty())
            {
                List_Tag_Relation = List_Tag_Relation.Where(p => p.relationId == recipe.id).ToList();
                List<TagModel> Listscore;
                List<Tag> ListTags = new List<Tag>();
                foreach (var ArrayItem in List_Tag_Relation)
                {
                    var model = List_Tag.Find(p => p.id == ArrayItem.tagId);
                    if (model != null)
                    {
                        ListTags.Add(model);
                    }
                }
                #region 计算此菜适合体质
                Listscore = BLL.Function.StringHandle.Instance.ConstitutionCalulate(ListTags);
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
                var list_model = BLL.Function.StringHandle.Instance.FoodListConvert(recipe, list_recipe_foods, List_Food_Type, List_Food);
                FoodRecipe = new List<RecipeItemStringModel>();
                foreach (var item in list_model)
                {
                    RecipeItemStringModel recipeItem = new RecipeItemStringModel(item);
                    FoodRecipe.Add(recipeItem);
                }
            }
            //订单价格
            OrderPrice = order.RecipePrice;
            //订单时间
            OrderTime = order.CreateTime;
            //餐厅Id
            RestaurantId = restaurant.id;
            //餐厅名称
            RestaurantName = restaurant.name;
        }

        /// <summary>
        /// 订单Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 食谱Id
        /// </summary>
        public string RecipeId { get; set; }

        /// <summary>
        /// 食谱名称
        /// </summary>
        public String RecipeName { get; set; }

        /// <summary>
        ///  食谱图片
        /// </summary>
        public string RecipeImage { get; set; }

        /// <summary>
        /// 体质
        /// </summary>
        public ArrayList Constitution { get; set; }

        /// <summary>
        /// 食物菜谱
        /// </summary>
        public List<RecipeItemStringModel> FoodRecipe { get; set; }

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
    }

}