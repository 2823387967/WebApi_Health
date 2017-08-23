using Common;
using Common.Enum;
using Common.Result;
using System.Collections.Generic;
using WebApi_Health.BLL.Cache;
using WebApi_Health.Models.Response;

namespace WebApi_Health.BLL.ControllerBiz
{
    public class DataDictionaryBiz : SingleTon<DataDictionaryBiz>
    {
        /// <summary>
        /// 美食类型
        /// </summary>
        /// <returns></returns>
        public ResultJson<GetDataDictionaryResponse> Cate()
        {
            return SearchByType("餐厅类型");
        }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public ResultJson<GetDataDictionaryResponse> Sex()
        {
            return SearchByType("性别");
        }

        /// <summary>
        /// 根据类型获取Type
        /// </summary>
        public ResultJson<GetDataDictionaryResponse> SearchByType(string DicType)
        {
            ResultJson<GetDataDictionaryResponse> resultJson = new ResultJson<GetDataDictionaryResponse>();
            List<GetDataDictionaryResponse> ListResponse = new List<GetDataDictionaryResponse>();
            var ListDic = CacheForModelDataDcitionarys.Instance.GetDataDictionaryList(DicType);
            foreach (var dic in ListDic)
            {
                GetDataDictionaryResponse item = new GetDataDictionaryResponse(dic);
                ListResponse.Add(item);
            }
            if (ListResponse.Count == 0)
            {
                resultJson.HttpCode = 300;
                resultJson.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                resultJson.HttpCode = 200;
                resultJson.ListData = ListResponse;
            }
            return resultJson;
        }

    }
}