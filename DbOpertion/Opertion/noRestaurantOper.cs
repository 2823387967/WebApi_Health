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
    public partial class noRestaurantOper : SingleTon<noRestaurantOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="norestaurant"></param>
        /// <returns>是否成功</returns>
        public bool Insert(noRestaurant norestaurant)
        {
            StringBuilder sql = new StringBuilder("insert into noRestaurant ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!norestaurant.location.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("location");
                    part2.Append("@location");
                    flag = false;
                }
                else
                {
                    part1.Append(",location");
                    part2.Append(",@location");
                }
                parm.Add("location", norestaurant.location);
            }
            if(!norestaurant.times.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("times");
                    part2.Append("@times");
                    flag = false;
                }
                else
                {
                    part1.Append(",times");
                    part2.Append(",@times");
                }
                parm.Add("times", norestaurant.times);
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
            var r = conn.Execute(@"Delete From noRestaurant where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="norestaurant"></param>
        /// <returns>是否成功</returns>
        public bool Update(noRestaurant norestaurant)
        {
            StringBuilder sql = new StringBuilder("update noRestaurant set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!norestaurant.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", norestaurant.id);
            }
            if(!norestaurant.location.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("location = @location");
                    flag = false;
                }
                else
                {
                    part1.Append(", location = @location");
                }
                parm.Add("location", norestaurant.location);
            }
            if(!norestaurant.times.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("times = @times");
                    flag = false;
                }
                else
                {
                    part1.Append(", times = @times");
                }
                parm.Add("times", norestaurant.times);
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
        /// <param name="norestaurant"></param>
        /// <returns>对象列表</returns>
        public List<noRestaurant> Select(noRestaurant norestaurant)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!norestaurant.Field.IsNullOrEmpty())
            {
                sql.Append(norestaurant.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from noRestaurant ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!norestaurant.id.IsNullOrEmpty())
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
                parm.Add("id", norestaurant.id);
            }
            if(!norestaurant.location.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("location = @location");
                    flag = false;
                }
                else
                {
                    part1.Append(" and location = @location");
                }
                parm.Add("location", norestaurant.location);
            }
            if(!norestaurant.times.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("times = @times");
                    flag = false;
                }
                else
                {
                    part1.Append(" and times = @times");
                }
                parm.Add("times", norestaurant.times);
            }

        if(!norestaurant.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(norestaurant.GroupBy).Append(" ");
            flag = false;
        }
        if(!norestaurant.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(norestaurant.OrderBy).Append(" ");
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
                var r = (List<noRestaurant>)conn.Query<noRestaurant>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<noRestaurant>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="norestaurant"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<noRestaurant> SelectByPage(noRestaurant norestaurant,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!norestaurant.Field.IsNullOrEmpty())
            {
                sql.Append(norestaurant.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from noRestaurant ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!norestaurant.id.IsNullOrEmpty())
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
                parm.Add("id", norestaurant.id);
            }
            if(!norestaurant.location.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("location = @location");
                    flag = false;
                }
                else
                {
                    part1.Append(" and location = @location");
                }
                parm.Add("location", norestaurant.location);
            }
            if(!norestaurant.times.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("times = @times");
                    flag = false;
                }
                else
                {
                    part1.Append(" and times = @times");
                }
                parm.Add("times", norestaurant.times);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from noRestaurant ");
        if(!norestaurant.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(norestaurant.GroupBy).Append(" ");
            flag = false;
        }
        if(!norestaurant.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(norestaurant.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!norestaurant.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(norestaurant.GroupBy).Append(" ");
        }
        if(!norestaurant.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(norestaurant.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<noRestaurant>)conn.Query<noRestaurant>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<noRestaurant>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<noRestaurant> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<noRestaurant>)conn.Query<noRestaurant>("Select * From noRestaurant where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<noRestaurant>();
                }
                return r;
        }
    }
    }
}
