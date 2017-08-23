using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Response
{
    /// <summary>
    ///  获得分数列表Response
    /// </summary>
    public class GetScoreListResponse
    {
        /// <summary>
        ///  获得分数列表Response
        /// </summary>
        public GetScoreListResponse()
        {

        }
        /// <summary>
        /// 获得分数列表Response
        /// </summary>
        /// <param name="score"></param>
        public GetScoreListResponse(Score score)
        {
            this.ScoreId = score.ScoreId;
            this.Time = score.ScoreDate;
            this.Content = score.ScoreContent;
            this.ScoreType = score.ScoreType;
            this.ScoreNum = score.ScoreNum;
        }
        /// <summary>
        /// 分数记录ID
        /// </summary>
        public int ScoreId { get; set; }

        /// <summary>
        /// 分数类型
        /// </summary>
        public string ScoreType { get; set; }

        /// <summary>
        /// 分数类型
        /// </summary>
        public double ScoreNum{ get; set; }

        /// <summary>
        /// 分数时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 分数内容
        /// </summary>
        public string Content { get; set; }
    }
}