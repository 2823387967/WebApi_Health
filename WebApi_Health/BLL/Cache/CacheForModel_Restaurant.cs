using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi_Health.BLL.Cache
{
    /// <summary>
    /// 模型类缓存
    /// </summary>
    public partial class CacheForModelRestaurant : SingleTon<CacheForModelRestaurant>
    {
        /// <summary>
        /// 获得餐厅列表
        /// </summary>
        /// <returns></returns>
        public List<Restaurant> RestaurantList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Restaurant>>("List_Restaurant");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Restaurant model = new Restaurant();
                ListModel = RestaurantOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("Restaurant", ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 获得餐厅名称列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> RestarantName()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var ListModel = CacheHelper.Instance.GetCache<List<Restaurant>>("List_Restaurant");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Restaurant model = new Restaurant();
                ListModel = RestaurantOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("Restaurant", ListModel, outTime);

            }
            foreach (var item in ListModel)
            {
                dic.Add(item.id, item.name);
            }
            return dic;
        }

        /// <summary>
        /// 通过Id获取餐厅信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Restaurant> GetRestaurantById(int id)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Restaurant>>("List_Restaurant");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Restaurant model = new Restaurant();
                model.id = id;
                ListModel = RestaurantOper.Instance.Select(model);
            }
            else
            {
                var model = ListModel.Where(p => p.id == id).FirstOrDefault();
                ListModel.Clear();
                ListModel.Add(model);
            }
            return ListModel;
        }

        /// <summary>
        /// 通过Name模糊查找餐厅
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Restaurant> GetRestaurantByName(string Name)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Restaurant>>("List_Restaurant");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Restaurant model = new Restaurant();
                model.name = Name;
                ListModel = RestaurantOper.Instance.SelectVagueByRestaurantName(model);
            }
            else
            {
                var model = ListModel.Find(p => p.name.Contains(Name));
                ListModel.Clear();
                ListModel.Add(model);
            }
            return ListModel;
        }
    }
}