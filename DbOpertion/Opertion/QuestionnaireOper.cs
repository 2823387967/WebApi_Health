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
    public partial class QuestionnaireOper : SingleTon<QuestionnaireOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="questionnaire"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Questionnaire questionnaire)
        {
            StringBuilder sql = new StringBuilder("insert into Questionnaire ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!questionnaire.Constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Constitution");
                    part2.Append("@Constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(",Constitution");
                    part2.Append(",@Constitution");
                }
                parm.Add("Constitution", questionnaire.Constitution);
            }
            if(!questionnaire.QuesOrOp.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("QuesOrOp");
                    part2.Append("@QuesOrOp");
                    flag = false;
                }
                else
                {
                    part1.Append(",QuesOrOp");
                    part2.Append(",@QuesOrOp");
                }
                parm.Add("QuesOrOp", questionnaire.QuesOrOp);
            }
            if(!questionnaire.Content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Content");
                    part2.Append("@Content");
                    flag = false;
                }
                else
                {
                    part1.Append(",Content");
                    part2.Append(",@Content");
                }
                parm.Add("Content", questionnaire.Content);
            }
            if(!questionnaire.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category");
                    part2.Append("@category");
                    flag = false;
                }
                else
                {
                    part1.Append(",category");
                    part2.Append(",@category");
                }
                parm.Add("category", questionnaire.category);
            }
            if(!questionnaire.RelationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RelationId");
                    part2.Append("@RelationId");
                    flag = false;
                }
                else
                {
                    part1.Append(",RelationId");
                    part2.Append(",@RelationId");
                }
                parm.Add("RelationId", questionnaire.RelationId);
            }
            if(!questionnaire.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex");
                    part2.Append("@sex");
                    flag = false;
                }
                else
                {
                    part1.Append(",sex");
                    part2.Append(",@sex");
                }
                parm.Add("sex", questionnaire.sex);
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
            var r = conn.Execute(@"Delete From Questionnaire where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="questionnaire"></param>
        /// <returns>是否成功</returns>
        public bool Update(Questionnaire questionnaire)
        {
            StringBuilder sql = new StringBuilder("update Questionnaire set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!questionnaire.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", questionnaire.id);
            }
            if(!questionnaire.Constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Constitution = @Constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(", Constitution = @Constitution");
                }
                parm.Add("Constitution", questionnaire.Constitution);
            }
            if(!questionnaire.QuesOrOp.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("QuesOrOp = @QuesOrOp");
                    flag = false;
                }
                else
                {
                    part1.Append(", QuesOrOp = @QuesOrOp");
                }
                parm.Add("QuesOrOp", questionnaire.QuesOrOp);
            }
            if(!questionnaire.Content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Content = @Content");
                    flag = false;
                }
                else
                {
                    part1.Append(", Content = @Content");
                }
                parm.Add("Content", questionnaire.Content);
            }
            if(!questionnaire.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category = @category");
                    flag = false;
                }
                else
                {
                    part1.Append(", category = @category");
                }
                parm.Add("category", questionnaire.category);
            }
            if(!questionnaire.RelationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RelationId = @RelationId");
                    flag = false;
                }
                else
                {
                    part1.Append(", RelationId = @RelationId");
                }
                parm.Add("RelationId", questionnaire.RelationId);
            }
            if(!questionnaire.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex = @sex");
                    flag = false;
                }
                else
                {
                    part1.Append(", sex = @sex");
                }
                parm.Add("sex", questionnaire.sex);
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
        /// <param name="questionnaire"></param>
        /// <returns>对象列表</returns>
        public List<Questionnaire> Select(Questionnaire questionnaire)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!questionnaire.Field.IsNullOrEmpty())
            {
                sql.Append(questionnaire.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Questionnaire ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!questionnaire.id.IsNullOrEmpty())
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
                parm.Add("id", questionnaire.id);
            }
            if(!questionnaire.Constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Constitution = @Constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Constitution = @Constitution");
                }
                parm.Add("Constitution", questionnaire.Constitution);
            }
            if(!questionnaire.QuesOrOp.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("QuesOrOp = @QuesOrOp");
                    flag = false;
                }
                else
                {
                    part1.Append(" and QuesOrOp = @QuesOrOp");
                }
                parm.Add("QuesOrOp", questionnaire.QuesOrOp);
            }
            if(!questionnaire.Content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Content = @Content");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Content = @Content");
                }
                parm.Add("Content", questionnaire.Content);
            }
            if(!questionnaire.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category = @category");
                    flag = false;
                }
                else
                {
                    part1.Append(" and category = @category");
                }
                parm.Add("category", questionnaire.category);
            }
            if(!questionnaire.RelationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RelationId = @RelationId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and RelationId = @RelationId");
                }
                parm.Add("RelationId", questionnaire.RelationId);
            }
            if(!questionnaire.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex = @sex");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sex = @sex");
                }
                parm.Add("sex", questionnaire.sex);
            }

        if(!questionnaire.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(questionnaire.GroupBy).Append(" ");
            flag = false;
        }
        if(!questionnaire.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(questionnaire.OrderBy).Append(" ");
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
                var r = (List<Questionnaire>)conn.Query<Questionnaire>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Questionnaire>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="questionnaire"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Questionnaire> SelectByPage(Questionnaire questionnaire,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!questionnaire.Field.IsNullOrEmpty())
            {
                sql.Append(questionnaire.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Questionnaire ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!questionnaire.id.IsNullOrEmpty())
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
                parm.Add("id", questionnaire.id);
            }
            if(!questionnaire.Constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Constitution = @Constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Constitution = @Constitution");
                }
                parm.Add("Constitution", questionnaire.Constitution);
            }
            if(!questionnaire.QuesOrOp.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("QuesOrOp = @QuesOrOp");
                    flag = false;
                }
                else
                {
                    part1.Append(" and QuesOrOp = @QuesOrOp");
                }
                parm.Add("QuesOrOp", questionnaire.QuesOrOp);
            }
            if(!questionnaire.Content.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Content = @Content");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Content = @Content");
                }
                parm.Add("Content", questionnaire.Content);
            }
            if(!questionnaire.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category = @category");
                    flag = false;
                }
                else
                {
                    part1.Append(" and category = @category");
                }
                parm.Add("category", questionnaire.category);
            }
            if(!questionnaire.RelationId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RelationId = @RelationId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and RelationId = @RelationId");
                }
                parm.Add("RelationId", questionnaire.RelationId);
            }
            if(!questionnaire.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex = @sex");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sex = @sex");
                }
                parm.Add("sex", questionnaire.sex);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Questionnaire ");
        if(!questionnaire.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(questionnaire.GroupBy).Append(" ");
            flag = false;
        }
        if(!questionnaire.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(questionnaire.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!questionnaire.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(questionnaire.GroupBy).Append(" ");
        }
        if(!questionnaire.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(questionnaire.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Questionnaire>)conn.Query<Questionnaire>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Questionnaire>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Questionnaire> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Questionnaire>)conn.Query<Questionnaire>("Select * From Questionnaire where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Questionnaire>();
                }
                return r;
        }
    }
    }
}
