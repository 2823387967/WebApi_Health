using System;
using Common.Extend;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace WebApi_Health.Models.Response
{
    public class GetRecipeListResponse
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();
        public GetRecipeListResponse()
        {

        }
        public GetRecipeListResponse(Recipe recipe, List<Food> List_Food, List<FoodType> List_Food_Type, List<Tag> List_Tag)
        {
            id = recipe.id;
            name = recipe.name;
            available = recipe.available;
            if (!recipe.foods.IsNullOrEmpty())
            {
                foods = new List<List<Models.Food.FoodModel>>();
                var ArrayListFood = recipe.foods.Split('|');
                foreach (var ArrayListItem in ArrayListFood)
                {
                    List<Models.Food.FoodModel> List_FoodModel = new List<Models.Food.FoodModel>();
                    var ArrayFoods = ArrayListItem.Split(';');
                    foreach (var ArrayItem in ArrayFoods)
                    {
                        var Array_Food = ArrayItem.Split(',');
                        Models.Food.FoodModel foodModel = new Models.Food.FoodModel();
                        foodModel.FoodWeight = Array_Food[1];
                        var model = List_Food.Find(p => p.id.ToString() == Array_Food[0]);
                        foodModel.FoodName = model == null ? null : model.name;
                        List_FoodModel.Add(foodModel);
                    }
                    foods.Add(List_FoodModel);
                }
            }
            if (!recipe.foodtypes.IsNullOrEmpty())
            {
                foodtypes = new ArrayList();
                var ArrayFoodType = recipe.foodtypes.Split('|');
                foreach (var ArrayItem in ArrayFoodType)
                {
                    var model = List_Food_Type.Find(p => p.id.ToString() == ArrayItem);
                    if (model != null)
                    {
                        foodtypes.Add(model.name);
                    }
                }
            }
            if (!recipe.tags.IsNullOrEmpty())
            {
                tags = new ArrayList();
                var Arraytag = recipe.tags.Split('|');
                List<Models.Tag.TagModel> Listscore = new List<Models.Tag.TagModel>();
                List<Tag> ListTags = new List<Tag>();
                foreach (var ArrayItem in Arraytag)
                {
                    var model = List_Tag.Find(p => p.id.ToString() == ArrayItem);
                    if (model != null)
                    {
                        ListTags.Add(model);
                        tags.Add(model.name);
                    }
                }
                Listscore = WebApi_Health.BLL.Function.StringHandle.Instance.ConstitutionCalulate(ListTags);
                score = Listscore;
            }
            if (!recipe.images.IsNullOrEmpty())
            {
                images = new ArrayList();
                foreach (var item in recipe.images.Split('|'))
                {
                    images.Add(ImageUrl + item);
                }
            }
            sales = recipe.sales;
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
        /// 是否还有存货
        /// </summary>
        public Boolean? available { get; set; }
        /// <summary>
        /// 菜品
        /// </summary>
        public List<List<Models.Food.FoodModel>> foods { get; set; }
        /// <summary>
        /// 菜品种类
        /// </summary>
        public ArrayList foodtypes { get; set; }
        /// <summary>
        /// 餐厅Id
        /// </summary>
        public Int32 restaurantId { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public ArrayList tags { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public List<Models.Tag.TagModel> score { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public ArrayList images { get; set; }
        /// <summary>
        /// 销售量
        /// </summary>
        public Int32? sales { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
    }

}