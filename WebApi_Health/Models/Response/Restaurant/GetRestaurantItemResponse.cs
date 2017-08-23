using System;
using Common.Extend;
using System.Configuration;
using System.Collections;
using DbOpertion.Models;
using System.Collections.Generic;
using Common.Helper;
using WebApi_Health.BLL.Enum;

namespace WebApi_Health.Models.Response
{
    public class GetRestaurantItemtResponse
    {
        public GetRestaurantItemtResponse()
        {

        }

        public GetRestaurantItemtResponse(DbOpertion.Models.Restaurant restaurant, double CoordX, double CoordY, int SeachRange, List<DataDictionary> List_DataDictionary)
        {
            address = restaurant.address;
            consumption = restaurant.consumption;
            discount = restaurant.discount;
            id = restaurant.id;
            if (!restaurant.images.IsNullOrEmpty())
            {
                images = new ArrayList();
                foreach (var item in restaurant.images.Split('|'))
                {
                    images.Add(AppSetting.ImageUrl + item);
                }

            }
            name = restaurant.name;
            phone = restaurant.phone;
            sales = restaurant.sales;
            foreach (var item in List_DataDictionary)
            {
                if (restaurant.category == item.id.ToString())
                {
                    category = item.typeValue;
                }
            }
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
        public ArrayList images { get; set; }
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
        public double distance { get; set; }
        /// <summary>
        /// 用户是否喜欢
        /// </summary>
        public bool cusLikeOrNot { get; set; }
}
}