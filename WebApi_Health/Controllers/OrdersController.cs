using Common.Enum;
using Common.Extend;
using Common.Filter;
using Common.Result;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi_Health.BLL.Cache;
using WebApi_Health.BLL.ControllerBiz;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Response;

namespace WebApi_Health.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    public class OrdersController : ApiController
    {

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [WebApiException]
        [HttpPost]
        [ValidateModel]
        public ResultJsonModel<GetOrderInfoResponse> OrderInfo(GetOrderInfoRequest request)
        {
            return OrdersBiz.Instance.OrderInfo(request);
        }

        /// <summary>
        /// 获得订单信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [WebApiException]
        [HttpPost]
        [ValidateModel]
        public ResultJson<GetOrderListResponse> OrderList(GetInfoByUserIdPageRequest request)
        {
            return OrdersBiz.Instance.OrderList(request);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [WebApiException]
        [HttpPost]
        [ValidateModel]
        public ResultJson<GetOrderListResponse> DeleteOrder(GetInfoByOrderIdRequest request)
        {
            return OrdersBiz.Instance.DeleteOrder(request);
        }

        /// <summary>
        /// 到店支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [WebApiException]
        [HttpPost]
        [ValidateModel]
        public ResultJson PayAtShopOrder(InsertPayAtShopRequest request)
        {
            return OrdersBiz.Instance.PayAtShopOrder(request);
        }
    }
}