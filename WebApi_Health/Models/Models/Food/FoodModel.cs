using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Models.Food
{

    /// <summary>
    /// 食物模型
    /// </summary>
    public class FoodModel
    {
        /// <summary>
        /// 食物Id
        /// </summary>
        public int FoodId { get; set; }
        /// <summary>
        /// 食物名称
        /// </summary>
        public string FoodName { get; set; }
        /// <summary>
        /// 食物重量
        /// </summary>
        public string FoodWeight { get; set; }
        /// <summary>
        /// 是否喜欢
        /// </summary>
        public bool? WhetherLike { get; set; }
    }
    /// <summary>
    /// 食物是否喜欢模型
    /// </summary>
    public class FoodItemLikeModel
    {
        public FoodItemLikeModel(FoodModel model)
        {
            FoodId = model.FoodId;
            FoodName = model.FoodName;
            FoodWeight = model.FoodWeight;
            if (model.WhetherLike == null)
            {
                WhetherLike = 0;
            }
            else if (model.WhetherLike.Value == false)
            {
                WhetherLike = 1;
            }
            else if (model.WhetherLike.Value == true)
            {
                WhetherLike = 2;
            }
        }
        /// <summary>
        /// 食物名称
        /// </summary>
        public int FoodId { get; set; }
        /// <summary>
        /// 食物名称
        /// </summary>
        public string FoodName { get; set; }
        /// <summary>
        /// 食物重量
        /// </summary>
        public string FoodWeight { get; set; }
        /// <summary>
        /// 是否喜欢
        /// </summary>
        public int WhetherLike { get; set; }
    }

    /// <summary>
    /// 食谱Item模型
    /// </summary>
    public class RecipeModel
    {
        /// <summary>
        /// 食谱ItemId
        /// </summary>
        public int RecipeId { get; set; }
        /// <summary>
        /// 食谱Item名称
        /// </summary>
        public string RecipeItemName { get; set; }
        /// <summary>
        /// 食物列表
        /// </summary>
        public List<FoodModel> ListFood { get; set; }
    }
    /// <summary>
    /// 食谱Item模型
    /// </summary>
    public class RecipeItemStringModel
    {
        public RecipeItemStringModel(RecipeModel model)
        {
            RecipeItemName = model.RecipeItemName;
            ListFood = "";
            if (model.ListFood != null)
            {
                bool flag = true;
                foreach (var item in model.ListFood)
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        ListFood += ", ";
                    }
                    ListFood += item.FoodName + " " + item.FoodWeight;
                }
            }
        }
        /// <summary>
        /// 食谱Item名称
        /// </summary>
        public string RecipeItemName { get; set; }
        /// <summary>
        /// 食物列表
        /// </summary>
        public string ListFood { get; set; }
    }
    /// <summary>
    /// 食谱Item模型
    /// </summary>
    public class RecipeItemModel
    {
        public RecipeItemModel(RecipeModel model)
        {
            RecipeId = model.RecipeId;
            RecipeItemName = model.RecipeItemName;
            ListFood = new List<FoodItemLikeModel>();
            if (model.ListFood != null)
            {
                foreach (var item in model.ListFood)
                {
                    FoodItemLikeModel food_like_model = new FoodItemLikeModel(item);
                    ListFood.Add(food_like_model);
                }
            }
        }
        /// <summary>
        /// 食谱Item名称
        /// </summary>
        public int RecipeId { get; set; }
        /// <summary>
        /// 食谱Item名称
        /// </summary>
        public string RecipeItemName { get; set; }
        /// <summary>
        /// 食物列表
        /// </summary>
        public List<FoodItemLikeModel> ListFood { get; set; }
    }
}