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
    public partial class SleepOper : SingleTon<SleepOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sleep"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Sleep sleep)
        {
            StringBuilder sql = new StringBuilder("insert into Sleep ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sleep.cid.IsNullOrEmpty())
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
                parm.Add("cid", sleep.cid);
            }
            if(!sleep.sleepTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sleepTime");
                    part2.Append("@sleepTime");
                    flag = false;
                }
                else
                {
                    part1.Append(",sleepTime");
                    part2.Append(",@sleepTime");
                }
                parm.Add("sleepTime", sleep.sleepTime);
            }
            if(!sleep.wakeTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wakeTime");
                    part2.Append("@wakeTime");
                    flag = false;
                }
                else
                {
                    part1.Append(",wakeTime");
                    part2.Append(",@wakeTime");
                }
                parm.Add("wakeTime", sleep.wakeTime);
            }
            if(!sleep.cycle.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cycle");
                    part2.Append("@cycle");
                    flag = false;
                }
                else
                {
                    part1.Append(",cycle");
                    part2.Append(",@cycle");
                }
                parm.Add("cycle", sleep.cycle);
            }
            if(!sleep.enable.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("enable");
                    part2.Append("@enable");
                    flag = false;
                }
                else
                {
                    part1.Append(",enable");
                    part2.Append(",@enable");
                }
                parm.Add("enable", sleep.enable);
            }
            if(!sleep.advanceMinutes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("advanceMinutes");
                    part2.Append("@advanceMinutes");
                    flag = false;
                }
                else
                {
                    part1.Append(",advanceMinutes");
                    part2.Append(",@advanceMinutes");
                }
                parm.Add("advanceMinutes", sleep.advanceMinutes);
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
            var r = conn.Execute(@"Delete From Sleep where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sleep"></param>
        /// <returns>是否成功</returns>
        public bool Update(Sleep sleep)
        {
            StringBuilder sql = new StringBuilder("update Sleep set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sleep.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", sleep.id);
            }
            if(!sleep.cid.IsNullOrEmpty())
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
                parm.Add("cid", sleep.cid);
            }
            if(!sleep.sleepTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sleepTime = @sleepTime");
                    flag = false;
                }
                else
                {
                    part1.Append(", sleepTime = @sleepTime");
                }
                parm.Add("sleepTime", sleep.sleepTime);
            }
            if(!sleep.wakeTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wakeTime = @wakeTime");
                    flag = false;
                }
                else
                {
                    part1.Append(", wakeTime = @wakeTime");
                }
                parm.Add("wakeTime", sleep.wakeTime);
            }
            if(!sleep.cycle.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cycle = @cycle");
                    flag = false;
                }
                else
                {
                    part1.Append(", cycle = @cycle");
                }
                parm.Add("cycle", sleep.cycle);
            }
            if(!sleep.enable.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("enable = @enable");
                    flag = false;
                }
                else
                {
                    part1.Append(", enable = @enable");
                }
                parm.Add("enable", sleep.enable);
            }
            if(!sleep.advanceMinutes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("advanceMinutes = @advanceMinutes");
                    flag = false;
                }
                else
                {
                    part1.Append(", advanceMinutes = @advanceMinutes");
                }
                parm.Add("advanceMinutes", sleep.advanceMinutes);
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
        /// <param name="sleep"></param>
        /// <returns>对象列表</returns>
        public List<Sleep> Select(Sleep sleep)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!sleep.Field.IsNullOrEmpty())
            {
                sql.Append(sleep.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Sleep ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sleep.id.IsNullOrEmpty())
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
                parm.Add("id", sleep.id);
            }
            if(!sleep.cid.IsNullOrEmpty())
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
                parm.Add("cid", sleep.cid);
            }
            if(!sleep.sleepTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sleepTime = @sleepTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sleepTime = @sleepTime");
                }
                parm.Add("sleepTime", sleep.sleepTime);
            }
            if(!sleep.wakeTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wakeTime = @wakeTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and wakeTime = @wakeTime");
                }
                parm.Add("wakeTime", sleep.wakeTime);
            }
            if(!sleep.cycle.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cycle = @cycle");
                    flag = false;
                }
                else
                {
                    part1.Append(" and cycle = @cycle");
                }
                parm.Add("cycle", sleep.cycle);
            }
            if(!sleep.enable.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("enable = @enable");
                    flag = false;
                }
                else
                {
                    part1.Append(" and enable = @enable");
                }
                parm.Add("enable", sleep.enable);
            }
            if(!sleep.advanceMinutes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("advanceMinutes = @advanceMinutes");
                    flag = false;
                }
                else
                {
                    part1.Append(" and advanceMinutes = @advanceMinutes");
                }
                parm.Add("advanceMinutes", sleep.advanceMinutes);
            }

        if(!sleep.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(sleep.GroupBy).Append(" ");
            flag = false;
        }
        if(!sleep.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(sleep.OrderBy).Append(" ");
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
                var r = (List<Sleep>)conn.Query<Sleep>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Sleep>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sleep"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Sleep> SelectByPage(Sleep sleep,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!sleep.Field.IsNullOrEmpty())
            {
                sql.Append(sleep.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Sleep ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!sleep.id.IsNullOrEmpty())
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
                parm.Add("id", sleep.id);
            }
            if(!sleep.cid.IsNullOrEmpty())
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
                parm.Add("cid", sleep.cid);
            }
            if(!sleep.sleepTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sleepTime = @sleepTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sleepTime = @sleepTime");
                }
                parm.Add("sleepTime", sleep.sleepTime);
            }
            if(!sleep.wakeTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wakeTime = @wakeTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and wakeTime = @wakeTime");
                }
                parm.Add("wakeTime", sleep.wakeTime);
            }
            if(!sleep.cycle.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cycle = @cycle");
                    flag = false;
                }
                else
                {
                    part1.Append(" and cycle = @cycle");
                }
                parm.Add("cycle", sleep.cycle);
            }
            if(!sleep.enable.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("enable = @enable");
                    flag = false;
                }
                else
                {
                    part1.Append(" and enable = @enable");
                }
                parm.Add("enable", sleep.enable);
            }
            if(!sleep.advanceMinutes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("advanceMinutes = @advanceMinutes");
                    flag = false;
                }
                else
                {
                    part1.Append(" and advanceMinutes = @advanceMinutes");
                }
                parm.Add("advanceMinutes", sleep.advanceMinutes);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Sleep ");
        if(!sleep.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(sleep.GroupBy).Append(" ");
            flag = false;
        }
        if(!sleep.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(sleep.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!sleep.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(sleep.GroupBy).Append(" ");
        }
        if(!sleep.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(sleep.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Sleep>)conn.Query<Sleep>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Sleep>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Sleep> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Sleep>)conn.Query<Sleep>("Select * From Sleep where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Sleep>();
                }
                return r;
        }
    }
    }
}
