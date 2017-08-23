using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelTag : SingleTon<CacheForModelTag>
    {
        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        public List<Tag> GetTagList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Tag>>("ListTag");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.TagCacheOutTime;
                Tag model = new Tag();
                ListModel = TagOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListTag", ListModel, outTime);
            }
            return ListModel;
        }
    }
}