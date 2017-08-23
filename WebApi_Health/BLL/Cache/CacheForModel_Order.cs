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
using Common.Extend;
using WebApi_Health.BLL.Enum;

namespace WebApi_Health.BLL.Cache
{

    /// <summary>
    /// 订单模型缓存
    /// </summary>
    public partial class CacheForModel_Order : SingleTon<CacheForModel_Order>
    {

        /// <summary>
        /// 根据用户Id获取订单列表
        /// </summary>
        /// <returns></returns>
        public List<Orders> GetOrdersByUserId(int UserId)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Orders>>("ListOrder_" + UserId);
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Orders model = new Orders();
                model.CustomerId = UserId;
                ListModel = OrdersOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListOrder_" + UserId, ListModel, outTime);
                if (ListModel == null)
                {
                    ListModel = new List<Orders>();
                }
            }
            return ListModel;
        }

        /// <summary>
        /// 根据订单Id获取订单列表
        /// </summary>
        /// <returns></returns>
        public Orders GetOrdersByOrderId(int OrderId)
        {
            Orders model = new Orders();
            model.Id = OrderId;
            model = OrdersOper.Instance.Select(model).FirstOrDefault();
            if (model == null)
            {
                model = new Orders();
            }
            return model;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <returns></returns>
        public bool Delete_Order_ById(int OrderId)
        {
            return OrdersOper.Instance.Delete(OrderId);
        }

        /// <summary>
        /// 插入用户到店支付
        /// </summary>
        /// <returns></returns>
        public bool Insert_Order_Pay_At_Shop(int UserId, Recipe recipe, DateTime At_Shop_Time)
        {
            Orders model = new Orders();
            model.CustomerId = UserId;
            model.PayType = Enum_PayType.PayAtShop.Enum_GetString();
            model.Pay = (decimal)recipe.price.ParseDouble().Value;
            model.RecipeId = recipe.id.ToString();
            model.RecipePrice = recipe.price;
            model.SellerId = recipe.restaurantId.Value;
            model.CreateTime = DateTime.Now;
            model.ShopTime = At_Shop_Time;
            return OrdersOper.Instance.Insert(model);
        }
    }
}