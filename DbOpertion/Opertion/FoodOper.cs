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
    public partial class FoodOper : SingleTon<FoodOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="food"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Food food)
        {
            StringBuilder sql = new StringBuilder("insert into Food ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!food.name.IsNullOrEmpty())
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
                parm.Add("name", food.name);
            }
            if(!food.isDeleted.IsNullOrEmpty())
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
                parm.Add("isDeleted", food.isDeleted);
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
            var r = conn.Execute(@"Delete From Food where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="food"></param>
        /// <returns>是否成功</returns>
        public bool Update(Food food)
        {
            StringBuilder sql = new StringBuilder("update Food set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!food.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", food.id);
            }
            if(!food.name.IsNullOrEmpty())
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
                parm.Add("name", food.name);
            }
            if(!food.isDeleted.IsNullOrEmpty())
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
                parm.Add("isDeleted", food.isDeleted);
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
        /// <param name="food"></param>
        /// <returns>对象列表</returns>
        public List<Food> Select(Food food)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!food.Field.IsNullOrEmpty())
            {
                sql.Append(food.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Food ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!food.id.IsNullOrEmpty())
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
                parm.Add("id", food.id);
            }
            if(!food.name.IsNullOrEmpty())
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
                parm.Add("name", food.name);
            }
            if(!food.isDeleted.IsNullOrEmpty())
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
                parm.Add("isDeleted", food.isDeleted);
            }

        if(!food.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(food.GroupBy).Append(" ");
            flag = false;
        }
        if(!food.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(food.OrderBy).Append(" ");
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
                var r = (List<Food>)conn.Query<Food>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Food>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="food"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Food> SelectByPage(Food food,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!food.Field.IsNullOrEmpty())
            {
                sql.Append(food.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Food ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!food.id.IsNullOrEmpty())
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
                parm.Add("id", food.id);
            }
            if(!food.name.IsNullOrEmpty())
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
                parm.Add("name", food.name);
            }
            if(!food.isDeleted.IsNullOrEmpty())
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
                parm.Add("isDeleted", food.isDeleted);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Food ");
        if(!food.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(food.GroupBy).Append(" ");
            flag = false;
        }
        if(!food.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(food.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!food.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(food.GroupBy).Append(" ");
        }
        if(!food.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(food.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Food>)conn.Query<Food>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Food>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Food> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Food>)conn.Query<Food>("Select * From Food where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Food>();
                }
                return r;
        }
    }
    }
}
