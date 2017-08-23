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
    public partial class ScoreOper : SingleTon<ScoreOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="score"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Score score)
        {
            StringBuilder sql = new StringBuilder("insert into Score ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!score.ScoreType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreType");
                    part2.Append("@ScoreType");
                    flag = false;
                }
                else
                {
                    part1.Append(",ScoreType");
                    part2.Append(",@ScoreType");
                }
                parm.Add("ScoreType", score.ScoreType);
            }
            if(!score.ScoreDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreDate");
                    part2.Append("@ScoreDate");
                    flag = false;
                }
                else
                {
                    part1.Append(",ScoreDate");
                    part2.Append(",@ScoreDate");
                }
                parm.Add("ScoreDate", score.ScoreDate);
            }
            if(!score.ScoreNum.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreNum");
                    part2.Append("@ScoreNum");
                    flag = false;
                }
                else
                {
                    part1.Append(",ScoreNum");
                    part2.Append(",@ScoreNum");
                }
                parm.Add("ScoreNum", score.ScoreNum);
            }
            if(!score.ScoreClick.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreClick");
                    part2.Append("@ScoreClick");
                    flag = false;
                }
                else
                {
                    part1.Append(",ScoreClick");
                    part2.Append(",@ScoreClick");
                }
                parm.Add("ScoreClick", score.ScoreClick);
            }
            if(!score.UserId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserId");
                    part2.Append("@UserId");
                    flag = false;
                }
                else
                {
                    part1.Append(",UserId");
                    part2.Append(",@UserId");
                }
                parm.Add("UserId", score.UserId);
            }
            if(!score.ScoreContent.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreContent");
                    part2.Append("@ScoreContent");
                    flag = false;
                }
                else
                {
                    part1.Append(",ScoreContent");
                    part2.Append(",@ScoreContent");
                }
                parm.Add("ScoreContent", score.ScoreContent);
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
            object parm = new { ScoreId = id };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
            var r = conn.Execute(@"Delete From Score where ScoreId=@ScoreId",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="score"></param>
        /// <returns>是否成功</returns>
        public bool Update(Score score)
        {
            StringBuilder sql = new StringBuilder("update Score set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!score.ScoreId.IsNullOrEmpty())
            {
                part2.Append("ScoreId = @ScoreId");
                parm.Add("ScoreId", score.ScoreId);
            }
            if(!score.ScoreType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreType = @ScoreType");
                    flag = false;
                }
                else
                {
                    part1.Append(", ScoreType = @ScoreType");
                }
                parm.Add("ScoreType", score.ScoreType);
            }
            if(!score.ScoreDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreDate = @ScoreDate");
                    flag = false;
                }
                else
                {
                    part1.Append(", ScoreDate = @ScoreDate");
                }
                parm.Add("ScoreDate", score.ScoreDate);
            }
            if(!score.ScoreNum.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreNum = @ScoreNum");
                    flag = false;
                }
                else
                {
                    part1.Append(", ScoreNum = @ScoreNum");
                }
                parm.Add("ScoreNum", score.ScoreNum);
            }
            if(!score.ScoreClick.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreClick = @ScoreClick");
                    flag = false;
                }
                else
                {
                    part1.Append(", ScoreClick = @ScoreClick");
                }
                parm.Add("ScoreClick", score.ScoreClick);
            }
            if(!score.UserId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserId = @UserId");
                    flag = false;
                }
                else
                {
                    part1.Append(", UserId = @UserId");
                }
                parm.Add("UserId", score.UserId);
            }
            if(!score.ScoreContent.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreContent = @ScoreContent");
                    flag = false;
                }
                else
                {
                    part1.Append(", ScoreContent = @ScoreContent");
                }
                parm.Add("ScoreContent", score.ScoreContent);
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
        /// <param name="score"></param>
        /// <returns>对象列表</returns>
        public List<Score> Select(Score score)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!score.Field.IsNullOrEmpty())
            {
                sql.Append(score.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Score ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!score.ScoreId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreId = @ScoreId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreId = @ScoreId");
                }
                parm.Add("ScoreId", score.ScoreId);
            }
            if(!score.ScoreType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreType = @ScoreType");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreType = @ScoreType");
                }
                parm.Add("ScoreType", score.ScoreType);
            }
            if(!score.ScoreDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreDate = @ScoreDate");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreDate = @ScoreDate");
                }
                parm.Add("ScoreDate", score.ScoreDate);
            }
            if(!score.ScoreNum.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreNum = @ScoreNum");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreNum = @ScoreNum");
                }
                parm.Add("ScoreNum", score.ScoreNum);
            }
            if(!score.ScoreClick.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreClick = @ScoreClick");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreClick = @ScoreClick");
                }
                parm.Add("ScoreClick", score.ScoreClick);
            }
            if(!score.UserId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserId = @UserId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and UserId = @UserId");
                }
                parm.Add("UserId", score.UserId);
            }
            if(!score.ScoreContent.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreContent = @ScoreContent");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreContent = @ScoreContent");
                }
                parm.Add("ScoreContent", score.ScoreContent);
            }

        if(!score.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(score.GroupBy).Append(" ");
            flag = false;
        }
        if(!score.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(score.OrderBy).Append(" ");
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
                var r = (List<Score>)conn.Query<Score>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Score>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="score"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Score> SelectByPage(Score score,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!score.Field.IsNullOrEmpty())
            {
                sql.Append(score.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Score ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!score.ScoreId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreId = @ScoreId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreId = @ScoreId");
                }
                parm.Add("ScoreId", score.ScoreId);
            }
            if(!score.ScoreType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreType = @ScoreType");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreType = @ScoreType");
                }
                parm.Add("ScoreType", score.ScoreType);
            }
            if(!score.ScoreDate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreDate = @ScoreDate");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreDate = @ScoreDate");
                }
                parm.Add("ScoreDate", score.ScoreDate);
            }
            if(!score.ScoreNum.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreNum = @ScoreNum");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreNum = @ScoreNum");
                }
                parm.Add("ScoreNum", score.ScoreNum);
            }
            if(!score.ScoreClick.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreClick = @ScoreClick");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreClick = @ScoreClick");
                }
                parm.Add("ScoreClick", score.ScoreClick);
            }
            if(!score.UserId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserId = @UserId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and UserId = @UserId");
                }
                parm.Add("UserId", score.UserId);
            }
            if(!score.ScoreContent.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ScoreContent = @ScoreContent");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ScoreContent = @ScoreContent");
                }
                parm.Add("ScoreContent", score.ScoreContent);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" ScoreId not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" ScoreId from Score ");
        if(!score.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(score.GroupBy).Append(" ");
            flag = false;
        }
        if(!score.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(score.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!score.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(score.GroupBy).Append(" ");
        }
        if(!score.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(score.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Score>)conn.Query<Score>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Score>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Score> SelectByIds(List<string> List_Id)
        {
            object parm = new { ScoreId = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Score>)conn.Query<Score>("Select * From Score where ScoreId in @ScoreId", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Score>();
                }
                return r;
        }
    }
    }
}
