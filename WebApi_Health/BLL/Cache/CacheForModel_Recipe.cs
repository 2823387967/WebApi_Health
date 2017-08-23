using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelRecipe : SingleTon<CacheForModelRecipe>
    {
        /// <summary>
        /// 获得菜谱列表
        /// </summary>
        /// <returns></returns>
        public List<Recipe> GetRecipeList()
        {

            var ListModel = CacheHelper.Instance.GetCache<List<Recipe>>("List_Recipe");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Recipe model = new Recipe();
                ListModel = RecipeOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("List_Recipe", ListModel, outTime);
                if (ListModel == null)
                {
                    ListModel = new List<Recipe>();
                }
            }
            return ListModel;
        }
        /// <summary>
        /// 根据餐厅Id获得菜谱列表
        /// </summary>
        /// <returns></returns>
        public List<Recipe> GetRecipeListByDRId(int RestaurantId)
        {

            var ListModel = CacheHelper.Instance.GetCache<List<Recipe>>("List_Recipe");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Recipe model = new Recipe();
                model.restaurantId = RestaurantId;
                ListModel = RecipeOper.Instance.Select(model);
            }
            else
            {
                ListModel = ListModel.Where(p => p.restaurantId == RestaurantId).ToList();
            }
            return ListModel;
        }

        /// <summary>
        /// 根据菜谱Id获得菜谱
        /// </summary>
        /// <returns></returns>
        public List<Recipe> GetRecipeListByRecipeId(int RecipeId)
        {
            var ListModel = GetRecipeList();
            if (ListModel.Count != 0)
            {
                ListModel = ListModel.Where(p => p.id == RecipeId).ToList();
            }
            return ListModel;
        }

        /// <summary>
        /// 根据菜谱名称获得菜谱
        /// </summary>
        /// <returns></returns>
        public List<Recipe> GetRecipeListByRecipeName(string RecipeName)
        {

            var ListModel = CacheHelper.Instance.GetCache<List<Recipe>>("List_Recipe");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Recipe model = new Recipe();
                model.name = RecipeName;
                ListModel = RecipeOper.Instance.SelectVagueByRecipeName(model);
            }
            else
            {
                ListModel = ListModel.Where(p => p.name.Contains(RecipeName)).ToList();
            }
            return ListModel;
            //var ListModel = GetRecipeList();
            //if (ListModel.Count != 0)
            //{
            //    ListModel = ListModel.Where(p => p.name.Contains(RecipeName)).ToList();
            //}
            //return ListModel;
        }
    }
}