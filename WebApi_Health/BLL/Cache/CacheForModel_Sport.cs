using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi_Health.BLL.Function;
using System.Configuration;
using Common.Extend;

namespace WebApi_Health.BLL.Cache
{
    public partial class CacheForModelSport : SingleTon<CacheForModelSport>
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
        /// <summary>
        /// 获取用户运动列表
        /// </summary>
        /// <returns></returns>
        public List<Sport> GetUserSportList(string UserId)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Sport>>("ListSport_" + UserId);
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.SportCacheOutTime;
                Sport model = new Sport();
                model.cid = UserId.ParseInt().GetValueOrDefault();
                ListModel = SportOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListSport_" + UserId, ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 获取用户当日数据
        /// </summary>
        /// <returns></returns>
        public Sport GetUserSportDate(string UserId, DateTime dateTime)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Sport>>("ListSport_" + UserId);
            if (ListModel == null)
            {
                Sport model = new Sport();
                model.cid = UserId.ParseInt().GetValueOrDefault();
                model.sDate = dateTime;
                ListModel = SportOper.Instance.Select(model);
            }
            else
            {
                ListModel = ListModel.Where(p => p.sDate == dateTime).ToList();
            }
            return ListModel.Count > 0 ? ListModel[0] : null;
        }

        /// <summary>
        /// 插入用户运动数据
        /// </summary>
        /// <returns></returns>
        public bool InsertUserSport(Sport sport)
        {
            Sport model = new Sport();
            model.cid = sport.cid;
            model.sDate = DateTime.Now.ToShortDateString().ToString().ParseDateTime().Value;
            var Date_Model = GetUserSportDate(sport.cid.ToString(), model.sDate);
            bool result;
            if (Date_Model == null)
            {
                result = SportOper.Instance.Insert(model);
            }
            else
            {
                Date_Model.steps = sport.steps;
                //Date_Model.sTime += sport.sTime;
                //Date_Model.distance += sport.distance;
                result = SportOper.Instance.Update(Date_Model);
            }
            CacheHelper.Instance.RemoveCache("ListSport_" + sport.cid.ToString());
            GetUserSportList(sport.cid.ToString());
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}