using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class Score
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 ScoreId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String ScoreType { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime ScoreDate { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Double ScoreNum { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? ScoreClick { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String ScoreContent { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "ScoreId";
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
