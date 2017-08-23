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
    public partial class RestaurantOper : SingleTon<RestaurantOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Restaurant restaurant)
        {
            StringBuilder sql = new StringBuilder("insert into Restaurant ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!restaurant.name.IsNullOrEmpty())
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
                parm.Add("name", restaurant.name);
            }
            if(!restaurant.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail");
                    part2.Append("@thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(",thumbnail");
                    part2.Append(",@thumbnail");
                }
                parm.Add("thumbnail", restaurant.thumbnail);
            }
            if(!restaurant.images.IsNullOrEmpty())
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
                parm.Add("images", restaurant.images);
            }
            if(!restaurant.address.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("address");
                    part2.Append("@address");
                    flag = false;
                }
                else
                {
                    part1.Append(",address");
                    part2.Append(",@address");
                }
                parm.Add("address", restaurant.address);
            }
            if(!restaurant.phone.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("phone");
                    part2.Append("@phone");
                    flag = false;
                }
                else
                {
                    part1.Append(",phone");
                    part2.Append(",@phone");
                }
                parm.Add("phone", restaurant.phone);
            }
            if(!restaurant.businesshours.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("businesshours");
                    part2.Append("@businesshours");
                    flag = false;
                }
                else
                {
                    part1.Append(",businesshours");
                    part2.Append(",@businesshours");
                }
                parm.Add("businesshours", restaurant.businesshours);
            }
            if(!restaurant.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category");
                    part2.Append("@category");
                    flag = false;
                }
                else
                {
                    part1.Append(",category");
                    part2.Append(",@category");
                }
                parm.Add("category", restaurant.category);
            }
            if(!restaurant.coordinate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("coordinate");
                    part2.Append("@coordinate");
                    flag = false;
                }
                else
                {
                    part1.Append(",coordinate");
                    part2.Append(",@coordinate");
                }
                parm.Add("coordinate", restaurant.coordinate);
            }
            if(!restaurant.sales.IsNullOrEmpty())
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
                parm.Add("sales", restaurant.sales);
            }
            if(!restaurant.consumption.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("consumption");
                    part2.Append("@consumption");
                    flag = false;
                }
                else
                {
                    part1.Append(",consumption");
                    part2.Append(",@consumption");
                }
                parm.Add("consumption", restaurant.consumption);
            }
            if(!restaurant.discount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("discount");
                    part2.Append("@discount");
                    flag = false;
                }
                else
                {
                    part1.Append(",discount");
                    part2.Append(",@discount");
                }
                parm.Add("discount", restaurant.discount);
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
            var r = conn.Execute(@"Delete From Restaurant where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns>是否成功</returns>
        public bool Update(Restaurant restaurant)
        {
            StringBuilder sql = new StringBuilder("update Restaurant set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!restaurant.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", restaurant.id);
            }
            if(!restaurant.name.IsNullOrEmpty())
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
                parm.Add("name", restaurant.name);
            }
            if(!restaurant.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail = @thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(", thumbnail = @thumbnail");
                }
                parm.Add("thumbnail", restaurant.thumbnail);
            }
            if(!restaurant.images.IsNullOrEmpty())
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
                parm.Add("images", restaurant.images);
            }
            if(!restaurant.address.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("address = @address");
                    flag = false;
                }
                else
                {
                    part1.Append(", address = @address");
                }
                parm.Add("address", restaurant.address);
            }
            if(!restaurant.phone.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("phone = @phone");
                    flag = false;
                }
                else
                {
                    part1.Append(", phone = @phone");
                }
                parm.Add("phone", restaurant.phone);
            }
            if(!restaurant.businesshours.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("businesshours = @businesshours");
                    flag = false;
                }
                else
                {
                    part1.Append(", businesshours = @businesshours");
                }
                parm.Add("businesshours", restaurant.businesshours);
            }
            if(!restaurant.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category = @category");
                    flag = false;
                }
                else
                {
                    part1.Append(", category = @category");
                }
                parm.Add("category", restaurant.category);
            }
            if(!restaurant.coordinate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("coordinate = @coordinate");
                    flag = false;
                }
                else
                {
                    part1.Append(", coordinate = @coordinate");
                }
                parm.Add("coordinate", restaurant.coordinate);
            }
            if(!restaurant.sales.IsNullOrEmpty())
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
                parm.Add("sales", restaurant.sales);
            }
            if(!restaurant.consumption.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("consumption = @consumption");
                    flag = false;
                }
                else
                {
                    part1.Append(", consumption = @consumption");
                }
                parm.Add("consumption", restaurant.consumption);
            }
            if(!restaurant.discount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("discount = @discount");
                    flag = false;
                }
                else
                {
                    part1.Append(", discount = @discount");
                }
                parm.Add("discount", restaurant.discount);
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
        /// <param name="restaurant"></param>
        /// <returns>对象列表</returns>
        public List<Restaurant> Select(Restaurant restaurant)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!restaurant.Field.IsNullOrEmpty())
            {
                sql.Append(restaurant.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Restaurant ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!restaurant.id.IsNullOrEmpty())
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
                parm.Add("id", restaurant.id);
            }
            if(!restaurant.name.IsNullOrEmpty())
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
                parm.Add("name", restaurant.name);
            }
            if(!restaurant.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail = @thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(" and thumbnail = @thumbnail");
                }
                parm.Add("thumbnail", restaurant.thumbnail);
            }
            if(!restaurant.images.IsNullOrEmpty())
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
                parm.Add("images", restaurant.images);
            }
            if(!restaurant.address.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("address = @address");
                    flag = false;
                }
                else
                {
                    part1.Append(" and address = @address");
                }
                parm.Add("address", restaurant.address);
            }
            if(!restaurant.phone.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("phone = @phone");
                    flag = false;
                }
                else
                {
                    part1.Append(" and phone = @phone");
                }
                parm.Add("phone", restaurant.phone);
            }
            if(!restaurant.businesshours.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("businesshours = @businesshours");
                    flag = false;
                }
                else
                {
                    part1.Append(" and businesshours = @businesshours");
                }
                parm.Add("businesshours", restaurant.businesshours);
            }
            if(!restaurant.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category = @category");
                    flag = false;
                }
                else
                {
                    part1.Append(" and category = @category");
                }
                parm.Add("category", restaurant.category);
            }
            if(!restaurant.coordinate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("coordinate = @coordinate");
                    flag = false;
                }
                else
                {
                    part1.Append(" and coordinate = @coordinate");
                }
                parm.Add("coordinate", restaurant.coordinate);
            }
            if(!restaurant.sales.IsNullOrEmpty())
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
                parm.Add("sales", restaurant.sales);
            }
            if(!restaurant.consumption.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("consumption = @consumption");
                    flag = false;
                }
                else
                {
                    part1.Append(" and consumption = @consumption");
                }
                parm.Add("consumption", restaurant.consumption);
            }
            if(!restaurant.discount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("discount = @discount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and discount = @discount");
                }
                parm.Add("discount", restaurant.discount);
            }

        if(!restaurant.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(restaurant.GroupBy).Append(" ");
            flag = false;
        }
        if(!restaurant.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(restaurant.OrderBy).Append(" ");
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
                var r = (List<Restaurant>)conn.Query<Restaurant>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Restaurant>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="restaurant"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Restaurant> SelectByPage(Restaurant restaurant,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!restaurant.Field.IsNullOrEmpty())
            {
                sql.Append(restaurant.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Restaurant ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!restaurant.id.IsNullOrEmpty())
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
                parm.Add("id", restaurant.id);
            }
            if(!restaurant.name.IsNullOrEmpty())
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
                parm.Add("name", restaurant.name);
            }
            if(!restaurant.thumbnail.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("thumbnail = @thumbnail");
                    flag = false;
                }
                else
                {
                    part1.Append(" and thumbnail = @thumbnail");
                }
                parm.Add("thumbnail", restaurant.thumbnail);
            }
            if(!restaurant.images.IsNullOrEmpty())
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
                parm.Add("images", restaurant.images);
            }
            if(!restaurant.address.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("address = @address");
                    flag = false;
                }
                else
                {
                    part1.Append(" and address = @address");
                }
                parm.Add("address", restaurant.address);
            }
            if(!restaurant.phone.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("phone = @phone");
                    flag = false;
                }
                else
                {
                    part1.Append(" and phone = @phone");
                }
                parm.Add("phone", restaurant.phone);
            }
            if(!restaurant.businesshours.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("businesshours = @businesshours");
                    flag = false;
                }
                else
                {
                    part1.Append(" and businesshours = @businesshours");
                }
                parm.Add("businesshours", restaurant.businesshours);
            }
            if(!restaurant.category.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("category = @category");
                    flag = false;
                }
                else
                {
                    part1.Append(" and category = @category");
                }
                parm.Add("category", restaurant.category);
            }
            if(!restaurant.coordinate.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("coordinate = @coordinate");
                    flag = false;
                }
                else
                {
                    part1.Append(" and coordinate = @coordinate");
                }
                parm.Add("coordinate", restaurant.coordinate);
            }
            if(!restaurant.sales.IsNullOrEmpty())
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
                parm.Add("sales", restaurant.sales);
            }
            if(!restaurant.consumption.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("consumption = @consumption");
                    flag = false;
                }
                else
                {
                    part1.Append(" and consumption = @consumption");
                }
                parm.Add("consumption", restaurant.consumption);
            }
            if(!restaurant.discount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("discount = @discount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and discount = @discount");
                }
                parm.Add("discount", restaurant.discount);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Restaurant ");
        if(!restaurant.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(restaurant.GroupBy).Append(" ");
            flag = false;
        }
        if(!restaurant.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(restaurant.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!restaurant.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(restaurant.GroupBy).Append(" ");
        }
        if(!restaurant.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(restaurant.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Restaurant>)conn.Query<Restaurant>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Restaurant>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Restaurant> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Restaurant>)conn.Query<Restaurant>("Select * From Restaurant where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Restaurant>();
                }
                return r;
        }
    }
    }
}
