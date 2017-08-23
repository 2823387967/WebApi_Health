using System;

namespace DbOpertion.Models
{
    public class Seller
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String name { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String loginname { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String password { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 restaurantid { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String recipeids { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Decimal balance { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "id";
        }
        /// <summary>
        /// 排序语句格式为 字段名,字段名,字段名...
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 排序语句 字段名,字段名,字段名...
        /// </summary>
        public string GroupBy { get; set; }
        /// <summary>
        /// 筛选字段
        /// </summary>
        public string Field { get; set; }

}
}
