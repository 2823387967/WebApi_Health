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
    public partial class Tag_RelationOper : SingleTon<Tag_RelationOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="tag_relation"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Tag_Relation tag_relation)
        {
            StringBuilder sql = new StringBuilder("insert into Tag_Relation ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag_relation.relationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("relationId");
                    part2.Append("@relationId");
                    flag = false;
                }
                else
                {
                    part1.Append(",relationId");
                    part2.Append(",@relationId");
                }
                parm.Add("relationId", tag_relation.relationId);
            }
            if(!tag_relation.tagId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tagId");
                    part2.Append("@tagId");
                    flag = false;
                }
                else
                {
                    part1.Append(",tagId");
                    part2.Append(",@tagId");
                }
                parm.Add("tagId", tag_relation.tagId);
            }
            if(!tag_relation.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename");
                    part2.Append("@typename");
                    flag = false;
                }
                else
                {
                    part1.Append(",typename");
                    part2.Append(",@typename");
                }
                parm.Add("typename", tag_relation.typename);
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
            var r = conn.Execute(@"Delete From Tag_Relation where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tag_relation"></param>
        /// <returns>是否成功</returns>
        public bool Update(Tag_Relation tag_relation)
        {
            StringBuilder sql = new StringBuilder("update Tag_Relation set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag_relation.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", tag_relation.id);
            }
            if(!tag_relation.relationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("relationId = @relationId");
                    flag = false;
                }
                else
                {
                    part1.Append(", relationId = @relationId");
                }
                parm.Add("relationId", tag_relation.relationId);
            }
            if(!tag_relation.tagId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tagId = @tagId");
                    flag = false;
                }
                else
                {
                    part1.Append(", tagId = @tagId");
                }
                parm.Add("tagId", tag_relation.tagId);
            }
            if(!tag_relation.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename = @typename");
                    flag = false;
                }
                else
                {
                    part1.Append(", typename = @typename");
                }
                parm.Add("typename", tag_relation.typename);
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
        /// <param name="tag_relation"></param>
        /// <returns>对象列表</returns>
        public List<Tag_Relation> Select(Tag_Relation tag_relation)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!tag_relation.Field.IsNullOrEmpty())
            {
                sql.Append(tag_relation.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Tag_Relation ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag_relation.id.IsNullOrEmpty())
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
                parm.Add("id", tag_relation.id);
            }
            if(!tag_relation.relationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("relationId = @relationId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and relationId = @relationId");
                }
                parm.Add("relationId", tag_relation.relationId);
            }
            if(!tag_relation.tagId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tagId = @tagId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tagId = @tagId");
                }
                parm.Add("tagId", tag_relation.tagId);
            }
            if(!tag_relation.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename = @typename");
                    flag = false;
                }
                else
                {
                    part1.Append(" and typename = @typename");
                }
                parm.Add("typename", tag_relation.typename);
            }

        if(!tag_relation.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(tag_relation.GroupBy).Append(" ");
            flag = false;
        }
        if(!tag_relation.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(tag_relation.OrderBy).Append(" ");
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
                var r = (List<Tag_Relation>)conn.Query<Tag_Relation>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Tag_Relation>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="tag_relation"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Tag_Relation> SelectByPage(Tag_Relation tag_relation,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!tag_relation.Field.IsNullOrEmpty())
            {
                sql.Append(tag_relation.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Tag_Relation ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!tag_relation.id.IsNullOrEmpty())
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
                parm.Add("id", tag_relation.id);
            }
            if(!tag_relation.relationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("relationId = @relationId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and relationId = @relationId");
                }
                parm.Add("relationId", tag_relation.relationId);
            }
            if(!tag_relation.tagId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tagId = @tagId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tagId = @tagId");
                }
                parm.Add("tagId", tag_relation.tagId);
            }
            if(!tag_relation.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename = @typename");
                    flag = false;
                }
                else
                {
                    part1.Append(" and typename = @typename");
                }
                parm.Add("typename", tag_relation.typename);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Tag_Relation ");
        if(!tag_relation.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(tag_relation.GroupBy).Append(" ");
            flag = false;
        }
        if(!tag_relation.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(tag_relation.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!tag_relation.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(tag_relation.GroupBy).Append(" ");
        }
        if(!tag_relation.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(tag_relation.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Tag_Relation>)conn.Query<Tag_Relation>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Tag_Relation>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Tag_Relation> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Tag_Relation>)conn.Query<Tag_Relation>("Select * From Tag_Relation where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Tag_Relation>();
                }
                return r;
        }
    }
    }
}
