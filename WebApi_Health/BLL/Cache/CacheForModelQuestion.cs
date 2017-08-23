using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi_Health.BLL.Function;
using System.Configuration;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelQuestion : SingleTon<CacheForModelQuestion>
    {
        /// <summary>
        /// 获取简易版列表
        /// </summary>
        /// <returns></returns>
        public List<Questionnaire> GetQuestionExpressList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Questionnaire>>("ListQuestion");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Questionnaire model = new Questionnaire();
                model.category = CategoryVariable.Express;
                ListModel = QuestionnaireOper.Instance.Select(model);
            }
            else
            {
                ListModel = ListModel.Where(p => p.category == CategoryVariable.Express).ToList();
            }
            return ListModel;
        }

        /// <summary>
        /// 获取专业版列表
        /// </summary>
        /// <returns></returns>
        public List<Questionnaire> GetQuestionProfessionList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Questionnaire>>("ListQuestion");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Questionnaire model = new Questionnaire();
                model.category = CategoryVariable.Profession;
                ListModel = QuestionnaireOper.Instance.Select(model);
            }
            else
            {
                ListModel = ListModel.Where(p => p.category == CategoryVariable.Profession).ToList();
            }
            return ListModel;
        }
    }
}