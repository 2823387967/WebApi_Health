using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class SearchRecord
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 SearchId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String SearchKey { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 SearchCount { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "SearchId";
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
