using Common;
using Common.Enum;
using Common.Extend;
using Common.Result;
using DbOpertion.Models;
using System.Collections.Generic;
using System.Linq;
using WebApi_Health.BLL.Cache;
using WebApi_Health.BLL.Enum;
using WebApi_Health.BLL.Function;
using WebApi_Health.Controllers;
using WebApi_Health.Models.Request;
using WebApi_Health.Models.Response;
using WebApi_Health.Models.Variable;

namespace WebApi_Health.BLL.ControllerBiz
{
    /// <summary>
    /// 文章接口业务逻辑层
    /// </summary>
    public class ArticleBiz : SingleTon<ArticleBiz>
    {
        private static object ObjLock = new object();

        /// <summary>
        /// 根据文章Id获取文章
        /// </summary>
        public ResultJson<GetArticleItemResponse> GetArticleItemInfo(GetInfoByOtherIdRequest request)
        {
            ResultJson<GetArticleItemResponse> result = new ResultJson<GetArticleItemResponse>();
            var Article_Like_List = CacheForModelCustomerLike.Instance.GetCustomLikeArticleList(request.UserId);
            List<Tag> List_Tag = CacheForModelTag.Instance.GetTagList();
            var list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationList();
            list_tag_relation = list_tag_relation.Where(p => Enum_SearchType.Article.Enum_GetString().EqualString(p.typename)).ToList();
            Article Item_Article;
            lock (ObjLock)
            {
                Item_Article = CacheForModel_Article.Instance.ArticleCilckCount(request.id);
                if (Item_Article == null)
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                    return result;
                }
            }
            List<GetArticleItemResponse> ArticleResponse = new List<GetArticleItemResponse>();
            GetArticleItemResponse response = new GetArticleItemResponse(Item_Article, List_Tag, Article_Like_List, list_tag_relation);
            ArticleResponse.Add(response);
            if (ArticleResponse.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.ListData = ArticleResponse;
            }
            return result;
        }

        /// <summary>
        /// 搜索文章列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        public ResultJson<GetArticleListResponse, GetUserSuitResponse> SearchArticleList(int UserId, int PageNo)
        {
            ResultJson<GetArticleListResponse, GetUserSuitResponse> result = new ResultJson<GetArticleListResponse, GetUserSuitResponse>();
            List<Article> List_Article = CacheForModel_Article.Instance.GetArticleList();
            List_Article = List_Article == null ? new List<Article>() : List_Article;
            List<Tag> List_Tag = CacheForModelTag.Instance.GetTagList();
            var list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationList();
            list_tag_relation = list_tag_relation.Where(p => Enum_SearchType.Article.Enum_GetString().EqualString(p.typename)).ToList();
            Customer User = CacheForModelUser.Instance.GetUserInfo(UserId);
            var Article_Like_List = CacheForModelCustomerLike.Instance.GetCustomLikeArticleList(UserId);
            List<GetArticleListResponse> ArticleResponse = new List<GetArticleListResponse>();
            foreach (var item in List_Article)
            {
                GetArticleListResponse response = new GetArticleListResponse(item, List_Tag, User, Article_Like_List, list_tag_relation);
                ArticleResponse.Add(response);
            }
            ArticleResponse = Paging.Instance.PageData<GetArticleListResponse>(ArticleResponse, PageNo);
            if (ArticleResponse.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.ListData = ArticleResponse;
                GetUserSuitResponse response = new GetUserSuitResponse(User);
                result.ListData2.Add(response);
            }
            return result;
        }

        /// <summary>
        /// 搜索文章列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        public ResultJson<GetArticleListResponse, GetUserSuitResponse> SearchArticleList(int UserId, int PageNo, string Name)
        {
            ResultJson<GetArticleListResponse, GetUserSuitResponse> result = new ResultJson<GetArticleListResponse, GetUserSuitResponse>();
            List<Article> List_Article = CacheForModel_Article.Instance.GetArticleListByName(Name);
            List_Article = List_Article == null ? new List<Article>() : List_Article;
            List<Tag> List_Tag = CacheForModelTag.Instance.GetTagList();
            var list_tag_relation = CacheForModel_TagRelation.Instance.GetTagRelationList();
            list_tag_relation = list_tag_relation.Where(p => Enum_SearchType.Article.Enum_GetString().EqualString(p.typename)).ToList();
            Customer User = CacheForModelUser.Instance.GetUserInfo(UserId);
            var Article_Like_List = CacheForModelCustomerLike.Instance.GetCustomLikeArticleList(UserId);
            List<GetArticleListResponse> ArticleResponse = new List<GetArticleListResponse>();
            foreach (var item in List_Article)
            {
                GetArticleListResponse response = new GetArticleListResponse(item, List_Tag, User, Article_Like_List, list_tag_relation);
                ArticleResponse.Add(response);
            }
            ArticleResponse = Paging.Instance.PageData<GetArticleListResponse>(ArticleResponse, PageNo);
            if (ArticleResponse.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.ListData = ArticleResponse;
                GetUserSuitResponse response = new GetUserSuitResponse(User);
                result.ListData2.Add(response);
            }
            return result;
        }

        /// <summary>
        /// 文章查看
        /// </summary>
        public ResultJson ArticleClick(GetInfoByOtherIdRequest request)
        {
            ResultJson result = new ResultJson();
            Article Item_Article;
            lock (ObjLock)
            {
                Item_Article = CacheForModel_Article.Instance.ArticleCilckCount(request.id);
                if (Item_Article == null)
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                    return result;
                }
                else
                {
                    result.HttpCode = 200;
                    result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                }
            }
            return result;
        }

        /// <summary>
        /// 文章点赞
        /// </summary>
        public ResultJson ArticlePointPraise(GetArticlePointPraiseRequest request)
        {
            ResultJson result = new ResultJson();
            RestaurantController restaurantContro = new RestaurantController();
            CustomLikeRequest customLike_Request = new CustomLikeRequest
            {
                Type_Like = UserLikeTypeVariable.ArticleLike,
                UserId = request.UserId,
                Token = request.Token,
                Opertion = request.Opertion,
                OtherId = request.OtherId
            };
            var customLike_Result = restaurantContro.CustomerLikeOrNot(customLike_Request);
            if (customLike_Result.HttpCode != 200)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitMessage.Enum_GetString();
                return result;
            }
            var UpdateResult = false;
            lock (ObjLock)
            {
                Article Item_Article = CacheForModel_Article.Instance.GetArticleListById(request.OtherId);
                if (Item_Article == null)
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
                    return result;
                }
                if (request.Opertion.ToLower() == Enum_Opertion.Delete.Enum_GetString())
                {
                    Item_Article.loveCount--;
                    UpdateResult = DbOpertion.DBoperation.ArticleOper.Instance.Update(Item_Article);
                }
                else if (request.Opertion.ToLower() == Enum_Opertion.Insert.Enum_GetString())
                {
                    Item_Article.loveCount++;
                    UpdateResult = DbOpertion.DBoperation.ArticleOper.Instance.Update(Item_Article);
                }

            }
            if (!UpdateResult)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            return result;
        }
    }
}