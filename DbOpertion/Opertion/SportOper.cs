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
    public partial class SportOper : SingleTon<SportOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sport"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Sport sport)
        {
            StringBuilder sql = new StringBuilder("insert into Sport ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sport.cid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cid");
                    part2.Append("@cid");
                    flag = false;
                }
                else
                {
                    part1.Append(",cid");
                    part2.Append(",@cid");
                }
                parm.Add("cid", sport.cid);
            }
            if(!sport.sDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sDate");
                    part2.Append("@sDate");
                    flag = false;
                }
                else
                {
                    part1.Append(",sDate");
                    part2.Append(",@sDate");
                }
                parm.Add("sDate", sport.sDate);
            }
            if(!sport.steps.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("steps");
                    part2.Append("@steps");
                    flag = false;
                }
                else
                {
                    part1.Append(",steps");
                    part2.Append(",@steps");
                }
                parm.Add("steps", sport.steps);
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
            var r = conn.Execute(@"Delete From Sport where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sport"></param>
        /// <returns>是否成功</returns>
        public bool Update(Sport sport)
        {
            StringBuilder sql = new StringBuilder("update Sport set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sport.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", sport.id);
            }
            if(!sport.cid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cid = @cid");
                    flag = false;
                }
                else
                {
                    part1.Append(", cid = @cid");
                }
                parm.Add("cid", sport.cid);
            }
            if(!sport.sDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sDate = @sDate");
                    flag = false;
                }
                else
                {
                    part1.Append(", sDate = @sDate");
                }
                parm.Add("sDate", sport.sDate);
            }
            if(!sport.steps.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("steps = @steps");
                    flag = false;
                }
                else
                {
                    part1.Append(", steps = @steps");
                }
                parm.Add("steps", sport.steps);
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
        /// <param name="sport"></param>
        /// <returns>对象列表</returns>
        public List<Sport> Select(Sport sport)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!sport.Field.IsNullOrEmpty())
            {
                sql.Append(sport.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Sport ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sport.id.IsNullOrEmpty())
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
                parm.Add("id", sport.id);
            }
            if(!sport.cid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cid = @cid");
                    flag = false;
                }
                else
                {
                    part1.Append(" and cid = @cid");
                }
                parm.Add("cid", sport.cid);
            }
            if(!sport.sDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sDate = @sDate");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sDate = @sDate");
                }
                parm.Add("sDate", sport.sDate);
            }
            if(!sport.steps.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("steps = @steps");
                    flag = false;
                }
                else
                {
                    part1.Append(" and steps = @steps");
                }
                parm.Add("steps", sport.steps);
            }

        if(!sport.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(sport.GroupBy).Append(" ");
            flag = false;
        }
        if(!sport.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(sport.OrderBy).Append(" ");
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
                var r = (List<Sport>)conn.Query<Sport>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Sport>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sport"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Sport> SelectByPage(Sport sport,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!sport.Field.IsNullOrEmpty())
            {
                sql.Append(sport.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Sport ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sport.id.IsNullOrEmpty())
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
                parm.Add("id", sport.id);
            }
            if(!sport.cid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cid = @cid");
                    flag = false;
                }
                else
                {
                    part1.Append(" and cid = @cid");
                }
                parm.Add("cid", sport.cid);
            }
            if(!sport.sDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sDate = @sDate");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sDate = @sDate");
                }
                parm.Add("sDate", sport.sDate);
            }
            if(!sport.steps.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("steps = @steps");
                    flag = false;
                }
                else
                {
                    part1.Append(" and steps = @steps");
                }
                parm.Add("steps", sport.steps);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Sport ");
        if(!sport.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(sport.GroupBy).Append(" ");
            flag = false;
        }
        if(!sport.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(sport.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!sport.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(sport.GroupBy).Append(" ");
        }
        if(!sport.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(sport.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Sport>)conn.Query<Sport>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Sport>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Sport> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Sport>)conn.Query<Sport>("Select * From Sport where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Sport>();
                }
                return r;
        }
    }
    }
}
