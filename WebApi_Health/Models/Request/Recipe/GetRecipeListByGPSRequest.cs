using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class GetRecipeListByGPSRequest : UserIDRequest
    {
        /// <summary>
        /// 坐标X
        /// </summary>
        [Required(ErrorMessage = "X轴坐标请填写", AllowEmptyStrings = false)]
        public double CoordX { get; set; }
        /// <summary>
        /// 坐标Y
        /// </summary>
        [Required(ErrorMessage = "Y轴坐标请填写", AllowEmptyStrings = false)]
        public double CoordY { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string GroupBy { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        [PageNoValid]
        public int PageNo { get; set; }
        /// <summary>
        /// 类型值
        /// </summary>
        public string TypeValue { get; set; }
    }
}