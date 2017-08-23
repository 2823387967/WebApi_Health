using Common.Result;
using System.Web.Http;
using Common.Filter;
using WebApi_Health.Models.Response;
using WebApi_Health.BLL.ControllerBiz;

namespace WebApi_Health.Controllers
{
    /// <summary>
    /// 数据字典接口
    /// </summary>
    public class DataDictionaryController : ApiController
    {
        /// <summary>
        /// 美食类型
        /// </summary>
        /// <returns></returns>
        [WebApiException]
        [HttpPost]
        public ResultJson<GetDataDictionaryResponse> Cate()
        {
            return DataDictionaryBiz.Instance.Cate();
        }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        [WebApiException]
        [HttpGet]
        public ResultJson<GetDataDictionaryResponse> Sex()
        {
            return DataDictionaryBiz.Instance.Sex();
        }
    }
}