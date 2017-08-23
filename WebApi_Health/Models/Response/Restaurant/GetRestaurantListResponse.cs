using System;
using Common.Extend;
using Common.Helper;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using DbOpertion.Models;

namespace WebApi_Health.Models.Response
{
    public class GetRestaurantListResponse
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();
        public GetRestaurantListResponse()
        {

        }
        public GetRestaurantListResponse(DbOpertion.Models.Restaurant restaurant, double CoordX, double CoordY, int SeachRange)
        {
            address = restaurant.address;
            consumption = restaurant.consumption;
            discount = restaurant.discount;
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
            sales = restaurant.sales;
            category = restaurant.category;
            string[] coordinate = restaurant.coordinate.Split(',');
            double coordX = double.Parse(coordinate[0]);
            double coordY = double.Parse(coordinate[1]);
            var Distance = BaiduMapHelper.Instance.getDistance(CoordX, CoordY, coordX, coordY);
            if (Distance < SeachRange)
            {
                distance = Math.Round(Distance);
            }
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
        /// 餐厅电话
        /// </summary>
        public String phone { get; set; }
        /// <summary>
        /// 餐厅类型
        /// </summary>
        public String category { get; set; }
        /// <summary>
        /// 餐厅销售量
        /// </summary>
        public Int32? sales { get; set; }
        /// <summary>
        /// 餐厅平均消费
        /// </summary>
        public Int32? consumption { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public String discount { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public double? distance { get; set; }
    }
}