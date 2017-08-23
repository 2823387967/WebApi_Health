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
    public partial class Recipe_foodsOper : SingleTon<Recipe_foodsOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="recipe_foods"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Recipe_foods recipe_foods)
        {
            StringBuilder sql = new StringBuilder("insert into Recipe_foods ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe_foods.recipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeId");
                    part2.Append("@recipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(",recipeId");
                    part2.Append(",@recipeId");
                }
                parm.Add("recipeId", recipe_foods.recipeId);
            }
            if(!recipe_foods.foodtypeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypeId");
                    part2.Append("@foodtypeId");
                    flag = false;
                }
                else
                {
                    part1.Append(",foodtypeId");
                    part2.Append(",@foodtypeId");
                }
                parm.Add("foodtypeId", recipe_foods.foodtypeId);
            }
            if(!recipe_foods.foodId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodId");
                    part2.Append("@foodId");
                    flag = false;
                }
                else
                {
                    part1.Append(",foodId");
                    part2.Append(",@foodId");
                }
                parm.Add("foodId", recipe_foods.foodId);
            }
            if(!recipe_foods.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight");
                    part2.Append("@weight");
                    flag = false;
                }
                else
                {
                    part1.Append(",weight");
                    part2.Append(",@weight");
                }
                parm.Add("weight", recipe_foods.weight);
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
            var r = conn.Execute(@"Delete From Recipe_foods where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="recipe_foods"></param>
        /// <returns>是否成功</returns>
        public bool Update(Recipe_foods recipe_foods)
        {
            StringBuilder sql = new StringBuilder("update Recipe_foods set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe_foods.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", recipe_foods.id);
            }
            if(!recipe_foods.recipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeId = @recipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(", recipeId = @recipeId");
                }
                parm.Add("recipeId", recipe_foods.recipeId);
            }
            if(!recipe_foods.foodtypeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypeId = @foodtypeId");
                    flag = false;
                }
                else
                {
                    part1.Append(", foodtypeId = @foodtypeId");
                }
                parm.Add("foodtypeId", recipe_foods.foodtypeId);
            }
            if(!recipe_foods.foodId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodId = @foodId");
                    flag = false;
                }
                else
                {
                    part1.Append(", foodId = @foodId");
                }
                parm.Add("foodId", recipe_foods.foodId);
            }
            if(!recipe_foods.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight = @weight");
                    flag = false;
                }
                else
                {
                    part1.Append(", weight = @weight");
                }
                parm.Add("weight", recipe_foods.weight);
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
        /// <param name="recipe_foods"></param>
        /// <returns>对象列表</returns>
        public List<Recipe_foods> Select(Recipe_foods recipe_foods)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!recipe_foods.Field.IsNullOrEmpty())
            {
                sql.Append(recipe_foods.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Recipe_foods ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe_foods.id.IsNullOrEmpty())
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
                parm.Add("id", recipe_foods.id);
            }
            if(!recipe_foods.recipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeId = @recipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and recipeId = @recipeId");
                }
                parm.Add("recipeId", recipe_foods.recipeId);
            }
            if(!recipe_foods.foodtypeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypeId = @foodtypeId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foodtypeId = @foodtypeId");
                }
                parm.Add("foodtypeId", recipe_foods.foodtypeId);
            }
            if(!recipe_foods.foodId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodId = @foodId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foodId = @foodId");
                }
                parm.Add("foodId", recipe_foods.foodId);
            }
            if(!recipe_foods.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight = @weight");
                    flag = false;
                }
                else
                {
                    part1.Append(" and weight = @weight");
                }
                parm.Add("weight", recipe_foods.weight);
            }

        if(!recipe_foods.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(recipe_foods.GroupBy).Append(" ");
            flag = false;
        }
        if(!recipe_foods.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(recipe_foods.OrderBy).Append(" ");
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
                var r = (List<Recipe_foods>)conn.Query<Recipe_foods>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Recipe_foods>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="recipe_foods"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Recipe_foods> SelectByPage(Recipe_foods recipe_foods,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!recipe_foods.Field.IsNullOrEmpty())
            {
                sql.Append(recipe_foods.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Recipe_foods ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe_foods.id.IsNullOrEmpty())
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
                parm.Add("id", recipe_foods.id);
            }
            if(!recipe_foods.recipeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("recipeId = @recipeId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and recipeId = @recipeId");
                }
                parm.Add("recipeId", recipe_foods.recipeId);
            }
            if(!recipe_foods.foodtypeId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypeId = @foodtypeId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foodtypeId = @foodtypeId");
                }
                parm.Add("foodtypeId", recipe_foods.foodtypeId);
            }
            if(!recipe_foods.foodId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodId = @foodId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foodId = @foodId");
                }
                parm.Add("foodId", recipe_foods.foodId);
            }
            if(!recipe_foods.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight = @weight");
                    flag = false;
                }
                else
                {
                    part1.Append(" and weight = @weight");
                }
                parm.Add("weight", recipe_foods.weight);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Recipe_foods ");
        if(!recipe_foods.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(recipe_foods.GroupBy).Append(" ");
            flag = false;
        }
        if(!recipe_foods.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(recipe_foods.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!recipe_foods.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(recipe_foods.GroupBy).Append(" ");
        }
        if(!recipe_foods.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(recipe_foods.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Recipe_foods>)conn.Query<Recipe_foods>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Recipe_foods>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Recipe_foods> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Recipe_foods>)conn.Query<Recipe_foods>("Select * From Recipe_foods where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Recipe_foods>();
                }
                return r;
        }
    }
    }
}
