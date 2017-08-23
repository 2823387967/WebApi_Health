using System;

namespace DbOpertion.Models
{
    public class Customer
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
        public String phone { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String wechat { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String password { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String height { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String weight { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? sex { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? birthday { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String labourIntensity { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String constitution { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? score { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String HeadImage { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Double? UserScore { get; set; }
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
