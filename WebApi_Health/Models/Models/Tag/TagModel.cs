using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Models.Tag
{
    public class TagModel
    {
        /// <summary>
        /// 体质名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 体质分数
        /// <summary>
        public double score { get; set; }
    }
}