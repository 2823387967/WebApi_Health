using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Attribute;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Common.Attribute.Constant;

namespace WebApi_Health.Models.Request
{
    public class GetRestaurantListByNameRequest : UserIDRequest
    {
        /// <summary>
        /// 坐标X
        /// </summary>
        [Required(ErrorMessage = "请填写X轴坐标", AllowEmptyStrings = false)]
        public double CoordX { get; set; }
        /// <summary>
        /// 坐标Y
        /// </summary>
        [Required(ErrorMessage = "请填写Y轴坐标", AllowEmptyStrings = false)]
        public double CoordY { get; set; }
        /// <summary>
        /// 餐厅名称
        /// </summary>
        [SearchValid(ErrorMessage = "请填写餐厅名称", AllowEmpty = false)]
        public string Name { get; set; }
        /// <summary>
        /// 页面序号
        /// </summary>
        [PageNoValid]
        public int PageNo { get; set; }
        /// <summary>
        /// 搜索类型
        /// </summary>
        [limitString(ErrorMessage = "请填写搜索类型", AllowEmpty = false, Limit = "Restaurant|Recipe|All")]
        public string SearchType { get; set; }
    }
}