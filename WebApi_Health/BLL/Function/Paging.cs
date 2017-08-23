using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApi_Health.BLL.Function
{
    public class Paging : SingleTon<Paging>
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        private int PageSizePaging = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
        /// <summary>
        /// 页面分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据</param>
        /// <param name="PageSize">页面大小</param>
        /// <param name="PageNo">页码</param>
        /// <returns></returns>
        public List<T> PageData<T>(List<T> list, int PageSize, int PageNo) where T : class, new()
        {
            List<T> pageList = new List<T>();
            for (int i = 0; i < PageSize; i++)
            {
                if (list.Count >= (PageNo - 1) * PageSize + i + 1)
                {
                    pageList.Add(list[(PageNo - 1) * PageSize + i]);
                }
            }
            return pageList;
        }
        /// <summary>
        /// 页面分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据</param>
        /// <param name="PageNo">页码</param>
        /// <returns></returns>
        public List<T> PageData<T>(List<T> list, int PageNo) where T : class, new()
        {
            List<T> pageList = new List<T>();
            for (int i = 0; i < PageSizePaging; i++)
            {
                if (list.Count >= (PageNo - 1) * PageSizePaging + i + 1)
                {
                    pageList.Add(list[(PageNo - 1) * PageSizePaging + i]);
                }
            }
            return pageList;
        }
    }
}