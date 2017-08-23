using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelRecipe_Foods : SingleTon<CacheForModelRecipe_Foods>
    {
        /// <summary>
        /// 获得菜谱列表
        /// </summary>
        /// <returns></returns>
        public List<Recipe_foods> GetRecipe_FoodsList()
        {

            var ListModel = CacheHelper.Instance.GetCache<List<Recipe_foods>>("Recipe_FoodsList");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Recipe_foods model = new Recipe_foods();
                ListModel = Recipe_foodsOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("Recipe_FoodsList", ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 根据餐厅Id获得菜谱列表
        /// </summary>
        /// <returns></returns>
        public List<Recipe_foods> GetRecipe_FoodsListByRecipeId(int recipeId)
        {

            var ListModel = GetRecipe_FoodsList();
            if (ListModel == null)
            {
                return new List<Recipe_foods>();
            }
            else
            {
                ListModel = ListModel.Where(p => p.recipeId == recipeId).ToList();
            }
            return ListModel;
        }

    }
}