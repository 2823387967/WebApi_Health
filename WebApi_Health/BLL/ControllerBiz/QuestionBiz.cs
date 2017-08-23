using Common;
using Common.Enum;
using Common.Extend;
using Common.Result;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using WebApi_Health.BLL.Cache;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Response;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.BLL.ControllerBiz
{
    public class QuestionBiz : SingleTon<QuestionBiz>
    {
        /// <summary>
        /// 简易版问题列表
        /// </summary>
        public ResultJson<GetQuestionListResponse> GetQuestionExpressList()
        {
            ResultJson<GetQuestionListResponse> result = new ResultJson<GetQuestionListResponse>();
            var questionnaire = CacheForModelQuestion.Instance.GetQuestionExpressList();
            var questions = questionnaire.Where(p => p.QuesOrOp == QuesOrOpVariable.Question).ToList();
            foreach (var item in questions)
            {
                GetQuestionListResponse response = new GetQuestionListResponse(item, questionnaire);
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
            }
            return result;
        }
        /// <summary>
        /// 专业版问题列表
        /// </summary>
        public ResultJson<GetQuestionListResponse> GetQuestionProfessionList(GetInfoByOtherIdRequest request)
        {
            ResultJson<GetQuestionListResponse> result = new ResultJson<GetQuestionListResponse>();
            var questionnaire = CacheForModelQuestion.Instance.GetQuestionProfessionList();
            var user = CacheForModelUser.Instance.GetUserInfo(request.id);
            var questions = questionnaire.Where(p => p.QuesOrOp == QuesOrOpVariable.Question && p.Constitution == user.constitution).ToList();
            foreach (var item in questions)
            {
                GetQuestionListResponse response = new GetQuestionListResponse(item);
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
            }
            return result;
        }
        /// <summary>
        /// 专业版问题提交结果
        /// </summary>
        public ResultJson GetSubmitQuestion(SubmitQusttionRequest request)
        {
            ResultJson result = new ResultJson();
            var user = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            if (user == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
            }
            int score = 0;
            var ArrayAnswer = request.Answer.Split('|');
            var ListAnswer = ArrayAnswer.Distinct().ToList();
            foreach (var item in ListAnswer)
            {
                if (!item.IsNullOrEmpty())
                {
                    var array = item.Split(',');
                    score += array[1].ParseInt() == null ? 0 : array[1].ParseInt().Value;
                }
            }

            user.score = score;
            DbOpertion.DBoperation.CustomerOper.Instance.Update(user);
            CacheForModelUser.Instance.SetUserInfo(request.UserId);
            result.HttpCode = 200;
            result.Message = score.ToString();
            return result;
        }
        /// <summary>
        /// 简易版问题提交结果
        /// </summary>
        public ResultJsonModel<string> GetSubmitExpressQuestion(SubmitQusttionRequest request)
        {
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            var ArrayAnswer = request.Answer.Split(',');
            var ListAnswer = ArrayAnswer.Distinct().ToList();
            var questionnaire = CacheForModelQuestion.Instance.GetQuestionExpressList();
            List<Questionnaire> List_Question = new List<Questionnaire>();
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var item in ListAnswer)
            {
                if (!item.IsNullOrEmpty())
                {
                    var model = questionnaire.Where(p => p.id.ToString() == item).FirstOrDefault();
                    if (model != null)
                    {
                        List_Question.Add(model);
                        var constitution = dic.Where(p => p.Key == model.Constitution).FirstOrDefault();

                        if (!dic.ContainsKey(model.Constitution))
                        {
                            dic.Add(model.Constitution, 1);
                        }
                        else
                        {
                            dic.Remove(model.Constitution);
                            dic.Add(model.Constitution, constitution.Value + 1);
                        }
                    }

                }
            }
            var user = CacheForModelUser.Instance.GetUserInfo(request.UserId);
            user.constitution = dic.OrderByDescending(p => p.Value).FirstOrDefault().Key;
            DbOpertion.DBoperation.CustomerOper.Instance.Update(user);
            CacheForModelUser.Instance.SetUserInfo(request.UserId);
            ConstitutionResult constitutionResult = new ConstitutionResult();
            constitutionResult.name = user.constitution;
            constitutionResult = DbOpertion.DBoperation.ConstitutionResultOper.Instance.Select(constitutionResult).FirstOrDefault();
            result.HttpCode = 200;
            result.Message = user.constitution;
            result.Model1 = constitutionResult.content;
            return result;
        }
    }
}