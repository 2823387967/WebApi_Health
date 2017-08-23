using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi_Health.BLL.Function;
using System.Configuration;
using WebApi_Health.BLL.Enum;
using Common.Extend;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelScore : SingleTon<CacheForModelScore>
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());

        private object objLock = new object();
        /// <summary>
        /// 分数列表
        /// </summary>
        /// <returns></returns>
        public List<Score> ScoreList(int UserId)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Score>>("ScoreList_" + UserId);
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Score model = new Score();
                model.UserId = UserId;
                ListModel = ScoreOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ScoreList_" + UserId, ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 根据列表更新分数
        /// </summary>
        /// <returns></returns>
        public bool UpdateScoreByIds(int UserId, string Ids)
        {
            if (ScoreOper.Instance.UpdateScoreClickByIds(Ids.Split(',').ToList()).GetValueOrDefault())
            {
                CacheHelper.Instance.SetCache("ScoreList_" + UserId, null);
                ScoreList(UserId);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 插入运动分数
        /// </summary>
        /// <returns></returns>
        public bool InsertSportScore(int UserId)
        {
            Score model = new Score();
            model.UserId = UserId;
            model.ScoreType = Enum_ScoreType.Sport.Enum_GetString();
            var ListModel = CacheHelper.Instance.GetCache<List<Score>>("ScoreList");
            if (ListModel == null)
            {
                ListModel = ScoreOper.Instance.Select(model);
                if (ListModel == null)
                {
                    ListModel = new List<Score>();
                }
            }
            else
            {
                ListModel = ListModel.Where(p => p.UserId == UserId && p.ScoreType == Enum_ScoreType.Sport.Enum_GetString()).ToList();
            }
            var ListToday = ListModel.Where(p => p.ScoreDate == DateTime.Now.ToDate()).ToList();
            var ListThisWeek = ListModel.Where(p => p.ScoreDate.AddDays(-(int)p.ScoreDate.DayOfWeek) == DateTime.Now.ToDate().AddDays(-(int)DateTime.Now.DayOfWeek)).ToList();
            if (ListToday.Count == 0 && ListThisWeek.Count < 4)
            {
                model.ScoreDate = DateTime.Now.ToDate();
                model.ScoreNum = 1;
                model.ScoreClick = false;
                model.ScoreContent = "运动3000步";
                if (ScoreOper.Instance.Insert(model))
                {
                    CacheHelper.Instance.SetCache("ScoreList_" + UserId, null);
                    ScoreList(UserId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 插入睡眠分数
        /// </summary>
        /// <returns></returns>
        public bool InsertSleepScore(int UserId)
        {
            Score model = new Score();
            model.UserId = UserId;
            model.ScoreType = Enum_ScoreType.Sleep.Enum_GetString();
            var ListModel = CacheHelper.Instance.GetCache<List<Score>>("ScoreList");
            if (ListModel == null)
            {
                ListModel = ScoreOper.Instance.Select(model);
                if (ListModel == null)
                {
                    ListModel = new List<Score>();
                }
            }
            else
            {
                ListModel = ListModel.Where(p => p.UserId == UserId && p.ScoreType == Enum_ScoreType.Sleep.Enum_GetString()).ToList();
            }
            var ListToday = ListModel.Where(p => p.ScoreDate == DateTime.Now.ToDate()).ToList();
            if (ListToday.Count == 0)
            {
                model.ScoreDate = DateTime.Now.ToDate();
                DateTime date = DateTime.Now;
                if (date.Hour <= 23 && date.Hour >= 12)
                {
                    model.ScoreNum = 0.5;
                    model.ScoreContent = "在23点之前睡觉";
                }
                else if (date.Hour > 23 && date.Minute <= 30)
                {
                    model.ScoreNum = 0.3;
                    model.ScoreContent = "在23点30分之前睡觉";
                }
                else
                {
                    return false;
                }
                model.ScoreClick = false;
                if (ScoreOper.Instance.Insert(model))
                {
                    CacheHelper.Instance.SetCache("ScoreList_" + UserId, null);
                    ScoreList(UserId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 插入餐饮分数
        /// </summary>
        /// <returns></returns>
        public bool InsertEatScore(int UserId, string recipeName)
        {
            Score model = new Score();
            var ListModel = CacheHelper.Instance.GetCache<List<Score>>("ScoreList");
            if (ListModel == null)
            {
                model.UserId = UserId;
                model.ScoreType = Enum_ScoreType.Eat.Enum_GetString();
                ListModel = ScoreOper.Instance.Select(model);
                if (ListModel == null)
                {
                    ListModel = new List<Score>();
                }
            }
            else
            {
                ListModel = ListModel.Where(p => p.UserId == UserId && p.ScoreType == Enum_ScoreType.Eat.Enum_GetString()).ToList();
            }
            var ListToday = ListModel.Where(p => p.ScoreDate == DateTime.Now.ToDate()).ToList();
            var ListThisWeek = ListModel.Where(p => p.ScoreDate.AddDays(-(int)p.ScoreDate.DayOfWeek) == DateTime.Now.ToDate().AddDays(-(int)DateTime.Now.DayOfWeek)).ToList();
            if (ListToday.Count < 2 && ListThisWeek.Count < 10)
            {
                model.ScoreDate = DateTime.Now.ToDate();
                model.ScoreNum = 1;
                model.ScoreClick = false;
                model.ScoreContent = "饮食:" + recipeName;
                if (ScoreOper.Instance.Insert(model))
                {
                    CacheHelper.Instance.SetCache("ScoreList_" + UserId, null);
                    ScoreList(UserId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}