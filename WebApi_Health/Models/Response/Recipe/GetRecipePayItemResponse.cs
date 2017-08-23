using System;
using Common.Extend;
using DbOpertion.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace WebApi_Health.Models.Response
{
    public class GetRecipePayItemResponse
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();
        public GetRecipePayItemResponse()
        {

        }

        public GetRecipePayItemResponse(Recipe recipe, List<Tag> List_Tag, Customer UserModel, List<Tag_Relation> List_Tag_Relation)
        {
            //食谱Id
            id = recipe.id;
            //食谱名称
            name = recipe.name;
            //餐厅Id
            RestaurantId = recipe.restaurantId == null ? 0 : recipe.restaurantId.Value;
            //标签与体质与体质百分比
            if (!recipe.tags.IsNullOrEmpty())
            {
                List_Tag_Relation = List_Tag_Relation.Where(p => p.relationId == recipe.id).ToList();
                List<Models.Tag.TagModel> Listscore;
                List<Tag> ListTags = new List<Tag>();
                Tags = new ArrayList();
                foreach (var ArrayItem in List_Tag_Relation)
                {
                    var model = List_Tag.Find(p => p.id == ArrayItem.tagId);
                    if (model != null)
                    {
                        ListTags.Add(model);
                        Tags.Add(model.name);
                    }
                }
                #region 计算此菜适合体质
                Listscore = WebApi_Health.BLL.Function.StringHandle.Instance.ConstitutionCalulate(ListTags);
                foreach (var item in Listscore)
                {
                    if (item.name == UserModel.constitution)
                    {
                        ConstitutionPercentage = (int)item.score;
                    }
                }
                Constitution = new ArrayList();
                Constitution.Add(Listscore[0].name);
                Constitution.Add(Listscore[1].name);
                Constitution.Add(Listscore[2].name);
                #endregion
            }
            //图片
            if (!recipe.images.IsNullOrEmpty())
            {
                images = new ArrayList();
                foreach (var item in recipe.images.Split('|'))
                {
                    images.Add(ImageUrl + item);
                }
            }
            //价格
            double? Price = recipe.price.ParseDouble();
            price = String.Format("{0:F}", Price);
            //销售量
            sales = recipe.sales.Value;
        }

        /// <summary>
        /// 食谱Id
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 食谱名称
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// 体质百分比
        /// </summary>
        public int ConstitutionPercentage { get; set; }
        /// <summary>
        /// 体质
        /// </summary>
        public ArrayList Constitution { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public ArrayList Tags { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public ArrayList images { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 销售量
        /// </summary>
        public int sales { get; set; }
        /// <summary>
        /// 餐厅Id
        /// </summary>
        public int RestaurantId { get; set; }
    }
}