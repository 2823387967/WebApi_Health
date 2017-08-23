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
        /// <summary>
        /// 模糊查找
        /// <summary>
        /// <param name="restaurant"></param>
        /// <returns>是否成功</returns>
        public List<Restaurant> SelectVagueByRestaurantName(Restaurant restaurant)
        {
            StringBuilder sql = new StringBuilder("Select * from Restaurant");
            var parm = new DynamicParameters();
            if (!restaurant.name.IsNullOrEmpty())
            {
                sql.Append(" where name like @name");
                parm.Add("name", "%" + restaurant.name + "%");
            }
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Restaurant>)conn.Query<Restaurant>(sql.ToString(), parm);
                conn.Close();
                return r;
            }
        }
    }
}