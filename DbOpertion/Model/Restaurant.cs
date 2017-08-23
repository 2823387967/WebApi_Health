using System;

namespace DbOpertion.Models
{
    public class Restaurant
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
        public String thumbnail { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String images { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String address { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String phone { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String businesshours { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String category { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String coordinate { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? sales { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? consumption { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String discount { get; set; }
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
