using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class GetRestaurantInfoByIdRequest : UserIDRequest
    {
        /// <summary>
        /// 各种类的ID
        /// </summary>
        [Required(ErrorMessage = "请填写Id", AllowEmptyStrings = false)]
        public int id { get; set; }
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
    }
}