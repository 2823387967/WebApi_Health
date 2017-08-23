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
    public partial class CustomerLikeOper : SingleTon<CustomerLikeOper>
    {
        /// <summary>
        /// 删除
        /// <summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public bool DeleteByModel(CustomerLike customerlike)
        {
            StringBuilder sql = new StringBuilder("Delete from CustomerLike ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if (!customerlike.id.IsNullOrEmpty())
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
                parm.Add("id", customerlike.id);
            }
            if (!customerlike.cid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cid = @cid");
                    flag = false;
                }
                else
                {
                    part1.Append(" and cid = @cid");
                }
                parm.Add("cid", customerlike.cid);
            }
            if (!customerlike.type.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("type = @type");
                    flag = false;
                }
                else
                {
                    part1.Append(" and type = @type");
                }
                parm.Add("type", customerlike.type);
            }
            if (!customerlike.lid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("lid = @lid");
                    flag = false;
                }
                else
                {
                    part1.Append(" and lid = @lid");
                }
                parm.Add("lid", customerlike.lid);
            }

            if (flag)
            {
                return false;
            }
            sql.Append(" where ");
            sql.Append(part1);
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