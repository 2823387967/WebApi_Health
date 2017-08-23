using Common;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebApi_Health.Models.Models.Tag;
using WebApi_Health.Models.Models.Food;
using Common.Extend;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.BLL.Function
{
    /// <summary>
    /// 字符串处理
    /// </summary>
    public class StringHandle : SingleTon<StringHandle>
    {


        /// <summary>
        /// 比例分母
        /// </summary>
        private int RatioDenominator = int.Parse(ConfigurationManager.AppSettings["RatioDenominator"]);

        /// <summary>
        /// 用户等级
        /// </summary>
        private string User_Level_String = ConfigurationManager.AppSettings["User_Level"];

        /// <summary>
        /// 用户等级列表
        /// </summary>
        private static List<User_Level> List_User_Level;


        /// <summary>
        /// 体质计算
        /// </summary>
        public List<TagModel> ConstitutionCalulate(List<Tag> ListTags)
        {
            List<TagModel> Listscore = new List<TagModel>();
            TagModel model1 = new TagModel();
            model1.name = "平和质";
            TagModel model2 = new TagModel();
            model2.name = "气郁质";
            TagModel model3 = new TagModel();
            model3.name = "阴虚质";
            TagModel model4 = new TagModel();
            model4.name = "痰湿质";
            TagModel model5 = new TagModel();
            model5.name = "阳虚质";
            TagModel model6 = new TagModel();
            model6.name = "特禀质";
            TagModel model7 = new TagModel();
            model7.name = "湿热质";
            TagModel model8 = new TagModel();
            model8.name = "气虚质";
            TagModel model9 = new TagModel();
            model9.name = "血瘀质";
            foreach (var item in ListTags)
            {
                model1.score += item.pinghescore;
                model2.score += item.qiyuscore;
                model3.score += item.yinxuscore;
                model4.score += item.tanshiscore;
                model5.score += item.yangxuscore;
                model6.score += item.tebingscore;
                model7.score += item.shirescore;
                model8.score += item.qixuscore;
                model9.score += item.xueyuscore;
            }
            model1.score = model1.score * 100 / ListTags.Count / RatioDenominator;
            model2.score = model2.score * 100 / ListTags.Count / RatioDenominator;
            model3.score = model3.score * 100 / ListTags.Count / RatioDenominator;
            model4.score = model4.score * 100 / ListTags.Count / RatioDenominator;
            model5.score = model5.score * 100 / ListTags.Count / RatioDenominator;
            model6.score = model6.score * 100 / ListTags.Count / RatioDenominator;
            model7.score = model7.score * 100 / ListTags.Count / RatioDenominator;
            model8.score = model8.score * 100 / ListTags.Count / RatioDenominator;
            model9.score = model9.score * 100 / ListTags.Count / RatioDenominator;
            Listscore.Add(model1);
            Listscore.Add(model2);
            Listscore.Add(model3);
            Listscore.Add(model4);
            Listscore.Add(model5);
            Listscore.Add(model6);
            Listscore.Add(model7);
            Listscore.Add(model8);
            Listscore.Add(model9);
            Listscore = Listscore.OrderByDescending(p => p.score).ToList();
            return Listscore;
        }

        /// <summary>
        /// 食物列表转换
        /// </summary>
        /// <param name="List_Recipe_Foods">菜谱食物关联表</param>
        /// <param name="List_Food_Type">食物类型列表</param>
        /// <param name="List_Food">食物列表</param>
        /// <param name="List_CustomerLike">用户喜欢列表</param>
        /// <returns></returns>
        public List<RecipeModel> FoodListConvert(List<Recipe_foods> List_Recipe_Foods, List<FoodType> List_Food_Type, List<Food> List_Food, List<CustomerLike> List_CustomerLike)
        {
            List<RecipeModel> list_model = new List<RecipeModel>();
            //解析菜品种类字符串
            var FoodType = List_Recipe_Foods.OrderBy(p => p.foodtypeId).GroupBy(p => p.foodtypeId).Where(p => !p.Key.IsNullOrEmpty()).Select(p => p.Key).ToList();
            foreach (var ArrayItem in FoodType)
            {
                var model = List_Food_Type.Find(p => p.id == ArrayItem);
                if (model != null)
                {
                    RecipeModel RecipeModel = new RecipeModel();
                    RecipeModel.ListFood = new List<FoodModel>();
                    RecipeModel.RecipeId = model.id;
                    RecipeModel.RecipeItemName = model.name;
                    var FoodList = List_Recipe_Foods.Where(p => p.foodtypeId == ArrayItem).ToList();
                    foreach (var item in FoodList)
                    {
                        var FoodInfo = List_Food.Find(p => p.id == item.foodId);
                        FoodModel foodmodel = new FoodModel();
                        foodmodel.FoodId = item.foodId.GetValueOrDefault();
                        foodmodel.FoodName = FoodInfo.name;
                        foodmodel.FoodWeight = item.weight.ToString() + "g";
                        var Custom_like = List_CustomerLike.Where(p => p.lid == foodmodel.FoodId).FirstOrDefault();
                        if (Custom_like != null)
                        {
                            if (Custom_like.type.IsNullOrEmpty() || Custom_like.type.ToLower() == UserLikeTypeVariable.RestLike)
                            {
                                foodmodel.WhetherLike = null;
                            }
                            else if (Custom_like.type.ToLower() == UserLikeTypeVariable.FoodLike)
                            {
                                foodmodel.WhetherLike = true;
                            }
                            else if (Custom_like.type.ToLower() == UserLikeTypeVariable.FoodUnLike)
                            {
                                foodmodel.WhetherLike = false;
                            }
                        }
                        RecipeModel.ListFood.Add(foodmodel);
                    }
                    list_model.Add(RecipeModel);
                }
            }
            return list_model;
        }

        /// <summary>
        /// 食物列表转换
        /// </summary>
        /// <param name="recipe">食谱</param>
        /// <param name="List_Recipe_Foods">菜谱食物关联表</param>
        /// <param name="List_Food_Type">食物类型列表</param>
        /// <param name="List_Food">食物列表</param>
        /// <returns></returns>
        public List<RecipeModel> FoodListConvert(Recipe recipe, List<Recipe_foods> List_Recipe_Foods, List<FoodType> List_Food_Type, List<Food> List_Food)
        {
            List<RecipeModel> list_model = new List<RecipeModel>();
            var FoodType = List_Recipe_Foods.Where(p => p.recipeId == recipe.id).GroupBy(p => p.foodtypeId).Where(p => !p.Key.IsNullOrEmpty()).Select(p => p.Key).ToList();
            foreach (var ArrayItem in FoodType)
            {
                var model = List_Food_Type.Find(p => p.id == ArrayItem);
                if (model != null)
                {
                    RecipeModel RecipeModel = new RecipeModel();
                    RecipeModel.ListFood = new List<FoodModel>();
                    RecipeModel.RecipeId = model.id;
                    RecipeModel.RecipeItemName = model.name;
                    var FoodList = List_Recipe_Foods.OrderBy(p => p.foodtypeId).Where(p => p.foodtypeId == ArrayItem && p.recipeId == recipe.id).ToList();
                    foreach (var item in FoodList)
                    {
                        var FoodInfo = List_Food.Find(p => p.id == item.foodId);
                        FoodModel foodmodel = new FoodModel();
                        foodmodel.FoodId = item.foodId.GetValueOrDefault();
                        foodmodel.FoodName = FoodInfo.name;
                        foodmodel.FoodWeight = item.weight.ToString() + "g";
                        RecipeModel.ListFood.Add(foodmodel);
                    }
                    list_model.Add(RecipeModel);
                }
            }
            return list_model;
        }
        /// <summary>
        /// 转换用户等级列表
        /// </summary>
        /// <returns></returns>
        public List<User_Level> ConvertToListUserLevel()
        {
            if (List_User_Level == null)
            {
                List_User_Level = new List<User_Level>();
            }
            if (List_User_Level.Count == 0)
            {
                var ArrayLevel = User_Level_String.Split('|');
                var count = 1;
                foreach (var Level in ArrayLevel)
                {
                    User_Level model = new User_Level();
                    var ListItem = Level.Split(',');
                    if (ListItem.Count() == 3)
                    {
                        var MaxOrMin = ListItem[0].Split('-');
                        if (MaxOrMin.Count() != 2)
                        {
                            List_User_Level.Clear();
                            return List_User_Level;
                        }
                        model.Min_Value = MaxOrMin[0];
                        model.Max_Value = MaxOrMin[1];
                        model.User_Level_Name = ListItem[1].ToString();
                        model.User_Delete_Score = ListItem[2].ToString();
                        model.User_Level_Lv = count;
                        count++;
                        List_User_Level.Add(model);
                    }
                    else if (ListItem.Count() == 2)
                    {
                        var MaxOrMin = ListItem[0].Split('-');
                        if (MaxOrMin.Count() != 2)
                        {
                            List_User_Level.Clear();
                            return List_User_Level;
                        }
                        model.Min_Value = MaxOrMin[0];
                        model.Max_Value = MaxOrMin[1];
                        model.User_Level_Name = ListItem[1].ToString();
                        model.User_Level_Lv = count;
                        count++;
                        List_User_Level.Add(model);
                    }
                    else
                    {
                        List_User_Level.Clear();
                        return List_User_Level;
                    }
                }
                return List_User_Level;
            }
            else
            {
                return List_User_Level;
            }
        }

        /// <summary>
        /// 计算分数
        /// </summary>
        /// <param name="Score">分数</param>
        /// <returns></returns>
        public User_Current CalculateScore(double Score)
        {
            var List_Level = ConvertToListUserLevel();
            foreach (var item in List_Level)
            {
                if (Score > item.Min_Value.ParseInt() && item.Max_Value == "*" ? true : Score < item.Max_Value.ParseInt())
                {
                    User_Current user_current = new User_Current();
                    user_current.Current_Name = item.User_Level_Name;
                    user_current.Current_Score = (double)Math.Round(Score * 10) / 10;
                    user_current.Next_Score = item.Max_Value == "*" ? 0 : Math.Round((item.Max_Value.ParseDouble().GetValueOrDefault() - Score) * 10) / 10;
                    user_current.Current_Lv = item.User_Level_Lv;
                    return user_current;
                }
            }
            return null;
        }
    }


    /// <summary>
    /// 用户等级
    /// </summary>
    public class User_Level
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public string Min_Value { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public string Max_Value { get; set; }
        /// <summary>
        /// 用户等级名称
        /// </summary>
        public string User_Level_Name { get; set; }
        /// <summary>
        /// 用户删除分数
        /// </summary>
        public string User_Delete_Score { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public int User_Level_Lv { get; set; }
    }
    /// <summary>
    /// 用户当前状态
    /// </summary>
    public class User_Current
    {
        /// <summary>
        /// 当前分数
        /// </summary>
        public double Current_Score { get; set; }
        /// <summary>
        /// 到达下级分数
        /// </summary>
        public double Next_Score { get; set; }
        /// <summary>
        /// 当前名称
        /// </summary>
        public string Current_Name { get; set; }
        /// <summary>
        /// 当前等级
        /// </summary>
        public int Current_Lv { get; set; }
    }
}