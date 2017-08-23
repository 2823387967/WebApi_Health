using Common.Extend;
using Dapper;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.DBoperation
{
    public partial class RecipeOper : SingleTon<RecipeOper>
    {
        /// <summary>
        /// 模糊查找
        /// <summary>
        /// <param name="restaurant"></param>
        /// <returns>是否成功</returns>
        public List<Recipe> SelectVagueByRecipeName(Recipe recipe)
        {
            StringBuilder sql = new StringBuilder("Select * from Recipe");
            var parm = new DynamicParameters();
            if (!recipe.name.IsNullOrEmpty())
            {
                sql.Append(" where name like @name");
                parm.Add("name", "%" + recipe.name + "%");
            }
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Recipe>)conn.Query<Recipe>(sql.ToString(), parm);
                conn.Close();
                return r;
            }
        }
    }
}
