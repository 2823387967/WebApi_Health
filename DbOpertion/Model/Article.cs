using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class Article
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String title { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String url { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String content { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String thumbnail { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String tags { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? cilckCount { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? loveCount { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? aTime { get; set; }
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
