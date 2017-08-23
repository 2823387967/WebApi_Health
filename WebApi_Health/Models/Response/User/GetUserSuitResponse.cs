using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Health.Models.Response
{
    /// <summary>
    /// 获得用户食品应答
    /// </summary>
    public class GetUserSuitResponse
    {
        /// <summary>
        /// 获得用户食品应答
        /// </summary>
        public GetUserSuitResponse(Customer user)
        {
            //constitution = user.constitution;
            //
            constitution = "您的体质是XXX";
            SuitEat = "适 合 吃：";
            NotSuitEat = "不适合吃：";
        }
        /// <summary>
        /// 体质
        /// </summary>
        public String constitution { get; set; }
        /// <summary>
        /// 适合吃
        /// </summary>
        public String SuitEat { get; set; }
        /// <summary>
        /// 不适合吃
        /// </summary>
        public String NotSuitEat { get; set; }
    }
}