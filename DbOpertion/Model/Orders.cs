using System;

namespace DbOpertion.Models
{
    public class Orders
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 SellerId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 CustomerId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String RecipeId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String RecipePrice { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Decimal Pay { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String PayType { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? ShopTime { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "Id";
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
