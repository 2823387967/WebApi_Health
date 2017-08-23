using System;

namespace DbOpertion.Models
{
    public class Tag
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
        public Int32 pinghescore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 qiyuscore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 yinxuscore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 tanshiscore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 yangxuscore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 tebingscore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 shirescore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 qixuscore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 xueyuscore { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? isDeleted { get; set; }
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
