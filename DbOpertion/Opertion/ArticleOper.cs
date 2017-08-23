using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Common.Extend;
using Common;
using System.Collections.Generic;
using DbOpertion.Models;

namespace DbOpertion.DBoperation
{
    public partial class ArticleOper : SingleTon<ArticleOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="article"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Article article)
        {
            StringBuilder sql = new StringBuilder("insert into Article ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!article.title.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("title");
                    part2.Append("@title");
                    flag = false;
                }
                else
                {
                    part1.Append(",title");
                    part2.Append(",@title");
                }
                parm.Add("title", article.title);
            }
            if(!article.url.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("url");
                    part2.Append("@url");
                    flag = false;
                }
                else
                {
                    part1.Append(",url");
                    part2.Append(",@url");
                }
                parm.Add("url", article.url);
            }
            if(!article.content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("content");
                    part2.Append("@content");
                    flag = false;
                }
                else
                {
                    part1.Append(",content");
                    part2.Append(",@content");
                }
                parm.Add("content", article.content);
            }
            if(!article.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail");
                    part2.Append("@thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(",thumbnail");
                    part2.Append(",@thumbnail");
                }
                parm.Add("thumbnail", article.thumbnail);
            }
            if(!article.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags");
                    part2.Append("@tags");
                    flag = false;
                }
                else
                {
                    part1.Append(",tags");
                    part2.Append(",@tags");
                }
                parm.Add("tags", article.tags);
            }
            if(!article.cilckCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cilckCount");
                    part2.Append("@cilckCount");
                    flag = false;
                }
                else
                {
                    part1.Append(",cilckCount");
                    part2.Append(",@cilckCount");
                }
                parm.Add("cilckCount", article.cilckCount);
            }
            if(!article.loveCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loveCount");
                    part2.Append("@loveCount");
                    flag = false;
                }
                else
                {
                    part1.Append(",loveCount");
                    part2.Append(",@loveCount");
                }
                parm.Add("loveCount", article.loveCount);
            }
            if(!article.aTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("aTime");
                    part2.Append("@aTime");
                    flag = false;
                }
                else
                {
                    part1.Append(",aTime");
                    part2.Append(",@aTime");
                }
                parm.Add("aTime", article.aTime);
            }

            sql.Append("(").Append(part1).Append(") values(").Append(part2).Append(")");

            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
            var r = conn.Execute(sql.ToString(), parm);
            conn.Close();
            return r > 0;
        }
    }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public bool Delete(int id)
        {
            object parm = new { id = id };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
            var r = conn.Execute(@"Delete From Article where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="article"></param>
        /// <returns>是否成功</returns>
        public bool Update(Article article)
        {
            StringBuilder sql = new StringBuilder("update Article set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!article.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", article.id);
            }
            if(!article.title.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("title = @title");
                    flag = false;
                }
                else
                {
                    part1.Append(", title = @title");
                }
                parm.Add("title", article.title);
            }
            if(!article.url.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("url = @url");
                    flag = false;
                }
                else
                {
                    part1.Append(", url = @url");
                }
                parm.Add("url", article.url);
            }
            if(!article.content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("content = @content");
                    flag = false;
                }
                else
                {
                    part1.Append(", content = @content");
                }
                parm.Add("content", article.content);
            }
            if(!article.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail = @thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(", thumbnail = @thumbnail");
                }
                parm.Add("thumbnail", article.thumbnail);
            }
            if(!article.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags = @tags");
                    flag = false;
                }
                else
                {
                    part1.Append(", tags = @tags");
                }
                parm.Add("tags", article.tags);
            }
            if(!article.cilckCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cilckCount = @cilckCount");
                    flag = false;
                }
                else
                {
                    part1.Append(", cilckCount = @cilckCount");
                }
                parm.Add("cilckCount", article.cilckCount);
            }
            if(!article.loveCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loveCount = @loveCount");
                    flag = false;
                }
                else
                {
                    part1.Append(", loveCount = @loveCount");
                }
                parm.Add("loveCount", article.loveCount);
            }
            if(!article.aTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("aTime = @aTime");
                    flag = false;
                }
                else
                {
                    part1.Append(", aTime = @aTime");
                }
                parm.Add("aTime", article.aTime);
            }

            sql.Append(part1).Append(" where ").Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
            var r = conn.Execute(sql.ToString(), parm);
            conn.Close();
            return r > 0;
        }
    }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="article"></param>
        /// <returns>对象列表</returns>
        public List<Article> Select(Article article)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!article.Field.IsNullOrEmpty())
            {
                sql.Append(article.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Article ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!article.id.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("id = @id");
                    flag = false;
                }
                else
                {
                    part1.Append(" and id = @id");
                }
                parm.Add("id", article.id);
            }
            if(!article.title.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("title = @title");
                    flag = false;
                }
                else
                {
                    part1.Append(" and title = @title");
                }
                parm.Add("title", article.title);
            }
            if(!article.url.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("url = @url");
                    flag = false;
                }
                else
                {
                    part1.Append(" and url = @url");
                }
                parm.Add("url", article.url);
            }
            if(!article.content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("content = @content");
                    flag = false;
                }
                else
                {
                    part1.Append(" and content = @content");
                }
                parm.Add("content", article.content);
            }
            if(!article.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail = @thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(" and thumbnail = @thumbnail");
                }
                parm.Add("thumbnail", article.thumbnail);
            }
            if(!article.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags = @tags");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tags = @tags");
                }
                parm.Add("tags", article.tags);
            }
            if(!article.cilckCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cilckCount = @cilckCount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and cilckCount = @cilckCount");
                }
                parm.Add("cilckCount", article.cilckCount);
            }
            if(!article.loveCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loveCount = @loveCount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and loveCount = @loveCount");
                }
                parm.Add("loveCount", article.loveCount);
            }
            if(!article.aTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("aTime = @aTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and aTime = @aTime");
                }
                parm.Add("aTime", article.aTime);
            }

        if(!article.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(article.GroupBy).Append(" ");
            flag = false;
        }
        if(!article.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(article.OrderBy).Append(" ");
            flag = false;
        }
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Article>)conn.Query<Article>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Article>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="article"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Article> SelectByPage(Article article,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!article.Field.IsNullOrEmpty())
            {
                sql.Append(article.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Article ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!article.id.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("id = @id");
                    flag = false;
                }
                else
                {
                    part1.Append(" and id = @id");
                }
                parm.Add("id", article.id);
            }
            if(!article.title.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("title = @title");
                    flag = false;
                }
                else
                {
                    part1.Append(" and title = @title");
                }
                parm.Add("title", article.title);
            }
            if(!article.url.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("url = @url");
                    flag = false;
                }
                else
                {
                    part1.Append(" and url = @url");
                }
                parm.Add("url", article.url);
            }
            if(!article.content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("content = @content");
                    flag = false;
                }
                else
                {
                    part1.Append(" and content = @content");
                }
                parm.Add("content", article.content);
            }
            if(!article.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail = @thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(" and thumbnail = @thumbnail");
                }
                parm.Add("thumbnail", article.thumbnail);
            }
            if(!article.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags = @tags");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tags = @tags");
                }
                parm.Add("tags", article.tags);
            }
            if(!article.cilckCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cilckCount = @cilckCount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and cilckCount = @cilckCount");
                }
                parm.Add("cilckCount", article.cilckCount);
            }
            if(!article.loveCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loveCount = @loveCount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and loveCount = @loveCount");
                }
                parm.Add("loveCount", article.loveCount);
            }
            if(!article.aTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("aTime = @aTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and aTime = @aTime");
                }
                parm.Add("aTime", article.aTime);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Article ");
        if(!article.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(article.GroupBy).Append(" ");
            flag = false;
        }
        if(!article.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(article.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!article.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(article.GroupBy).Append(" ");
        }
        if(!article.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(article.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Article>)conn.Query<Article>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Article>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Article> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Article>)conn.Query<Article>("Select * From Article where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Article>();
                }
                return r;
        }
    }
    }
}
