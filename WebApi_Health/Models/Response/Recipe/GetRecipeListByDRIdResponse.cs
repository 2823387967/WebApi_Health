using System;
using Common.Extend;
using DbOpertion.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace WebApi_Health.Models.Response
{
    public class GetRecipeListByDRIdResponse
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();
        public GetRecipeListByDRIdResponse()
        {

        }

        public GetRecipeListByDRIdResponse(Recipe recipe, List<Food> List_Food, List<FoodType> List_Food_Type, List<Tag> List_Tag, List<Tag_Relation> list_tag_relation, List<Recipe_foods> list_recipe_food)
        {
            //食谱Id
            id = recipe.id;
            //食谱名称
            name = recipe.name;
            //餐厅Id
            RestaurantId = recipe.restaurantId == null ? 0 : recipe.restaurantId.Value;
            //食物菜谱
            if (!recipe.foodtypes.IsNullOrEmpty() && !recipe.foods.IsNullOrEmpty())
            {
                var list_model = BLL.Function.StringHandle.Instance.FoodListConvert(recipe, list_recipe_food, List_Food_Type, List_Food);
                foodRecipe = new List<Models.Food.RecipeItemStringModel>();
                foreach (var item in list_model)
                {
                    Models.Food.RecipeItemStringModel recipeItem = new Models.Food.RecipeItemStringModel(item);
                    foodRecipe.Add(recipeItem);
                }
            }
            //标签与体质
            if (!recipe.tags.IsNullOrEmpty())
            {
                list_tag_relation = list_tag_relation.Where(p => p.relationId == recipe.id).ToList();
                List<Models.Tag.TagModel> Listscore;
                List<Tag> ListTags = new List<Tag>();
                foreach (var ArrayItem in list_tag_relation)
                {
                    var model = List_Tag.Find(p => p.id == ArrayItem.tagId);
                    if (model != null)
                    {
                        ListTags.Add(model);
                    }
                }
                #region 计算此菜适合体质
                Listscore = WebApi_Health.BLL.Function.StringHandle.Instance.ConstitutionCalulate(ListTags);
                Constitution = new ArrayList();
                Constitution.Add(Listscore[0].name);
                Constitution.Add(Listscore[1].name);
                Constitution.Add(Listscore[2].name);
                #endregion
            }
            //图片
            if (!recipe.images.IsNullOrEmpty())
            {
                titleImage = ImageUrl + recipe.images.Split('|')[0];
            }
            else
            {
                titleImage = " ";
            }
            //销售量
            sales = recipe.sales;
            //价格
            double? Price = recipe.price.ParseDouble();
            price = String.Format("{0:F}", Price);
        }

        /// <summary>
        /// 食谱Id
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 食谱名称
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// 食物菜谱
        /// </summary>
        public List<Models.Food.RecipeItemStringModel> foodRecipe { get; set; }
        /// <summary>
        /// 体质
        /// </summary>
        public ArrayList Constitution { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string titleImage { get; set; }
        /// <summary>
        /// 销售量
        /// </summary>
        public Int32? sales { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 餐厅ID
        /// </summary>
        public int RestaurantId { get; set; }
    }
}