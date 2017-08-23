using System;
using Common.Extend;
using DbOpertion.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace WebApi_Health.Models.Response
{
    /// <summary>
    /// 获得文章Item的应答
    /// </summary>
    public class GetArticleItemResponse : GetArticleListResponse
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();
        /// <summary>
        /// 获得文章Item的应答构造函数
        /// </summary>
        public GetArticleItemResponse()
        {

        }
        /// <summary>
        /// 获得文章Item的应答构造函数
        /// </summary>
        public GetArticleItemResponse(Article article, List<Tag> List_Tag, List<CustomerLike> List_CustomerLike, List<Tag_Relation> List_Tag_Relation)
        {
            //文章id
            ArticleId = article.id;
            //标题
            title = article.title;
            //图片
            if (!article.thumbnail.IsNullOrEmpty())
            {
                TitleImage = ImageUrl + article.thumbnail;
            }
            else
            {
                TitleImage = " ";
            }
            //文章地址
            url = ImageUrl + article.url;
            //标签分离(算法未确定)
            if (!article.tags.IsNullOrEmpty())
            {
                List_Tag_Relation = List_Tag_Relation.Where(p => p.relationId == article.id).ToList();
                tags = new ArrayList();
                foreach (var item in List_Tag_Relation)
                {
                    var tag_item = List_Tag.Where(p => p.id == item.tagId).FirstOrDefault();
                    if (tag_item != null)
                        tags.Add(tag_item.name);
                }
            }

            //查看数
            cilckCount = article.cilckCount;
            //喜欢数
            loveCount = article.loveCount;
            //最后修改时间
            aTime = article.aTime;
            //是否点赞
            PointPraise = List_CustomerLike.Where(p => p.lid == ArticleId).FirstOrDefault() == null ? false : true;
        }
    }
}