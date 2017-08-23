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
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="customerlike"></param>
        /// <returns>是否成功</returns>
        public bool Insert(CustomerLike customerlike)
        {
            StringBuilder sql = new StringBuilder("insert into CustomerLike ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customerlike.cid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cid");
                    part2.Append("@cid");
                    flag = false;
                }
                else
                {
                    part1.Append(",cid");
                    part2.Append(",@cid");
                }
                parm.Add("cid", customerlike.cid);
            }
            if(!customerlike.type.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("type");
                    part2.Append("@type");
                    flag = false;
                }
                else
                {
                    part1.Append(",type");
                    part2.Append(",@type");
                }
                parm.Add("type", customerlike.type);
            }
            if(!customerlike.lid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("lid");
                    part2.Append("@lid");
                    flag = false;
                }
                else
                {
                    part1.Append(",lid");
                    part2.Append(",@lid");
                }
                parm.Add("lid", customerlike.lid);
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
            var r = conn.Execute(@"Delete From CustomerLike where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="customerlike"></param>
        /// <returns>是否成功</returns>
        public bool Update(CustomerLike customerlike)
        {
            StringBuilder sql = new StringBuilder("update CustomerLike set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customerlike.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", customerlike.id);
            }
            if(!customerlike.cid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("cid = @cid");
                    flag = false;
                }
                else
                {
                    part1.Append(", cid = @cid");
                }
                parm.Add("cid", customerlike.cid);
            }
            if(!customerlike.type.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("type = @type");
                    flag = false;
                }
                else
                {
                    part1.Append(", type = @type");
                }
                parm.Add("type", customerlike.type);
            }
            if(!customerlike.lid.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("lid = @lid");
                    flag = false;
                }
                else
                {
                    part1.Append(", lid = @lid");
                }
                parm.Add("lid", customerlike.lid);
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
        /// <param name="customerlike"></param>
        /// <returns>对象列表</returns>
        public List<CustomerLike> Select(CustomerLike customerlike)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!customerlike.Field.IsNullOrEmpty())
            {
                sql.Append(customerlike.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from CustomerLike ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customerlike.id.IsNullOrEmpty())
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
            if(!customerlike.cid.IsNullOrEmpty())
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
            if(!customerlike.type.IsNullOrEmpty())
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
            if(!customerlike.lid.IsNullOrEmpty())
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

        if(!customerlike.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(customerlike.GroupBy).Append(" ");
            flag = false;
        }
        if(!customerlike.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(customerlike.OrderBy).Append(" ");
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
                var r = (List<CustomerLike>)conn.Query<CustomerLike>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<CustomerLike>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="customerlike"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<CustomerLike> SelectByPage(CustomerLike customerlike,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!customerlike.Field.IsNullOrEmpty())
            {
                sql.Append(customerlike.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from CustomerLike ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customerlike.id.IsNullOrEmpty())
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
            if(!customerlike.cid.IsNullOrEmpty())
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
            if(!customerlike.type.IsNullOrEmpty())
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
            if(!customerlike.lid.IsNullOrEmpty())
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
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from CustomerLike ");
        if(!customerlike.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(customerlike.GroupBy).Append(" ");
            flag = false;
        }
        if(!customerlike.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(customerlike.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!customerlike.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(customerlike.GroupBy).Append(" ");
        }
        if(!customerlike.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(customerlike.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<CustomerLike>)conn.Query<CustomerLike>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<CustomerLike>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<CustomerLike> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<CustomerLike>)conn.Query<CustomerLike>("Select * From CustomerLike where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<CustomerLike>();
                }
                return r;
        }
    }
    }
}
