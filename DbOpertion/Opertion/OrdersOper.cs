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
    public partial class OrdersOper : SingleTon<OrdersOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Orders orders)
        {
            StringBuilder sql = new StringBuilder("insert into Orders ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!orders.SellerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SellerId");
                    part2.Append("@SellerId");
                    flag = false;
                }
                else
                {
                    part1.Append(",SellerId");
                    part2.Append(",@SellerId");
                }
                parm.Add("SellerId", orders.SellerId);
            }
            if(!orders.CustomerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CustomerId");
                    part2.Append("@CustomerId");
                    flag = false;
                }
                else
                {
                    part1.Append(",CustomerId");
                    part2.Append(",@CustomerId");
                }
                parm.Add("CustomerId", orders.CustomerId);
            }
            if(!orders.RecipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipeId");
                    part2.Append("@RecipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(",RecipeId");
                    part2.Append(",@RecipeId");
                }
                parm.Add("RecipeId", orders.RecipeId);
            }
            if(!orders.CreateTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CreateTime");
                    part2.Append("@CreateTime");
                    flag = false;
                }
                else
                {
                    part1.Append(",CreateTime");
                    part2.Append(",@CreateTime");
                }
                parm.Add("CreateTime", orders.CreateTime);
            }
            if(!orders.RecipePrice.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipePrice");
                    part2.Append("@RecipePrice");
                    flag = false;
                }
                else
                {
                    part1.Append(",RecipePrice");
                    part2.Append(",@RecipePrice");
                }
                parm.Add("RecipePrice", orders.RecipePrice);
            }
            if(!orders.Pay.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Pay");
                    part2.Append("@Pay");
                    flag = false;
                }
                else
                {
                    part1.Append(",Pay");
                    part2.Append(",@Pay");
                }
                parm.Add("Pay", orders.Pay);
            }
            if(!orders.PayType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("PayType");
                    part2.Append("@PayType");
                    flag = false;
                }
                else
                {
                    part1.Append(",PayType");
                    part2.Append(",@PayType");
                }
                parm.Add("PayType", orders.PayType);
            }
            if(!orders.ShopTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ShopTime");
                    part2.Append("@ShopTime");
                    flag = false;
                }
                else
                {
                    part1.Append(",ShopTime");
                    part2.Append(",@ShopTime");
                }
                parm.Add("ShopTime", orders.ShopTime);
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
            object parm = new { Id = id };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
            var r = conn.Execute(@"Delete From Orders where Id=@Id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>是否成功</returns>
        public bool Update(Orders orders)
        {
            StringBuilder sql = new StringBuilder("update Orders set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!orders.Id.IsNullOrEmpty())
            {
                part2.Append("Id = @Id");
                parm.Add("Id", orders.Id);
            }
            if(!orders.SellerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SellerId = @SellerId");
                    flag = false;
                }
                else
                {
                    part1.Append(", SellerId = @SellerId");
                }
                parm.Add("SellerId", orders.SellerId);
            }
            if(!orders.CustomerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CustomerId = @CustomerId");
                    flag = false;
                }
                else
                {
                    part1.Append(", CustomerId = @CustomerId");
                }
                parm.Add("CustomerId", orders.CustomerId);
            }
            if(!orders.RecipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipeId = @RecipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(", RecipeId = @RecipeId");
                }
                parm.Add("RecipeId", orders.RecipeId);
            }
            if(!orders.CreateTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CreateTime = @CreateTime");
                    flag = false;
                }
                else
                {
                    part1.Append(", CreateTime = @CreateTime");
                }
                parm.Add("CreateTime", orders.CreateTime);
            }
            if(!orders.RecipePrice.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipePrice = @RecipePrice");
                    flag = false;
                }
                else
                {
                    part1.Append(", RecipePrice = @RecipePrice");
                }
                parm.Add("RecipePrice", orders.RecipePrice);
            }
            if(!orders.Pay.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Pay = @Pay");
                    flag = false;
                }
                else
                {
                    part1.Append(", Pay = @Pay");
                }
                parm.Add("Pay", orders.Pay);
            }
            if(!orders.PayType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("PayType = @PayType");
                    flag = false;
                }
                else
                {
                    part1.Append(", PayType = @PayType");
                }
                parm.Add("PayType", orders.PayType);
            }
            if(!orders.ShopTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ShopTime = @ShopTime");
                    flag = false;
                }
                else
                {
                    part1.Append(", ShopTime = @ShopTime");
                }
                parm.Add("ShopTime", orders.ShopTime);
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
        /// <param name="orders"></param>
        /// <returns>对象列表</returns>
        public List<Orders> Select(Orders orders)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!orders.Field.IsNullOrEmpty())
            {
                sql.Append(orders.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Orders ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!orders.Id.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Id = @Id");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Id = @Id");
                }
                parm.Add("Id", orders.Id);
            }
            if(!orders.SellerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SellerId = @SellerId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SellerId = @SellerId");
                }
                parm.Add("SellerId", orders.SellerId);
            }
            if(!orders.CustomerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CustomerId = @CustomerId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and CustomerId = @CustomerId");
                }
                parm.Add("CustomerId", orders.CustomerId);
            }
            if(!orders.RecipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipeId = @RecipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and RecipeId = @RecipeId");
                }
                parm.Add("RecipeId", orders.RecipeId);
            }
            if(!orders.CreateTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CreateTime = @CreateTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and CreateTime = @CreateTime");
                }
                parm.Add("CreateTime", orders.CreateTime);
            }
            if(!orders.RecipePrice.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipePrice = @RecipePrice");
                    flag = false;
                }
                else
                {
                    part1.Append(" and RecipePrice = @RecipePrice");
                }
                parm.Add("RecipePrice", orders.RecipePrice);
            }
            if(!orders.Pay.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Pay = @Pay");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Pay = @Pay");
                }
                parm.Add("Pay", orders.Pay);
            }
            if(!orders.PayType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("PayType = @PayType");
                    flag = false;
                }
                else
                {
                    part1.Append(" and PayType = @PayType");
                }
                parm.Add("PayType", orders.PayType);
            }
            if(!orders.ShopTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ShopTime = @ShopTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ShopTime = @ShopTime");
                }
                parm.Add("ShopTime", orders.ShopTime);
            }

        if(!orders.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(orders.GroupBy).Append(" ");
            flag = false;
        }
        if(!orders.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(orders.OrderBy).Append(" ");
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
                var r = (List<Orders>)conn.Query<Orders>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Orders>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Orders> SelectByPage(Orders orders,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!orders.Field.IsNullOrEmpty())
            {
                sql.Append(orders.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Orders ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!orders.Id.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Id = @Id");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Id = @Id");
                }
                parm.Add("Id", orders.Id);
            }
            if(!orders.SellerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SellerId = @SellerId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SellerId = @SellerId");
                }
                parm.Add("SellerId", orders.SellerId);
            }
            if(!orders.CustomerId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CustomerId = @CustomerId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and CustomerId = @CustomerId");
                }
                parm.Add("CustomerId", orders.CustomerId);
            }
            if(!orders.RecipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipeId = @RecipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and RecipeId = @RecipeId");
                }
                parm.Add("RecipeId", orders.RecipeId);
            }
            if(!orders.CreateTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("CreateTime = @CreateTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and CreateTime = @CreateTime");
                }
                parm.Add("CreateTime", orders.CreateTime);
            }
            if(!orders.RecipePrice.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("RecipePrice = @RecipePrice");
                    flag = false;
                }
                else
                {
                    part1.Append(" and RecipePrice = @RecipePrice");
                }
                parm.Add("RecipePrice", orders.RecipePrice);
            }
            if(!orders.Pay.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("Pay = @Pay");
                    flag = false;
                }
                else
                {
                    part1.Append(" and Pay = @Pay");
                }
                parm.Add("Pay", orders.Pay);
            }
            if(!orders.PayType.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("PayType = @PayType");
                    flag = false;
                }
                else
                {
                    part1.Append(" and PayType = @PayType");
                }
                parm.Add("PayType", orders.PayType);
            }
            if(!orders.ShopTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("ShopTime = @ShopTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and ShopTime = @ShopTime");
                }
                parm.Add("ShopTime", orders.ShopTime);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" Id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" Id from Orders ");
        if(!orders.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(orders.GroupBy).Append(" ");
            flag = false;
        }
        if(!orders.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(orders.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!orders.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(orders.GroupBy).Append(" ");
        }
        if(!orders.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(orders.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Orders>)conn.Query<Orders>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Orders>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Orders> SelectByIds(List<string> List_Id)
        {
            object parm = new { Id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Orders>)conn.Query<Orders>("Select * From Orders where Id in @Id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Orders>();
                }
                return r;
        }
    }
    }
}
