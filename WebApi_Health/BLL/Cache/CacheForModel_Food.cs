using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelFood : SingleTon<CacheForModelFood>
    {
        /// <summary>
        /// 获取食物列表
        /// </summary>
        /// <returns></returns>
        public List<Food> GetFoodList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Food>>("ListFood");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Food model = new Food();
                ListModel = FoodOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListFood", ListModel, outTime);
                if(ListModel==null)
                {
                    ListModel = new List<Food>();
                }
            }
            return ListModel;
        }
    }
}