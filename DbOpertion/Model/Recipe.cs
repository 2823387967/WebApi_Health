using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class Recipe
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
        public Boolean? available { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String foodtypes { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String foods { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? restaurantId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String tags { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String images { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? sales { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String price { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
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
