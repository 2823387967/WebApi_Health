using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelCustomerLike : SingleTon<CacheForModelCustomerLike>
    {
        /// <summary>
        /// 获取用户喜欢列表
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public List<CustomerLike> GetCustomLike(int id)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<CustomerLike>>("CustomerLike");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                CustomerLike model = new CustomerLike();
                model.cid = id;
                ListModel = CustomerLikeOper.Instance.Select(model);
            }
            else
            {
                ListModel = ListModel.Where(p => p.cid == id).ToList();
            }
            return ListModel;
        }

        /// <summary>
        /// 获取用户喜欢餐厅
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public CustomerLike GetCustomLikeRest(int Userid, int RestId)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<CustomerLike>>("CustomerLike");
            CustomerLike customer_like_model = new CustomerLike();
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                CustomerLike model = new CustomerLike();
                model.cid = Userid;
                model.lid = RestId;
                model.type = UserLikeTypeVariable.RestLike;
                ListModel = CustomerLikeOper.Instance.Select(model);
                customer_like_model = ListModel.FirstOrDefault();
            }
            else
            {
                customer_like_model = ListModel.Where(p => p.cid == Userid && p.lid == RestId && p.type == UserLikeTypeVariable.RestLike).FirstOrDefault(); ;
            }
            return customer_like_model;
        }

        /// <summary>
        /// 获取用户喜欢文章列表
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <returns></returns>
        public List<CustomerLike> GetCustomLikeArticleList(int Userid)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<CustomerLike>>("CustomerLike");
            List<CustomerLike> customer_like_model = new List<CustomerLike>();
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                CustomerLike model = new CustomerLike();
                model.cid = Userid;
                model.type = UserLikeTypeVariable.ArticleLike;
                ListModel = CustomerLikeOper.Instance.Select(model);
                customer_like_model = ListModel;
            }
            else
            {
                customer_like_model = ListModel.Where(p => p.cid == Userid && p.type == UserLikeTypeVariable.ArticleLike).ToList();
            }
            return customer_like_model;
        }

        /// <summary>
        /// 设置用户喜欢餐厅
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public CustomerLike InsertCustomLikeRest(int Userid, int RestId)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<CustomerLike>>("CustomerLike");
            CustomerLike customer_like_model = new CustomerLike();
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                CustomerLike model = new CustomerLike();
                model.cid = Userid;
                model.lid = RestId;
                model.type = UserLikeTypeVariable.RestLike;
                ListModel = CustomerLikeOper.Instance.Select(model);
                customer_like_model = ListModel.FirstOrDefault();
            }
            else
            {
                customer_like_model = ListModel.Where(p => p.cid == Userid && p.lid == RestId && p.type == UserLikeTypeVariable.RestLike).FirstOrDefault(); ;
            }
            return customer_like_model;
        }
    }
}