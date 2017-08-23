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
    public partial class ArticleOper : SingleTon<ArticleOper>
    {
        /// <summary>
        /// 模糊查找
        /// <summary>
        /// <param name="restaurant"></param>
        /// <returns>是否成功</returns>
        public List<Article> SelectVagueByArticleName(string name)
        {
            StringBuilder sql = new StringBuilder("Select * from Article");
            var parm = new DynamicParameters();
            if (!name.IsNullOrEmpty())
            {
                sql.Append(" where title like @title");
                parm.Add("title", "%" + name + "%");
            }
            else
            {
                return null;
            }
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Article>)conn.Query<Article>(sql.ToString(), parm);
                conn.Close();
                return r;
            }
        }
    }
}

