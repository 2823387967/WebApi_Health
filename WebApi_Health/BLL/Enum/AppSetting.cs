using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApi_Health.BLL.Enum
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class AppSetting
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public static string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();

        /// <summary>
        /// 搜索范围
        /// </summary>
        public static int SeachRange = int.Parse(ConfigurationManager.AppSettings["SeachRange"].ToString());

        /// <summary>
        /// 页面大小
        /// </summary>
        public static int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
    }
}