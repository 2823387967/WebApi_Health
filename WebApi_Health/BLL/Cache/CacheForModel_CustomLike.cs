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
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public List<CustomerLike> GetCustomLike(int id)
        {
            int outTime = CacheHelper.Instance.CacheOutTime;
            CustomerLike model = new CustomerLike();
            model.cid = id;
            var ListModel = CustomerLikeOper.Instance.Select(model);
            return ListModel;
        }

        /// <summary>
        /// 获取用户喜欢餐厅
        /// </summary>
        /// <param name="Userid">用户Id</param>
        /// <param name="RestId">餐厅Id</param>
        /// <returns></returns>
        public CustomerLike GetCustomLikeRest(int Userid, int RestId)
        {
            int outTime = CacheHelper.Instance.CacheOutTime;
            CustomerLike model = new CustomerLike();
            model.cid = Userid;
            model.lid = RestId;
            model.type = UserLikeTypeVariable.RestLike;
            var Model = CustomerLikeOper.Instance.Select(model).FirstOrDefault();
            return Model;
        }

        /// <summary>
        /// 获取用户喜欢文章列表
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <returns></returns>
        public List<CustomerLike> GetCustomLikeArticleList(int Userid)
        {
            int outTime = CacheHelper.Instance.CacheOutTime;
            CustomerLike model = new CustomerLike();
            model.cid = Userid;
            model.type = UserLikeTypeVariable.ArticleLike;
            var ListModel = CustomerLikeOper.Instance.Select(model);
            return ListModel;
        }

        /// <summary>
        /// 设置用户喜欢餐厅
        /// </summary>
        /// <param name="Userid">用户Id</param>
        /// <param name="RestId">餐厅Id</param>
        /// <returns></returns>
        public bool InsertCustomLikeRest(int Userid, int RestId)
        {
            int outTime = CacheHelper.Instance.CacheOutTime;
            CustomerLike model = new CustomerLike();
            model.cid = Userid;
            model.lid = RestId;
            model.type = UserLikeTypeVariable.RestLike;
            var ListModel = CustomerLikeOper.Instance.Select(model).FirstOrDefault();
            if (ListModel == null)
            {
                return CustomerLikeOper.Instance.Insert(model);
            }
            return false;
        }
    }
}