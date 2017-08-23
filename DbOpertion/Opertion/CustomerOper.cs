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
    public partial class CustomerOper : SingleTon<CustomerOper>
    {
        public string ConnString=ConfigurationManager.AppSettings["ConnString"].ToString();
                /// <summary>
        /// 插入
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>是否成功</returns>
        public bool Insert(Customer customer)
        {
            StringBuilder sql = new StringBuilder("insert into Customer ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customer.name.IsNullOrEmpty())
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
                parm.Add("name", customer.name);
            }
            if(!customer.phone.IsNullOrEmpty())
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
                parm.Add("phone", customer.phone);
            }
            if(!customer.wechat.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wechat");
                    part2.Append("@wechat");
                    flag = false;
                }
                else
                {
                    part1.Append(",wechat");
                    part2.Append(",@wechat");
                }
                parm.Add("wechat", customer.wechat);
            }
            if(!customer.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password");
                    part2.Append("@password");
                    flag = false;
                }
                else
                {
                    part1.Append(",password");
                    part2.Append(",@password");
                }
                parm.Add("password", customer.password);
            }
            if(!customer.height.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("height");
                    part2.Append("@height");
                    flag = false;
                }
                else
                {
                    part1.Append(",height");
                    part2.Append(",@height");
                }
                parm.Add("height", customer.height);
            }
            if(!customer.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight");
                    part2.Append("@weight");
                    flag = false;
                }
                else
                {
                    part1.Append(",weight");
                    part2.Append(",@weight");
                }
                parm.Add("weight", customer.weight);
            }
            if(!customer.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex");
                    part2.Append("@sex");
                    flag = false;
                }
                else
                {
                    part1.Append(",sex");
                    part2.Append(",@sex");
                }
                parm.Add("sex", customer.sex);
            }
            if(!customer.birthday.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("birthday");
                    part2.Append("@birthday");
                    flag = false;
                }
                else
                {
                    part1.Append(",birthday");
                    part2.Append(",@birthday");
                }
                parm.Add("birthday", customer.birthday);
            }
            if(!customer.labourIntensity.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("labourIntensity");
                    part2.Append("@labourIntensity");
                    flag = false;
                }
                else
                {
                    part1.Append(",labourIntensity");
                    part2.Append(",@labourIntensity");
                }
                parm.Add("labourIntensity", customer.labourIntensity);
            }
            if(!customer.constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("constitution");
                    part2.Append("@constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(",constitution");
                    part2.Append(",@constitution");
                }
                parm.Add("constitution", customer.constitution);
            }
            if(!customer.score.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("score");
                    part2.Append("@score");
                    flag = false;
                }
                else
                {
                    part1.Append(",score");
                    part2.Append(",@score");
                }
                parm.Add("score", customer.score);
            }
            if(!customer.HeadImage.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("HeadImage");
                    part2.Append("@HeadImage");
                    flag = false;
                }
                else
                {
                    part1.Append(",HeadImage");
                    part2.Append(",@HeadImage");
                }
                parm.Add("HeadImage", customer.HeadImage);
            }
            if(!customer.UserScore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserScore");
                    part2.Append("@UserScore");
                    flag = false;
                }
                else
                {
                    part1.Append(",UserScore");
                    part2.Append(",@UserScore");
                }
                parm.Add("UserScore", customer.UserScore);
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
            var r = conn.Execute(@"Delete From Customer where id=@id",parm);
            conn.Close();
                return r > 0;
        }
    }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>是否成功</returns>
        public bool Update(Customer customer)
        {
            StringBuilder sql = new StringBuilder("update Customer set ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customer.id.IsNullOrEmpty())
            {
                part2.Append("id = @id");
                parm.Add("id", customer.id);
            }
            if(!customer.name.IsNullOrEmpty())
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
                parm.Add("name", customer.name);
            }
            if(!customer.phone.IsNullOrEmpty())
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
                parm.Add("phone", customer.phone);
            }
            if(!customer.wechat.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wechat = @wechat");
                    flag = false;
                }
                else
                {
                    part1.Append(", wechat = @wechat");
                }
                parm.Add("wechat", customer.wechat);
            }
            if(!customer.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password = @password");
                    flag = false;
                }
                else
                {
                    part1.Append(", password = @password");
                }
                parm.Add("password", customer.password);
            }
            if(!customer.height.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("height = @height");
                    flag = false;
                }
                else
                {
                    part1.Append(", height = @height");
                }
                parm.Add("height", customer.height);
            }
            if(!customer.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight = @weight");
                    flag = false;
                }
                else
                {
                    part1.Append(", weight = @weight");
                }
                parm.Add("weight", customer.weight);
            }
            if(!customer.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex = @sex");
                    flag = false;
                }
                else
                {
                    part1.Append(", sex = @sex");
                }
                parm.Add("sex", customer.sex);
            }
            if(!customer.birthday.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("birthday = @birthday");
                    flag = false;
                }
                else
                {
                    part1.Append(", birthday = @birthday");
                }
                parm.Add("birthday", customer.birthday);
            }
            if(!customer.labourIntensity.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("labourIntensity = @labourIntensity");
                    flag = false;
                }
                else
                {
                    part1.Append(", labourIntensity = @labourIntensity");
                }
                parm.Add("labourIntensity", customer.labourIntensity);
            }
            if(!customer.constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("constitution = @constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(", constitution = @constitution");
                }
                parm.Add("constitution", customer.constitution);
            }
            if(!customer.score.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("score = @score");
                    flag = false;
                }
                else
                {
                    part1.Append(", score = @score");
                }
                parm.Add("score", customer.score);
            }
            if(!customer.HeadImage.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("HeadImage = @HeadImage");
                    flag = false;
                }
                else
                {
                    part1.Append(", HeadImage = @HeadImage");
                }
                parm.Add("HeadImage", customer.HeadImage);
            }
            if(!customer.UserScore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserScore = @UserScore");
                    flag = false;
                }
                else
                {
                    part1.Append(", UserScore = @UserScore");
                }
                parm.Add("UserScore", customer.UserScore);
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
        /// <param name="customer"></param>
        /// <returns>对象列表</returns>
        public List<Customer> Select(Customer customer)
        {
            StringBuilder sql = new StringBuilder("Select ");
            if(!customer.Field.IsNullOrEmpty())
            {
                sql.Append(customer.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Customer ");
            StringBuilder part1 = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customer.id.IsNullOrEmpty())
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
                parm.Add("id", customer.id);
            }
            if(!customer.name.IsNullOrEmpty())
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
                parm.Add("name", customer.name);
            }
            if(!customer.phone.IsNullOrEmpty())
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
                parm.Add("phone", customer.phone);
            }
            if(!customer.wechat.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wechat = @wechat");
                    flag = false;
                }
                else
                {
                    part1.Append(" and wechat = @wechat");
                }
                parm.Add("wechat", customer.wechat);
            }
            if(!customer.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password = @password");
                    flag = false;
                }
                else
                {
                    part1.Append(" and password = @password");
                }
                parm.Add("password", customer.password);
            }
            if(!customer.height.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("height = @height");
                    flag = false;
                }
                else
                {
                    part1.Append(" and height = @height");
                }
                parm.Add("height", customer.height);
            }
            if(!customer.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight = @weight");
                    flag = false;
                }
                else
                {
                    part1.Append(" and weight = @weight");
                }
                parm.Add("weight", customer.weight);
            }
            if(!customer.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex = @sex");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sex = @sex");
                }
                parm.Add("sex", customer.sex);
            }
            if(!customer.birthday.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("birthday = @birthday");
                    flag = false;
                }
                else
                {
                    part1.Append(" and birthday = @birthday");
                }
                parm.Add("birthday", customer.birthday);
            }
            if(!customer.labourIntensity.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("labourIntensity = @labourIntensity");
                    flag = false;
                }
                else
                {
                    part1.Append(" and labourIntensity = @labourIntensity");
                }
                parm.Add("labourIntensity", customer.labourIntensity);
            }
            if(!customer.constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("constitution = @constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(" and constitution = @constitution");
                }
                parm.Add("constitution", customer.constitution);
            }
            if(!customer.score.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("score = @score");
                    flag = false;
                }
                else
                {
                    part1.Append(" and score = @score");
                }
                parm.Add("score", customer.score);
            }
            if(!customer.HeadImage.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("HeadImage = @HeadImage");
                    flag = false;
                }
                else
                {
                    part1.Append(" and HeadImage = @HeadImage");
                }
                parm.Add("HeadImage", customer.HeadImage);
            }
            if(!customer.UserScore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserScore = @UserScore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and UserScore = @UserScore");
                }
                parm.Add("UserScore", customer.UserScore);
            }

        if(!customer.GroupBy.IsNullOrEmpty())
        {
            part1.Append(" Group By ").Append(customer.GroupBy).Append(" ");
            flag = false;
        }
        if(!customer.OrderBy.IsNullOrEmpty())
        {
            part1.Append(" Order By ").Append(customer.OrderBy).Append(" ");
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
                var r = (List<Customer>)conn.Query<Customer>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Customer>();
                }
                return r;
            }
    }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">页面编号</param>
        /// <returns>对象列表</returns>
        public List<Customer> SelectByPage(Customer customer,int pageSize,int pageNo)
        {
            StringBuilder sql = new StringBuilder("Select Top ").Append(pageSize).Append(" ");
            if(!customer.Field.IsNullOrEmpty())
            {
                sql.Append(customer.Field);
            }
            else
            {
                sql.Append("*");
            }
            sql.Append(" from Customer ");
            StringBuilder part1 = new StringBuilder();
            StringBuilder part2 = new StringBuilder();
            StringBuilder strBuliderPage = new StringBuilder();
            var parm = new DynamicParameters();
            bool flag = true;
            if(!customer.id.IsNullOrEmpty())
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
                parm.Add("id", customer.id);
            }
            if(!customer.name.IsNullOrEmpty())
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
                parm.Add("name", customer.name);
            }
            if(!customer.phone.IsNullOrEmpty())
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
                parm.Add("phone", customer.phone);
            }
            if(!customer.wechat.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("wechat = @wechat");
                    flag = false;
                }
                else
                {
                    part1.Append(" and wechat = @wechat");
                }
                parm.Add("wechat", customer.wechat);
            }
            if(!customer.password.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("password = @password");
                    flag = false;
                }
                else
                {
                    part1.Append(" and password = @password");
                }
                parm.Add("password", customer.password);
            }
            if(!customer.height.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("height = @height");
                    flag = false;
                }
                else
                {
                    part1.Append(" and height = @height");
                }
                parm.Add("height", customer.height);
            }
            if(!customer.weight.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("weight = @weight");
                    flag = false;
                }
                else
                {
                    part1.Append(" and weight = @weight");
                }
                parm.Add("weight", customer.weight);
            }
            if(!customer.sex.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("sex = @sex");
                    flag = false;
                }
                else
                {
                    part1.Append(" and sex = @sex");
                }
                parm.Add("sex", customer.sex);
            }
            if(!customer.birthday.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("birthday = @birthday");
                    flag = false;
                }
                else
                {
                    part1.Append(" and birthday = @birthday");
                }
                parm.Add("birthday", customer.birthday);
            }
            if(!customer.labourIntensity.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("labourIntensity = @labourIntensity");
                    flag = false;
                }
                else
                {
                    part1.Append(" and labourIntensity = @labourIntensity");
                }
                parm.Add("labourIntensity", customer.labourIntensity);
            }
            if(!customer.constitution.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("constitution = @constitution");
                    flag = false;
                }
                else
                {
                    part1.Append(" and constitution = @constitution");
                }
                parm.Add("constitution", customer.constitution);
            }
            if(!customer.score.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("score = @score");
                    flag = false;
                }
                else
                {
                    part1.Append(" and score = @score");
                }
                parm.Add("score", customer.score);
            }
            if(!customer.HeadImage.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("HeadImage = @HeadImage");
                    flag = false;
                }
                else
                {
                    part1.Append(" and HeadImage = @HeadImage");
                }
                parm.Add("HeadImage", customer.HeadImage);
            }
            if(!customer.UserScore.IsNullOrEmpty())
            {
                if (flag)
                {
                    part1.Append("UserScore = @UserScore");
                    flag = false;
                }
                else
                {
                    part1.Append(" and UserScore = @UserScore");
                }
                parm.Add("UserScore", customer.UserScore);
            }
        if(!flag)
        {
            strBuliderPage.Append(" and");
        }strBuliderPage.Append(" id not in (").Append("Select Top ").Append(pageSize * (pageNo - 1)).Append(" id from Customer ");
        if(!customer.GroupBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Group By ").Append(customer.GroupBy).Append(" ");
            flag = false;
        }
        if(!customer.OrderBy.IsNullOrEmpty())
        {
            strBuliderPage.Append(" Order By ").Append(customer.OrderBy).Append(" ");
            flag = false;
        }
        strBuliderPage.Append(" )");
            if (!flag)
            {
                sql.Append(" where ");
            }
            sql.Append(part1).Append(strBuliderPage).Append(part1);
        if(!customer.GroupBy.IsNullOrEmpty())
        {
            part2.Append(" Group By ").Append(customer.GroupBy).Append(" ");
        }
        if(!customer.OrderBy.IsNullOrEmpty())
        {
            part2.Append(" Order By ").Append(customer.OrderBy).Append(" ");
        }
        sql.Append(part2);
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Customer>)conn.Query<Customer>(sql.ToString(), parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Customer>();
                }
                return r;
        }
    }
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否成功</returns>
        public List<Customer> SelectByIds(List<string> List_Id)
        {
            object parm = new { id = List_Id.ToArray() };
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var r = (List<Customer>)conn.Query<Customer>("Select * From Customer where id in @id", parm);
                conn.Close();
                if(r == null)
                {
                    r = new List<Customer>();
                }
                return r;
        }
    }
    }
}
