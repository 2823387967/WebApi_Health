using System;
using Common.Extend;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using DbOpertion.Models;
using System.Linq;

namespace WebApi_Health.Models.Response
{
    public class GetRestaurantPayItemResponse
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();

        public GetRestaurantPayItemResponse(Restaurant restaurant, List<DataDictionary> dic)
        {
            address = restaurant.address;
            var cateDic = dic.Where(p => p.id.ToString() == restaurant.category).FirstOrDefault();
            category = cateDic == null ? "" : cateDic.typename;
            category = restaurant.category;
            id = restaurant.id;
            if (!restaurant.images.IsNullOrEmpty())
            {
                titleImage = ImageUrl + restaurant.images.Split('|')[0];
            }
            else
            {
                titleImage = " ";
            }
            name = restaurant.name;
            phone = restaurant.phone;
        }

        /// <summary>
        /// 餐厅Id
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 餐厅名称
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// 餐厅图片
        /// </summary>
        public string titleImage { get; set; }
        /// <summary>
        /// 餐厅地址
        /// </summary>
        public String address { get; set; }

        /// <summary>
        /// 餐厅类型
        /// </summary>
        public String category { get; set; }

        /// <summary>
        /// 餐厅电话
        /// </summary>
        public String phone { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public double distance { get; set; }
    }
}