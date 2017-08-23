using System;
using Common.Extend;
using DbOpertion.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace WebApi_Health.Models.Response
{
    public class GetArticleListResponse
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();
        public GetArticleListResponse()
        {

        }
        public GetArticleListResponse(Article article, List<Tag> List_Tag, Customer User, List<CustomerLike> List_CustomerLike, List<Tag_Relation> List_Tag_Relation)
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
            //标签分离(算法未确定)
            if (!article.tags.IsNullOrEmpty())
            {
                tags = new ArrayList();
                List_Tag_Relation = List_Tag_Relation.Where(p => p.relationId == article.id).ToList();
                List<Models.Tag.TagModel> Listscore;
                List<Tag> ListTags = new List<Tag>();
                foreach (var tag_relation in List_Tag_Relation)
                {
                    var model = List_Tag.Find(p => p.id == tag_relation.tagId);
                    if (model != null)
                    {
                        ListTags.Add(model);
                        tags.Add(model.name);
                    }
                }
                #region 计算此菜适合体质
                Listscore = WebApi_Health.BLL.Function.StringHandle.Instance.ConstitutionCalulate(ListTags);
                foreach (var item in Listscore)
                {
                    if (item.name == User.constitution)
                    {
                        ConstitutionPercentage = (int)item.score;
                    }
                }
                #endregion
            }

            //查看数
            cilckCount = article.cilckCount;
            //喜欢数
            loveCount = article.loveCount;
            //最后修改时间
            aTime = article.aTime == null ? new DateTime() : article.aTime;
            //文章路径
            url = ImageUrl + article.url;
            //是否点赞
            PointPraise = List_CustomerLike.Where(p => p.lid == ArticleId).FirstOrDefault() == null ? false : true;
        }

        /// <summary>
        /// 文章id
        /// </summary>
        public Int32 ArticleId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public String content { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public String TitleImage { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public ArrayList tags { get; set; }
        /// <summary>
        /// 体质百分比
        /// </summary>
        public int ConstitutionPercentage { get; set; }
        /// <summary>
        /// 查看数
        /// </summary>
        public Int32? cilckCount { get; set; }
        /// <summary>
        /// 喜欢数
        /// </summary>
        public Int32? loveCount { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? aTime { get; set; }
        /// <summary>
        /// 文章地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 当前用户用没有收藏
        /// </summary>
        public bool PointPraise { get; set; }
    }
}