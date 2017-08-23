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
    public partial class RecipeOper : SingleTon<RecipeOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Recipe recipe)
        {
            StringBuilder sql = new StringBuilder("insert into Recipe ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe.name.IsNullOrEmpty())
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
                parm.Add("name", recipe.name);
            }
            if(!recipe.available.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("available");
                    part2.Append("@available");
                    flag = false;
                }
                else
                {
                    part1.Append(",available");
                    part2.Append(",@available");
                }
                parm.Add("available", recipe.available);
            }
            if(!recipe.foodtypes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypes");
                    part2.Append("@foodtypes");
                    flag = false;
                }
                else
                {
                    part1.Append(",foodtypes");
                    part2.Append(",@foodtypes");
                }
                parm.Add("foodtypes", recipe.foodtypes);
            }
            if(!recipe.foods.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foods");
                    part2.Append("@foods");
                    flag = false;
                }
                else
                {
                    part1.Append(",foods");
                    part2.Append(",@foods");
                }
                parm.Add("foods", recipe.foods);
            }
            if(!recipe.restaurantId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantId");
                    part2.Append("@restaurantId");
                    flag = false;
                }
                else
                {
                    part1.Append(",restaurantId");
                    part2.Append(",@restaurantId");
                }
                parm.Add("restaurantId", recipe.restaurantId);
            }
            if(!recipe.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags");
                    part2.Append("@tags");
                    flag = false;
                }
                else
                {
                    part1.Append(",tags");
                    part2.Append(",@tags");
                }
                parm.Add("tags", recipe.tags);
            }
            if(!recipe.images.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("images");
                    part2.Append("@images");
                    flag = false;
                }
                else
                {
                    part1.Append(",images");
                    part2.Append(",@images");
                }
                parm.Add("images", recipe.images);
            }
            if(!recipe.sales.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sales");
                    part2.Append("@sales");
                    flag = false;
                }
                else
                {
                    part1.Append(",sales");
                    part2.Append(",@sales");
                }
                parm.Add("sales", recipe.sales);
            }
            if(!recipe.price.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("price");
                    part2.Append("@price");
                    flag = false;
                }
                else
                {
                    part1.Append(",price");
                    part2.Append(",@price");
                }
                parm.Add("price", recipe.price);
            }
            if(!recipe.createTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("createTime");
                    part2.Append("@createTime");
                    flag = false;
                }
                else
                {
                    part1.Append(",createTime");
                    part2.Append(",@createTime");
                }
                parm.Add("createTime", recipe.createTime);
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
            var r = conn.Execute(@"Delete From Recipe where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns>是否成功</returns>
        public bool Update(Recipe recipe)
        {
            StringBuilder sql = new StringBuilder("update Recipe set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", recipe.id);
            }
            if(!recipe.name.IsNullOrEmpty())
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
                parm.Add("name", recipe.name);
            }
            if(!recipe.available.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("available = @available");
                    flag = false;
                }
                else
                {
                    part1.Append(", available = @available");
                }
                parm.Add("available", recipe.available);
            }
            if(!recipe.foodtypes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypes = @foodtypes");
                    flag = false;
                }
                else
                {
                    part1.Append(", foodtypes = @foodtypes");
                }
                parm.Add("foodtypes", recipe.foodtypes);
            }
            if(!recipe.foods.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foods = @foods");
                    flag = false;
                }
                else
                {
                    part1.Append(", foods = @foods");
                }
                parm.Add("foods", recipe.foods);
            }
            if(!recipe.restaurantId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantId = @restaurantId");
                    flag = false;
                }
                else
                {
                    part1.Append(", restaurantId = @restaurantId");
                }
                parm.Add("restaurantId", recipe.restaurantId);
            }
            if(!recipe.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags = @tags");
                    flag = false;
                }
                else
                {
                    part1.Append(", tags = @tags");
                }
                parm.Add("tags", recipe.tags);
            }
            if(!recipe.images.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("images = @images");
                    flag = false;
                }
                else
                {
                    part1.Append(", images = @images");
                }
                parm.Add("images", recipe.images);
            }
            if(!recipe.sales.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sales = @sales");
                    flag = false;
                }
                else
                {
                    part1.Append(", sales = @sales");
                }
                parm.Add("sales", recipe.sales);
            }
            if(!recipe.price.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("price = @price");
                    flag = false;
                }
                else
                {
                    part1.Append(", price = @price");
                }
                parm.Add("price", recipe.price);
            }
            if(!recipe.createTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("createTime = @createTime");
                    flag = false;
                }
                else
                {
                    part1.Append(", createTime = @createTime");
                }
                parm.Add("createTime", recipe.createTime);
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
        /// <param name="recipe"></param>
        /// <returns>对象列表</returns>
        public List<Recipe> Select(Recipe recipe)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!recipe.Field.IsNullOrEmpty())
            {
                sql.Append(recipe.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Recipe ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe.id.IsNullOrEmpty())
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
                parm.Add("id", recipe.id);
            }
            if(!recipe.name.IsNullOrEmpty())
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
                parm.Add("name", recipe.name);
            }
            if(!recipe.available.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("available = @available");
                    flag = false;
                }
                else
                {
                    part1.Append(" and available = @available");
                }
                parm.Add("available", recipe.available);
            }
            if(!recipe.foodtypes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypes = @foodtypes");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foodtypes = @foodtypes");
                }
                parm.Add("foodtypes", recipe.foodtypes);
            }
            if(!recipe.foods.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foods = @foods");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foods = @foods");
                }
                parm.Add("foods", recipe.foods);
            }
            if(!recipe.restaurantId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantId = @restaurantId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and restaurantId = @restaurantId");
                }
                parm.Add("restaurantId", recipe.restaurantId);
            }
            if(!recipe.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags = @tags");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tags = @tags");
                }
                parm.Add("tags", recipe.tags);
            }
            if(!recipe.images.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("images = @images");
                    flag = false;
                }
                else
                {
                    part1.Append(" and images = @images");
                }
                parm.Add("images", recipe.images);
            }
            if(!recipe.sales.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sales = @sales");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sales = @sales");
                }
                parm.Add("sales", recipe.sales);
            }
            if(!recipe.price.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("price = @price");
                    flag = false;
                }
                else
                {
                    part1.Append(" and price = @price");
                }
                parm.Add("price", recipe.price);
            }
            if(!recipe.createTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("createTime = @createTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and createTime = @createTime");
                }
                parm.Add("createTime", recipe.createTime);
            }

        if(!recipe.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(recipe.GroupBy).Append(" ");
            flag = false;
        }
        if(!recipe.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(recipe.OrderBy).Append(" ");
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
                var r = (List<Recipe>)conn.Query<Recipe>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Recipe>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Recipe> SelectByPage(Recipe recipe,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!recipe.Field.IsNullOrEmpty())
            {
                sql.Append(recipe.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Recipe ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!recipe.id.IsNullOrEmpty())
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
                parm.Add("id", recipe.id);
            }
            if(!recipe.name.IsNullOrEmpty())
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
                parm.Add("name", recipe.name);
            }
            if(!recipe.available.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("available = @available");
                    flag = false;
                }
                else
                {
                    part1.Append(" and available = @available");
                }
                parm.Add("available", recipe.available);
            }
            if(!recipe.foodtypes.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foodtypes = @foodtypes");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foodtypes = @foodtypes");
                }
                parm.Add("foodtypes", recipe.foodtypes);
            }
            if(!recipe.foods.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("foods = @foods");
                    flag = false;
                }
                else
                {
                    part1.Append(" and foods = @foods");
                }
                parm.Add("foods", recipe.foods);
            }
            if(!recipe.restaurantId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("restaurantId = @restaurantId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and restaurantId = @restaurantId");
                }
                parm.Add("restaurantId", recipe.restaurantId);
            }
            if(!recipe.tags.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("tags = @tags");
                    flag = false;
                }
                else
                {
                    part1.Append(" and tags = @tags");
                }
                parm.Add("tags", recipe.tags);
            }
            if(!recipe.images.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("images = @images");
                    flag = false;
                }
                else
                {
                    part1.Append(" and images = @images");
                }
                parm.Add("images", recipe.images);
            }
            if(!recipe.sales.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sales = @sales");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sales = @sales");
                }
                parm.Add("sales", recipe.sales);
            }
            if(!recipe.price.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("price = @price");
                    flag = false;
                }
                else
                {
                    part1.Append(" and price = @price");
                }
                parm.Add("price", recipe.price);
            }
            if(!recipe.createTime.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("createTime = @createTime");
                    flag = false;
                }
                else
                {
                    part1.Append(" and createTime = @createTime");
                }
                parm.Add("createTime", recipe.createTime);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Recipe ");
        if(!recipe.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(recipe.GroupBy).Append(" ");
            flag = false;
        }
        if(!recipe.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(recipe.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!recipe.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(recipe.GroupBy).Append(" ");
        }
        if(!recipe.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(recipe.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Recipe>)conn.Query<Recipe>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Recipe>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Recipe> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Recipe>)conn.Query<Recipe>("Select * From Recipe where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Recipe>();
                }
                return r;
        }
    }
    }
}
