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
    public partial class ConstitutionResultOper : SingleTon<ConstitutionResultOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="constitutionresult"></param>
        /// <returns>是否成功</returns>
        public bool Insert(ConstitutionResult constitutionresult)
        {
            StringBuilder sql = new StringBuilder("insert into ConstitutionResult ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!constitutionresult.name.IsNullOrEmpty())
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
                parm.Add("name", constitutionresult.name);
            }
            if(!constitutionresult.content.IsNullOrEmpty())
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
                parm.Add("content", constitutionresult.content);
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
            var r = conn.Execute(@"Delete From ConstitutionResult where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="constitutionresult"></param>
        /// <returns>是否成功</returns>
        public bool Update(ConstitutionResult constitutionresult)
        {
            StringBuilder sql = new StringBuilder("update ConstitutionResult set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!constitutionresult.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", constitutionresult.id);
            }
            if(!constitutionresult.name.IsNullOrEmpty())
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
                parm.Add("name", constitutionresult.name);
            }
            if(!constitutionresult.content.IsNullOrEmpty())
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
                parm.Add("content", constitutionresult.content);
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
        /// <param name="constitutionresult"></param>
        /// <returns>对象列表</returns>
        public List<ConstitutionResult> Select(ConstitutionResult constitutionresult)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!constitutionresult.Field.IsNullOrEmpty())
            {
                sql.Append(constitutionresult.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from ConstitutionResult ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!constitutionresult.id.IsNullOrEmpty())
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
                parm.Add("id", constitutionresult.id);
            }
            if(!constitutionresult.name.IsNullOrEmpty())
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
                parm.Add("name", constitutionresult.name);
            }
            if(!constitutionresult.content.IsNullOrEmpty())
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
                parm.Add("content", constitutionresult.content);
            }

        if(!constitutionresult.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(constitutionresult.GroupBy).Append(" ");
            flag = false;
        }
        if(!constitutionresult.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(constitutionresult.OrderBy).Append(" ");
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
                var r = (List<ConstitutionResult>)conn.Query<ConstitutionResult>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<ConstitutionResult>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="constitutionresult"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<ConstitutionResult> SelectByPage(ConstitutionResult constitutionresult,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!constitutionresult.Field.IsNullOrEmpty())
            {
                sql.Append(constitutionresult.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from ConstitutionResult ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!constitutionresult.id.IsNullOrEmpty())
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
                parm.Add("id", constitutionresult.id);
            }
            if(!constitutionresult.name.IsNullOrEmpty())
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
                parm.Add("name", constitutionresult.name);
            }
            if(!constitutionresult.content.IsNullOrEmpty())
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
                parm.Add("content", constitutionresult.content);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from ConstitutionResult ");
        if(!constitutionresult.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(constitutionresult.GroupBy).Append(" ");
            flag = false;
        }
        if(!constitutionresult.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(constitutionresult.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!constitutionresult.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(constitutionresult.GroupBy).Append(" ");
        }
        if(!constitutionresult.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(constitutionresult.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<ConstitutionResult>)conn.Query<ConstitutionResult>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<ConstitutionResult>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<ConstitutionResult> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<ConstitutionResult>)conn.Query<ConstitutionResult>("Select * From ConstitutionResult where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<ConstitutionResult>();
                }
                return r;
        }
    }
    }
}
