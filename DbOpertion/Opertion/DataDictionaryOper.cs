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
    public partial class DataDictionaryOper : SingleTon<DataDictionaryOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="datadictionary"></param>
        /// <returns>是否成功</returns>
        public bool Insert(DataDictionary datadictionary)
        {
            StringBuilder sql = new StringBuilder("insert into DataDictionary ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!datadictionary.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename");
                    part2.Append("@typename");
                    flag = false;
                }
                else
                {
                    part1.Append(",typename");
                    part2.Append(",@typename");
                }
                parm.Add("typename", datadictionary.typename);
            }
            if(!datadictionary.typeValue.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typeValue");
                    part2.Append("@typeValue");
                    flag = false;
                }
                else
                {
                    part1.Append(",typeValue");
                    part2.Append(",@typeValue");
                }
                parm.Add("typeValue", datadictionary.typeValue);
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
            var r = conn.Execute(@"Delete From DataDictionary where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="datadictionary"></param>
        /// <returns>是否成功</returns>
        public bool Update(DataDictionary datadictionary)
        {
            StringBuilder sql = new StringBuilder("update DataDictionary set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!datadictionary.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", datadictionary.id);
            }
            if(!datadictionary.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename = @typename");
                    flag = false;
                }
                else
                {
                    part1.Append(", typename = @typename");
                }
                parm.Add("typename", datadictionary.typename);
            }
            if(!datadictionary.typeValue.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typeValue = @typeValue");
                    flag = false;
                }
                else
                {
                    part1.Append(", typeValue = @typeValue");
                }
                parm.Add("typeValue", datadictionary.typeValue);
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
        /// <param name="datadictionary"></param>
        /// <returns>对象列表</returns>
        public List<DataDictionary> Select(DataDictionary datadictionary)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!datadictionary.Field.IsNullOrEmpty())
            {
                sql.Append(datadictionary.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from DataDictionary ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!datadictionary.id.IsNullOrEmpty())
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
                parm.Add("id", datadictionary.id);
            }
            if(!datadictionary.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename = @typename");
                    flag = false;
                }
                else
                {
                    part1.Append(" and typename = @typename");
                }
                parm.Add("typename", datadictionary.typename);
            }
            if(!datadictionary.typeValue.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typeValue = @typeValue");
                    flag = false;
                }
                else
                {
                    part1.Append(" and typeValue = @typeValue");
                }
                parm.Add("typeValue", datadictionary.typeValue);
            }

        if(!datadictionary.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(datadictionary.GroupBy).Append(" ");
            flag = false;
        }
        if(!datadictionary.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(datadictionary.OrderBy).Append(" ");
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
                var r = (List<DataDictionary>)conn.Query<DataDictionary>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<DataDictionary>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="datadictionary"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<DataDictionary> SelectByPage(DataDictionary datadictionary,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!datadictionary.Field.IsNullOrEmpty())
            {
                sql.Append(datadictionary.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from DataDictionary ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!datadictionary.id.IsNullOrEmpty())
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
                parm.Add("id", datadictionary.id);
            }
            if(!datadictionary.typename.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typename = @typename");
                    flag = false;
                }
                else
                {
                    part1.Append(" and typename = @typename");
                }
                parm.Add("typename", datadictionary.typename);
            }
            if(!datadictionary.typeValue.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("typeValue = @typeValue");
                    flag = false;
                }
                else
                {
                    part1.Append(" and typeValue = @typeValue");
                }
                parm.Add("typeValue", datadictionary.typeValue);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from DataDictionary ");
        if(!datadictionary.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(datadictionary.GroupBy).Append(" ");
            flag = false;
        }
        if(!datadictionary.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(datadictionary.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!datadictionary.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(datadictionary.GroupBy).Append(" ");
        }
        if(!datadictionary.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(datadictionary.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<DataDictionary>)conn.Query<DataDictionary>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<DataDictionary>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<DataDictionary> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<DataDictionary>)conn.Query<DataDictionary>("Select * From DataDictionary where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<DataDictionary>();
                }
                return r;
        }
    }
    }
}
