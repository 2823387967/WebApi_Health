using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelDataDcitionarys : SingleTon<CacheForModelDataDcitionarys>
    {
        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public List<DataDictionary> GetDataDictionaryList(string Key)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<DataDictionary>>("ListDic_" + Key);
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.DataDictionaryCacheOutTime;
                DataDictionary model = new DataDictionary();
                model.typename = Key;
                ListModel = DataDictionaryOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListDic_" + Key, ListModel, outTime);
            }
            return ListModel;
        }
    }
}