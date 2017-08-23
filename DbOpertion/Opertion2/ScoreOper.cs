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
    public partial class ScoreOper : SingleTon<ScoreOper>
    {
        /// <summary>
        /// 模糊查找
        /// <summary>
        /// <param name="restaurant"></param>
        /// <returns>是否成功</returns>
        public bool? UpdateScoreClickByIds(List<string> List_Id)
        {
            List_Id = List_Id.Where(p => !p.IsNullOrEmpty()).ToList();
            StringBuilder sql = new StringBuilder("Update Score Set ScoreClick = 'true' ");
            var parm = new DynamicParameters();
            if (List_Id.Count != 0)
            {
                sql.Append(" where ScoreId in @ScoreId");
                parm.Add("ScoreId", List_Id.ToArray());
            }
            else
            {
                return null;
            }
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = conn.Execute(sql.ToString(), parm);
                conn.Close();
                return r > 0;
            }
        }
    }
}

