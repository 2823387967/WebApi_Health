using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.Models.Response
{
    /// <summary>
    /// 获取问题列表Response
    /// </summary>
    public class GetQuestionListResponse
    {
        /// <summary>
        /// 专业版问题
        /// </summary>
        /// <param name="Question">问题</param>
        public GetQuestionListResponse(Questionnaire Question)
        {
            //问题Id
            QuestionId = Question.id;
            //问题内容
            QuestionContent = Question.Content;
            //选项
            Options = new List<QuestionOption>();
            Options.Add(new QuestionOption { OptionId = 0, OptionContent = "完全不" });
            Options.Add(new QuestionOption { OptionId = 1, OptionContent = "有一点" });
            Options.Add(new QuestionOption { OptionId = 2, OptionContent = "非常" });
        }
        /// <summary>
        /// 简易版问题
        /// </summary>
        /// <param name="Question">问题</param>
        /// <param name="QuestionOptions">选项</param>
        public GetQuestionListResponse(Questionnaire Question, List<Questionnaire> QuestionOptions)
        {
            //问题Id
            QuestionId = Question.id;
            //问题内容
            QuestionContent = Question.Content;
            //选项
            QuestionOptions = QuestionOptions.Where(p => p.QuesOrOp == QuesOrOpVariable.Option && p.RelationId == Question.id).ToList();
            Options = new List<QuestionOption>();
            foreach (var item in QuestionOptions)
            {
                QuestionOption option = new QuestionOption { OptionId = item.id, OptionContent = item.Content };
                Options.Add(option);
            }
        }
        /// <summary>
        /// 问题Id
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 问题内容
        /// </summary>
        public string QuestionContent { get; set; }
        /// <summary>
        /// 选项列表
        /// </summary>
        public List<QuestionOption> Options { get; set; }
    }
    /// <summary>
    /// 问题选项
    /// </summary>
    public class QuestionOption
    {
        /// <summary>
        /// 选项Id
        /// </summary>
        public int OptionId { get; set; }
        /// <summary>
        /// 选项内容
        /// </summary>
        public string OptionContent { get; set; }
    }
}