using Common;
using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi_Health.BLL.Function;
using System.Configuration;
using Common.Enum;

namespace WebApi_Health.BLL.Cache
{
    /// <summary>
    /// 文章模型缓存
    /// </summary>
    public partial class CacheForModel_Article : SingleTon<CacheForModel_Article>
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        private int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public List<Article> GetArticleList()
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Article>>("ListArticle");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Article model = new Article();
                ListModel = ArticleOper.Instance.Select(model);
                CacheHelper.Instance.SetCache("ListArticle", ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 获取文章列表分页
        /// </summary>
        /// <returns></returns>
        public List<Article> GetArticleListByPage(string OrderBy, int PageNo)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Article>>("ListArticle");
            if (ListModel == null)
            {
                Article model = new Article();
                model.OrderBy = OrderBy;
                ListModel = ArticleOper.Instance.SelectByPage(model, PageSize, PageNo);
            }
            else
            {
                if (OrderBy.ToLower() == "atime")
                {
                    ListModel = ListModel.OrderBy(p => p.aTime).ToList();
                }
                else if (OrderBy.ToLower() == "cilckcount")
                {
                    ListModel = ListModel.OrderBy(p => p.cilckCount).ToList();
                }
            }
            ListModel = Paging.Instance.PageData<Article>(ListModel, PageSize, PageNo);
            return ListModel;
        }

        /// <summary>
        /// 根据名称获取文章列表
        /// </summary>
        public List<Article> GetArticleListByName(string name)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Article>>("ListArticle");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Article model = new Article();
                ListModel = ArticleOper.Instance.SelectVagueByArticleName(name);
                CacheHelper.Instance.SetCache("ListArticle", ListModel, outTime);
            }
            return ListModel;
        }

        /// <summary>
        /// 根据ID获取文章列表
        /// </summary>
        /// <returns></returns>
        public Article GetArticleListById(int ArticleId)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Article>>("ListArticle");
            if (ListModel == null)
            {
                int outTime = CacheHelper.Instance.CacheOutTime;
                Article model = new Article();
                model.id = ArticleId;
                model = ArticleOper.Instance.Select(model).FirstOrDefault();
                return model;
            }
            else
            {
                return ListModel.Where(p => p.id == ArticleId).FirstOrDefault();
            }
        }

        /// <summary>
        /// 文章查看
        /// </summary>
        /// <returns></returns>
        public Article ArticleCilckCount(int ArticleId)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Article>>("ListArticle");
            Article model;
            if (ListModel == null)
            {
                model = new Article();
                model.id = ArticleId;
                model = ArticleOper.Instance.Select(model).FirstOrDefault();
            }
            else
            {
                model = ListModel.Where(p => p.id == ArticleId).FirstOrDefault();
            }
            if (model != null)
            {
                model.cilckCount++;
                if (ArticleOper.Instance.Update(model))
                {
                    CacheHelper.Instance.RemoveCache("ListArticle");
                    GetArticleList();
                    return model;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 文章点赞
        /// </summary>
        /// <returns></returns>
        public Article ArticleLoveCount(int ArticleId, Enum_Opertion opertion)
        {
            var ListModel = CacheHelper.Instance.GetCache<List<Article>>("ListArticle");
            Article model;
            if (ListModel == null)
            {
                model = new Article();
                model.id = ArticleId;
                model = ArticleOper.Instance.Select(model).FirstOrDefault();
            }
            else
            {
                model = ListModel.Where(p => p.id == ArticleId).FirstOrDefault();
            }
            if (model != null)
            {
                if (opertion == Enum_Opertion.Delete)
                {
                    model.loveCount--;
                }
                else if (opertion == Enum_Opertion.Insert)
                {
                    model.loveCount++;
                }
                if (ArticleOper.Instance.Update(model))
                {
                    CacheHelper.Instance.RemoveCache("ListArticle");
                    GetArticleList();
                    return model;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}