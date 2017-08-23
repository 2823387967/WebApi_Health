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
    public partial class TagOper : SingleTon<TagOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Tag tag)
        {
            StringBuilder sql = new StringBuilder("insert into Tag ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag.name.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("name");
                    part2.Append("@name");
                    flag = false;
                }
                else
                {
                    part1.Append(",name");
                    part2.Append(",@name");
                }
                parm.Add("name", tag.name);
            }
            if(!tag.pinghescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("pinghescore");
                    part2.Append("@pinghescore");
                    flag = false;
                }
                else
                {
                    part1.Append(",pinghescore");
                    part2.Append(",@pinghescore");
                }
                parm.Add("pinghescore", tag.pinghescore);
            }
            if(!tag.qiyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qiyuscore");
                    part2.Append("@qiyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(",qiyuscore");
                    part2.Append(",@qiyuscore");
                }
                parm.Add("qiyuscore", tag.qiyuscore);
            }
            if(!tag.yinxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yinxuscore");
                    part2.Append("@yinxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(",yinxuscore");
                    part2.Append(",@yinxuscore");
                }
                parm.Add("yinxuscore", tag.yinxuscore);
            }
            if(!tag.tanshiscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tanshiscore");
                    part2.Append("@tanshiscore");
                    flag = false;
                }
                else
                {
                    part1.Append(",tanshiscore");
                    part2.Append(",@tanshiscore");
                }
                parm.Add("tanshiscore", tag.tanshiscore);
            }
            if(!tag.yangxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yangxuscore");
                    part2.Append("@yangxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(",yangxuscore");
                    part2.Append(",@yangxuscore");
                }
                parm.Add("yangxuscore", tag.yangxuscore);
            }
            if(!tag.tebingscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tebingscore");
                    part2.Append("@tebingscore");
                    flag = false;
                }
                else
                {
                    part1.Append(",tebingscore");
                    part2.Append(",@tebingscore");
                }
                parm.Add("tebingscore", tag.tebingscore);
            }
            if(!tag.shirescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("shirescore");
                    part2.Append("@shirescore");
                    flag = false;
                }
                else
                {
                    part1.Append(",shirescore");
                    part2.Append(",@shirescore");
                }
                parm.Add("shirescore", tag.shirescore);
            }
            if(!tag.qixuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qixuscore");
                    part2.Append("@qixuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(",qixuscore");
                    part2.Append(",@qixuscore");
                }
                parm.Add("qixuscore", tag.qixuscore);
            }
            if(!tag.xueyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("xueyuscore");
                    part2.Append("@xueyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(",xueyuscore");
                    part2.Append(",@xueyuscore");
                }
                parm.Add("xueyuscore", tag.xueyuscore);
            }
            if(!tag.isDeleted.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("isDeleted");
                    part2.Append("@isDeleted");
                    flag = false;
                }
                else
                {
                    part1.Append(",isDeleted");
                    part2.Append(",@isDeleted");
                }
                parm.Add("isDeleted", tag.isDeleted);
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
            var r = conn.Execute(@"Delete From Tag where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>是否成功</returns>
        public bool Update(Tag tag)
        {
            StringBuilder sql = new StringBuilder("update Tag set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", tag.id);
            }
            if(!tag.name.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("name = @name");
                    flag = false;
                }
                else
                {
                    part1.Append(", name = @name");
                }
                parm.Add("name", tag.name);
            }
            if(!tag.pinghescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("pinghescore = @pinghescore");
                    flag = false;
                }
                else
                {
                    part1.Append(", pinghescore = @pinghescore");
                }
                parm.Add("pinghescore", tag.pinghescore);
            }
            if(!tag.qiyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qiyuscore = @qiyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(", qiyuscore = @qiyuscore");
                }
                parm.Add("qiyuscore", tag.qiyuscore);
            }
            if(!tag.yinxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yinxuscore = @yinxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(", yinxuscore = @yinxuscore");
                }
                parm.Add("yinxuscore", tag.yinxuscore);
            }
            if(!tag.tanshiscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tanshiscore = @tanshiscore");
                    flag = false;
                }
                else
                {
                    part1.Append(", tanshiscore = @tanshiscore");
                }
                parm.Add("tanshiscore", tag.tanshiscore);
            }
            if(!tag.yangxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yangxuscore = @yangxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(", yangxuscore = @yangxuscore");
                }
                parm.Add("yangxuscore", tag.yangxuscore);
            }
            if(!tag.tebingscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tebingscore = @tebingscore");
                    flag = false;
                }
                else
                {
                    part1.Append(", tebingscore = @tebingscore");
                }
                parm.Add("tebingscore", tag.tebingscore);
            }
            if(!tag.shirescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("shirescore = @shirescore");
                    flag = false;
                }
                else
                {
                    part1.Append(", shirescore = @shirescore");
                }
                parm.Add("shirescore", tag.shirescore);
            }
            if(!tag.qixuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qixuscore = @qixuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(", qixuscore = @qixuscore");
                }
                parm.Add("qixuscore", tag.qixuscore);
            }
            if(!tag.xueyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("xueyuscore = @xueyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(", xueyuscore = @xueyuscore");
                }
                parm.Add("xueyuscore", tag.xueyuscore);
            }
            if(!tag.isDeleted.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("isDeleted = @isDeleted");
                    flag = false;
                }
                else
                {
                    part1.Append(", isDeleted = @isDeleted");
                }
                parm.Add("isDeleted", tag.isDeleted);
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
        /// <param name="tag"></param>
        /// <returns>对象列表</returns>
        public List<Tag> Select(Tag tag)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!tag.Field.IsNullOrEmpty())
            {
                sql.Append(tag.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Tag ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag.id.IsNullOrEmpty())
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
                parm.Add("id", tag.id);
            }
            if(!tag.name.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("name = @name");
                    flag = false;
                }
                else
                {
                    part1.Append(" and name = @name");
                }
                parm.Add("name", tag.name);
            }
            if(!tag.pinghescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("pinghescore = @pinghescore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and pinghescore = @pinghescore");
                }
                parm.Add("pinghescore", tag.pinghescore);
            }
            if(!tag.qiyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qiyuscore = @qiyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and qiyuscore = @qiyuscore");
                }
                parm.Add("qiyuscore", tag.qiyuscore);
            }
            if(!tag.yinxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yinxuscore = @yinxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and yinxuscore = @yinxuscore");
                }
                parm.Add("yinxuscore", tag.yinxuscore);
            }
            if(!tag.tanshiscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tanshiscore = @tanshiscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tanshiscore = @tanshiscore");
                }
                parm.Add("tanshiscore", tag.tanshiscore);
            }
            if(!tag.yangxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yangxuscore = @yangxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and yangxuscore = @yangxuscore");
                }
                parm.Add("yangxuscore", tag.yangxuscore);
            }
            if(!tag.tebingscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tebingscore = @tebingscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tebingscore = @tebingscore");
                }
                parm.Add("tebingscore", tag.tebingscore);
            }
            if(!tag.shirescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("shirescore = @shirescore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and shirescore = @shirescore");
                }
                parm.Add("shirescore", tag.shirescore);
            }
            if(!tag.qixuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qixuscore = @qixuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and qixuscore = @qixuscore");
                }
                parm.Add("qixuscore", tag.qixuscore);
            }
            if(!tag.xueyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("xueyuscore = @xueyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and xueyuscore = @xueyuscore");
                }
                parm.Add("xueyuscore", tag.xueyuscore);
            }
            if(!tag.isDeleted.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("isDeleted = @isDeleted");
                    flag = false;
                }
                else
                {
                    part1.Append(" and isDeleted = @isDeleted");
                }
                parm.Add("isDeleted", tag.isDeleted);
            }

        if(!tag.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(tag.GroupBy).Append(" ");
            flag = false;
        }
        if(!tag.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(tag.OrderBy).Append(" ");
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
                var r = (List<Tag>)conn.Query<Tag>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Tag>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Tag> SelectByPage(Tag tag,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!tag.Field.IsNullOrEmpty())
            {
                sql.Append(tag.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Tag ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag.id.IsNullOrEmpty())
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
                parm.Add("id", tag.id);
            }
            if(!tag.name.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("name = @name");
                    flag = false;
                }
                else
                {
                    part1.Append(" and name = @name");
                }
                parm.Add("name", tag.name);
            }
            if(!tag.pinghescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("pinghescore = @pinghescore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and pinghescore = @pinghescore");
                }
                parm.Add("pinghescore", tag.pinghescore);
            }
            if(!tag.qiyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qiyuscore = @qiyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and qiyuscore = @qiyuscore");
                }
                parm.Add("qiyuscore", tag.qiyuscore);
            }
            if(!tag.yinxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yinxuscore = @yinxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and yinxuscore = @yinxuscore");
                }
                parm.Add("yinxuscore", tag.yinxuscore);
            }
            if(!tag.tanshiscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tanshiscore = @tanshiscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tanshiscore = @tanshiscore");
                }
                parm.Add("tanshiscore", tag.tanshiscore);
            }
            if(!tag.yangxuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("yangxuscore = @yangxuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and yangxuscore = @yangxuscore");
                }
                parm.Add("yangxuscore", tag.yangxuscore);
            }
            if(!tag.tebingscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tebingscore = @tebingscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tebingscore = @tebingscore");
                }
                parm.Add("tebingscore", tag.tebingscore);
            }
            if(!tag.shirescore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("shirescore = @shirescore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and shirescore = @shirescore");
                }
                parm.Add("shirescore", tag.shirescore);
            }
            if(!tag.qixuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("qixuscore = @qixuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and qixuscore = @qixuscore");
                }
                parm.Add("qixuscore", tag.qixuscore);
            }
            if(!tag.xueyuscore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("xueyuscore = @xueyuscore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and xueyuscore = @xueyuscore");
                }
                parm.Add("xueyuscore", tag.xueyuscore);
            }
            if(!tag.isDeleted.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("isDeleted = @isDeleted");
                    flag = false;
                }
                else
                {
                    part1.Append(" and isDeleted = @isDeleted");
                }
                parm.Add("isDeleted", tag.isDeleted);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Tag ");
        if(!tag.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(tag.GroupBy).Append(" ");
            flag = false;
        }
        if(!tag.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(tag.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!tag.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(tag.GroupBy).Append(" ");
        }
        if(!tag.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(tag.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Tag>)conn.Query<Tag>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Tag>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Tag> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Tag>)conn.Query<Tag>("Select * From Tag where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Tag>();
                }
                return r;
        }
    }
    }
}
