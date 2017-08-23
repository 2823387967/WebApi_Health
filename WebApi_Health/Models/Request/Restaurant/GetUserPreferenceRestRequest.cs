using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Request
{
    public class GetUserPreferenceRestRequest : GetInfoByUserIdPageRequest
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

    }
}