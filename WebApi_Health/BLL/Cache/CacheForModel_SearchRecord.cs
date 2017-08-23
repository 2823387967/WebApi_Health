using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi_Health.BLL.Function;
using System.Configuration;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelSearchRecord : SingleTon<CacheForModelSearchRecord>
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());

        private object objLock = new object();
        /// <summary>
        /// 搜索记录列表
        /// </summary>
        /// <returns></returns>
        public List<SearchRecord> SearchRecordList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<SearchRecord>>("SearchRecordList");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                SearchRecord model = new SearchRecord();
                model.OrderBy = "SearchCount Desc";
                ListModel = SearchRecordOper.Instance.SelectByPage(model, PageSize, 1);
                CacheHelper.Instance.SetCache("SearchRecordList", ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 插入搜索记录
        /// </summary>
        /// <returns></returns>
        public void InsertSearchRecord(string Key)
        {
            SearchRecord model = new SearchRecord();
            model.OrderBy = "SearchCount Desc";
            model.SearchKey = Key.Trim();
            lock (objLock)
            {
                var ListModel = SearchRecordOper.Instance.Select(model).FirstOrDefault();
                if (ListModel == null)
                {
                    model.SearchCount = 1;
                    SearchRecordOper.Instance.Insert(model);
                }
                else
                {
                    ListModel.SearchCount++;
                    SearchRecordOper.Instance.Update(ListModel);
                }
            }
        }
    }
}