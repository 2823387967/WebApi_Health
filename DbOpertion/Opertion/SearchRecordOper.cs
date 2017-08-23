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
    public partial class SearchRecordOper : SingleTon<SearchRecordOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="searchrecord"></param>
        /// <returns>是否成功</returns>
        public bool Insert(SearchRecord searchrecord)
        {
            StringBuilder sql = new StringBuilder("insert into SearchRecord ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!searchrecord.SearchKey.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchKey");
                    part2.Append("@SearchKey");
                    flag = false;
                }
                else
                {
                    part1.Append(",SearchKey");
                    part2.Append(",@SearchKey");
                }
                parm.Add("SearchKey", searchrecord.SearchKey);
            }
            if(!searchrecord.SearchCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchCount");
                    part2.Append("@SearchCount");
                    flag = false;
                }
                else
                {
                    part1.Append(",SearchCount");
                    part2.Append(",@SearchCount");
                }
                parm.Add("SearchCount", searchrecord.SearchCount);
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
            object parm = new { SearchId = id };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
            var r = conn.Execute(@"Delete From SearchRecord where SearchId=@SearchId",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="searchrecord"></param>
        /// <returns>是否成功</returns>
        public bool Update(SearchRecord searchrecord)
        {
            StringBuilder sql = new StringBuilder("update SearchRecord set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!searchrecord.SearchId.IsNullOrEmpty())
            {
                part2.Append("SearchId = @SearchId");
                parm.Add("SearchId", searchrecord.SearchId);
            }
            if(!searchrecord.SearchKey.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchKey = @SearchKey");
                    flag = false;
                }
                else
                {
                    part1.Append(", SearchKey = @SearchKey");
                }
                parm.Add("SearchKey", searchrecord.SearchKey);
            }
            if(!searchrecord.SearchCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchCount = @SearchCount");
                    flag = false;
                }
                else
                {
                    part1.Append(", SearchCount = @SearchCount");
                }
                parm.Add("SearchCount", searchrecord.SearchCount);
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
        /// <param name="searchrecord"></param>
        /// <returns>对象列表</returns>
        public List<SearchRecord> Select(SearchRecord searchrecord)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!searchrecord.Field.IsNullOrEmpty())
            {
                sql.Append(searchrecord.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from SearchRecord ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!searchrecord.SearchId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchId = @SearchId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SearchId = @SearchId");
                }
                parm.Add("SearchId", searchrecord.SearchId);
            }
            if(!searchrecord.SearchKey.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchKey = @SearchKey");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SearchKey = @SearchKey");
                }
                parm.Add("SearchKey", searchrecord.SearchKey);
            }
            if(!searchrecord.SearchCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchCount = @SearchCount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SearchCount = @SearchCount");
                }
                parm.Add("SearchCount", searchrecord.SearchCount);
            }

        if(!searchrecord.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(searchrecord.GroupBy).Append(" ");
            flag = false;
        }
        if(!searchrecord.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(searchrecord.OrderBy).Append(" ");
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
                var r = (List<SearchRecord>)conn.Query<SearchRecord>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<SearchRecord>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="searchrecord"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<SearchRecord> SelectByPage(SearchRecord searchrecord,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!searchrecord.Field.IsNullOrEmpty())
            {
                sql.Append(searchrecord.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from SearchRecord ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!searchrecord.SearchId.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchId = @SearchId");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SearchId = @SearchId");
                }
                parm.Add("SearchId", searchrecord.SearchId);
            }
            if(!searchrecord.SearchKey.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchKey = @SearchKey");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SearchKey = @SearchKey");
                }
                parm.Add("SearchKey", searchrecord.SearchKey);
            }
            if(!searchrecord.SearchCount.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("SearchCount = @SearchCount");
                    flag = false;
                }
                else
                {
                    part1.Append(" and SearchCount = @SearchCount");
                }
                parm.Add("SearchCount", searchrecord.SearchCount);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" SearchId not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" SearchId from SearchRecord ");
        if(!searchrecord.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(searchrecord.GroupBy).Append(" ");
            flag = false;
        }
        if(!searchrecord.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(searchrecord.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!searchrecord.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(searchrecord.GroupBy).Append(" ");
        }
        if(!searchrecord.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(searchrecord.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<SearchRecord>)conn.Query<SearchRecord>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<SearchRecord>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<SearchRecord> SelectByIds(List<string> List_Id)
        {
            object parm = new { SearchId = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<SearchRecord>)conn.Query<SearchRecord>("Select * From SearchRecord where SearchId in @SearchId", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<SearchRecord>();
                }
                return r;
        }
    }
    }
}
