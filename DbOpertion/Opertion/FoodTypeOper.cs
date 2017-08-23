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
    public partial class FoodTypeOper : SingleTon<FoodTypeOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="foodtype"></param>
        /// <returns>是否成功</returns>
        public bool Insert(FoodType foodtype)
        {
            StringBuilder sql = new StringBuilder("insert into FoodType ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!foodtype.name.IsNullOrEmpty())
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
                parm.Add("name", foodtype.name);
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
            var r = conn.Execute(@"Delete From FoodType where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="foodtype"></param>
        /// <returns>是否成功</returns>
        public bool Update(FoodType foodtype)
        {
            StringBuilder sql = new StringBuilder("update FoodType set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!foodtype.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", foodtype.id);
            }
            if(!foodtype.name.IsNullOrEmpty())
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
                parm.Add("name", foodtype.name);
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
        /// <param name="foodtype"></param>
        /// <returns>对象列表</returns>
        public List<FoodType> Select(FoodType foodtype)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!foodtype.Field.IsNullOrEmpty())
            {
                sql.Append(foodtype.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from FoodType ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!foodtype.id.IsNullOrEmpty())
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
                parm.Add("id", foodtype.id);
            }
            if(!foodtype.name.IsNullOrEmpty())
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
                parm.Add("name", foodtype.name);
            }

        if(!foodtype.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(foodtype.GroupBy).Append(" ");
            flag = false;
        }
        if(!foodtype.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(foodtype.OrderBy).Append(" ");
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
                var r = (List<FoodType>)conn.Query<FoodType>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<FoodType>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="foodtype"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<FoodType> SelectByPage(FoodType foodtype,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!foodtype.Field.IsNullOrEmpty())
            {
                sql.Append(foodtype.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from FoodType ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!foodtype.id.IsNullOrEmpty())
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
                parm.Add("id", foodtype.id);
            }
            if(!foodtype.name.IsNullOrEmpty())
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
                parm.Add("name", foodtype.name);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from FoodType ");
        if(!foodtype.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(foodtype.GroupBy).Append(" ");
            flag = false;
        }
        if(!foodtype.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(foodtype.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!foodtype.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(foodtype.GroupBy).Append(" ");
        }
        if(!foodtype.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(foodtype.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<FoodType>)conn.Query<FoodType>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<FoodType>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<FoodType> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<FoodType>)conn.Query<FoodType>("Select * From FoodType where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<FoodType>();
                }
                return r;
        }
    }
    }
}
