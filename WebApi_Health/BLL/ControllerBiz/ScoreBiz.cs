using Common;
using DbOpertion.DBoperation;
using System.Linq;
using WebApi_Health.BLL.Cache;
using Common.Result;
using Common.Enum;
using WebApi_Health.Models.Request;
using Common.Extend;
using WebApi_Health.BLL.Enum;
using WebApi_Health.Models.Models;
using WebApi_Health.BLL.Function;
using WebApi_Health.Models.Response;

namespace WebApi_Health.BLL.ControllerBiz
{
    /// <summary>
    /// Score业务逻辑层
    /// </summary>
    public class ScoreBiz : SingleTon<ScoreBiz>
    {
        object obj = new object();

        /// <summary>
        /// 获得未获取分数列表
        /// </summary>
        public ResultJsonModel<GetScoreResponse, GetScoreResponse, GetScoreResponse> GetClickScore(UserIDRequest request)
        {
            ResultJsonModel<GetScoreResponse, GetScoreResponse, GetScoreResponse> result = new ResultJsonModel<GetScoreResponse, GetScoreResponse, GetScoreResponse>();
            var scoreList = CacheForModelScore.Instance.ScoreList(request.UserId);
            scoreList = scoreList.Where(p => p.ScoreClick == false).ToList();
            var scoreEatList = scoreList.Where(p => p.ScoreType == Enum_ScoreType.Eat.Enum_GetString()).ToList();
            var scoreSleepList = scoreList.Where(p => p.ScoreType == Enum_ScoreType.Sleep.Enum_GetString()).ToList();
            var scoreSportList = scoreList.Where(p => p.ScoreType == Enum_ScoreType.Sport.Enum_GetString()).ToList();
            result.HttpCode = 200;
            result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            if (scoreEatList.Count != 0)
            {
                GetScoreResponse scoreResponse = new GetScoreResponse(scoreEatList, Enum_ScoreType.Eat);
                result.Model1 = scoreResponse;
            }
            if (scoreSleepList.Count != 0)
            {
                GetScoreResponse scoreResponse = new GetScoreResponse(scoreSleepList, Enum_ScoreType.Sleep);
                result.Model2 = scoreResponse;
            }
            if (scoreSportList.Count != 0)
            {
                GetScoreResponse scoreResponse = new GetScoreResponse(scoreSportList, Enum_ScoreType.Sport);
                result.Model3 = scoreResponse;
            }
            return result;
        }

        /// <summary>
        /// 获得分数列表
        /// </summary>
        public ResultJson<GetScoreListResponse> GetScoreList(GetInfoByUserIdPageRequest request)
        {
            ResultJson<GetScoreListResponse> result = new ResultJson<GetScoreListResponse>();
            var scoreList = CacheForModelScore.Instance.ScoreList(request.UserId);
            scoreList = scoreList.Where(p => p.ScoreClick == true).OrderByDescending(p => p.ScoreId).OrderByDescending(p => p.ScoreDate).ToList();
            scoreList = Paging.Instance.PageData(scoreList, request.PageNo);
            foreach (var item in scoreList)
            {
                GetScoreListResponse response = new GetScoreListResponse(item);
                result.ListData.Add(response);
            }
            if (result.ListData.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }


            return result;
        }

        /// <summary>
        /// 增加积分记录
        /// </summary>
        public ResultJson AddScoreRecord(ScoreRequest request)
        {
            ResultJson result = new ResultJson();
            bool flag = false;
            if (request.ScoreType.EqualString(Enum_ScoreType.Sport.Enum_GetString()))
            {
                if (CacheForModelScore.Instance.InsertSportScore(request.UserId))
                    flag = true;
            }
            else if (request.ScoreType.EqualString(Enum_ScoreType.Sleep.Enum_GetString()))
            {
                if (CacheForModelScore.Instance.InsertSleepScore(request.UserId))
                    flag = true;
            }
            if (flag)
            {
                result.HttpCode = 200;
                result.Message = "用户积分增加";
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 积分点击
        /// </summary>
        public ResultJson ClickScore(ClickScoreRequest request)
        {
            ResultJson result = new ResultJson();
            var Ids = request.ScoreIds.Split(',').GroupBy(p => p).Select(p => p.Key).ToList();
            lock (obj)
            {
                if (CacheForModelScore.Instance.UpdateScoreByIds(request.UserId, request.ScoreIds))
                {
                    var scoreList = CacheForModelScore.Instance.ScoreList(request.UserId);
                    var a = scoreList.Where(p => Ids.Contains(p.ScoreId.ToString())).ToList();
                    var Score_Add = scoreList.Where(p => Ids.Contains(p.ScoreId.ToString())).ToList().Sum(p => p.ScoreNum);
                    var UserInfo = CacheForModelUser.Instance.GetUserInfo(request.UserId);
                    UserInfo.UserScore = UserInfo.UserScore == null ? Score_Add : Score_Add + UserInfo.UserScore;
                    if (CustomerOper.Instance.Update(UserInfo))
                    {
                        result.HttpCode = 200;
                        result.Message = "用户积分增加";
                    }
                    else
                    {
                        result.HttpCode = 300;
                        result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
                    }

                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
                }
            }
            return result;
        }

    }
}