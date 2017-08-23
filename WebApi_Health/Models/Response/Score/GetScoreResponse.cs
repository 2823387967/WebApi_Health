using Common.Extend;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi_Health.BLL.Enum;

namespace WebApi_Health.Models.Models
{
    public class GetScoreResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public GetScoreResponse()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public GetScoreResponse(List<Score> List_Score, Enum_ScoreType scoreType)
        {
            this.Score = List_Score.Sum(p => p.ScoreNum);
            this.ScoreType = scoreType.Enum_GetString();
            this.ListId = List_Score.Select(p => p.ScoreId).ToList().ParseString();
        }
        /// <summary>
        /// 用户获得分数
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Id列表
        /// </summary>
        public string ListId { get; set; }
        /// <summary>
        /// 分数类型
        /// </summary>
        public string ScoreType { get; set; }
        /// <summary>
        /// 分数内容
        /// </summary>
        public string ScoreContent { get; set; }
    }
}