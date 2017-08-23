using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi_Health.BLL.Function;
using System.Configuration;
using Common.Enum;

namespace WebApi_Health.BLL.Cache
{
    public class CacheForModel_TagRelation : SingleTon<CacheForModel_TagRelation>
    {
        /// <summary>
        /// 获取标签关系列表
        /// </summary>
        /// <returns></returns>
        public List<Tag_Relation> GetTagRelationList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Tag_Relation>>("ListTagRelation");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Tag_Relation model = new Tag_Relation();
                ListModel = Tag_RelationOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListTagRelation", ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 根据relationId获取标签关列表
        /// </summary>
        /// <returns></returns>
        public List<Tag_Relation> GetTagRelationListByRelationId(int relationId)
        {
            var ListModel = GetTagRelationList();
            if (ListModel == null)
            {
                return new List<Tag_Relation>();
            }
            ListModel = ListModel.Where(p => p.relationId == relationId).ToList();
            return ListModel;
        }
    }
}