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
    public partial class SellerOper : SingleTon<SellerOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="seller"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Seller seller)
        {
            StringBuilder sql = new StringBuilder("insert into Seller ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!seller.name.IsNullOrEmpty())
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
                parm.Add("name", seller.name);
            }
            if(!seller.loginname.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loginname");
                    part2.Append("@loginname");
                    flag = false;
                }
                else
                {
                    part1.Append(",loginname");
                    part2.Append(",@loginname");
                }
                parm.Add("loginname", seller.loginname);
            }
            if(!seller.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password");
                    part2.Append("@password");
                    flag = false;
                }
                else
                {
                    part1.Append(",password");
                    part2.Append(",@password");
                }
                parm.Add("password", seller.password);
            }
            if(!seller.restaurantid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantid");
                    part2.Append("@restaurantid");
                    flag = false;
                }
                else
                {
                    part1.Append(",restaurantid");
                    part2.Append(",@restaurantid");
                }
                parm.Add("restaurantid", seller.restaurantid);
            }
            if(!seller.recipeids.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeids");
                    part2.Append("@recipeids");
                    flag = false;
                }
                else
                {
                    part1.Append(",recipeids");
                    part2.Append(",@recipeids");
                }
                parm.Add("recipeids", seller.recipeids);
            }
            if(!seller.balance.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("balance");
                    part2.Append("@balance");
                    flag = false;
                }
                else
                {
                    part1.Append(",balance");
                    part2.Append(",@balance");
                }
                parm.Add("balance", seller.balance);
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
            var r = conn.Execute(@"Delete From Seller where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="seller"></param>
        /// <returns>是否成功</returns>
        public bool Update(Seller seller)
        {
            StringBuilder sql = new StringBuilder("update Seller set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!seller.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", seller.id);
            }
            if(!seller.name.IsNullOrEmpty())
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
                parm.Add("name", seller.name);
            }
            if(!seller.loginname.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loginname = @loginname");
                    flag = false;
                }
                else
                {
                    part1.Append(", loginname = @loginname");
                }
                parm.Add("loginname", seller.loginname);
            }
            if(!seller.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password = @password");
                    flag = false;
                }
                else
                {
                    part1.Append(", password = @password");
                }
                parm.Add("password", seller.password);
            }
            if(!seller.restaurantid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantid = @restaurantid");
                    flag = false;
                }
                else
                {
                    part1.Append(", restaurantid = @restaurantid");
                }
                parm.Add("restaurantid", seller.restaurantid);
            }
            if(!seller.recipeids.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeids = @recipeids");
                    flag = false;
                }
                else
                {
                    part1.Append(", recipeids = @recipeids");
                }
                parm.Add("recipeids", seller.recipeids);
            }
            if(!seller.balance.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("balance = @balance");
                    flag = false;
                }
                else
                {
                    part1.Append(", balance = @balance");
                }
                parm.Add("balance", seller.balance);
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
        /// <param name="seller"></param>
        /// <returns>对象列表</returns>
        public List<Seller> Select(Seller seller)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!seller.Field.IsNullOrEmpty())
            {
                sql.Append(seller.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Seller ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!seller.id.IsNullOrEmpty())
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
                parm.Add("id", seller.id);
            }
            if(!seller.name.IsNullOrEmpty())
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
                parm.Add("name", seller.name);
            }
            if(!seller.loginname.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loginname = @loginname");
                    flag = false;
                }
                else
                {
                    part1.Append(" and loginname = @loginname");
                }
                parm.Add("loginname", seller.loginname);
            }
            if(!seller.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password = @password");
                    flag = false;
                }
                else
                {
                    part1.Append(" and password = @password");
                }
                parm.Add("password", seller.password);
            }
            if(!seller.restaurantid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantid = @restaurantid");
                    flag = false;
                }
                else
                {
                    part1.Append(" and restaurantid = @restaurantid");
                }
                parm.Add("restaurantid", seller.restaurantid);
            }
            if(!seller.recipeids.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeids = @recipeids");
                    flag = false;
                }
                else
                {
                    part1.Append(" and recipeids = @recipeids");
                }
                parm.Add("recipeids", seller.recipeids);
            }
            if(!seller.balance.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("balance = @balance");
                    flag = false;
                }
                else
                {
                    part1.Append(" and balance = @balance");
                }
                parm.Add("balance", seller.balance);
            }

        if(!seller.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(seller.GroupBy).Append(" ");
            flag = false;
        }
        if(!seller.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(seller.OrderBy).Append(" ");
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
                var r = (List<Seller>)conn.Query<Seller>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Seller>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="seller"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Seller> SelectByPage(Seller seller,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!seller.Field.IsNullOrEmpty())
            {
                sql.Append(seller.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Seller ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!seller.id.IsNullOrEmpty())
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
                parm.Add("id", seller.id);
            }
            if(!seller.name.IsNullOrEmpty())
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
                parm.Add("name", seller.name);
            }
            if(!seller.loginname.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("loginname = @loginname");
                    flag = false;
                }
                else
                {
                    part1.Append(" and loginname = @loginname");
                }
                parm.Add("loginname", seller.loginname);
            }
            if(!seller.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password = @password");
                    flag = false;
                }
                else
                {
                    part1.Append(" and password = @password");
                }
                parm.Add("password", seller.password);
            }
            if(!seller.restaurantid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantid = @restaurantid");
                    flag = false;
                }
                else
                {
                    part1.Append(" and restaurantid = @restaurantid");
                }
                parm.Add("restaurantid", seller.restaurantid);
            }
            if(!seller.recipeids.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeids = @recipeids");
                    flag = false;
                }
                else
                {
                    part1.Append(" and recipeids = @recipeids");
                }
                parm.Add("recipeids", seller.recipeids);
            }
            if(!seller.balance.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("balance = @balance");
                    flag = false;
                }
                else
                {
                    part1.Append(" and balance = @balance");
                }
                parm.Add("balance", seller.balance);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Seller ");
        if(!seller.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(seller.GroupBy).Append(" ");
            flag = false;
        }
        if(!seller.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(seller.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!seller.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(seller.GroupBy).Append(" ");
        }
        if(!seller.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(seller.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Seller>)conn.Query<Seller>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Seller>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Seller> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Seller>)conn.Query<Seller>("Select * From Seller where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Seller>();
                }
                return r;
        }
    }
    }
}
