using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;

namespace WebApi_Health.BLL.Cache
{
    /// <summary>
    /// 食物类型缓存
    /// </summary>
    public partial class CacheForModelFoodType : SingleTon<CacheForModelFoodType>
    {
        /// <summary>
        /// 获取食物类型列表
        /// </summary>
        /// <returns></returns>
        public List<FoodType> GetFoodTypeList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<FoodType>>("ListFoodType");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                FoodType model = new FoodType();
                ListModel = FoodTypeOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListFoodType", ListModel, outTime);
                if(ListModel==null)
                {
                    ListModel = new List<FoodType>();
                }
            }
            return ListModel;
        }
    }
}