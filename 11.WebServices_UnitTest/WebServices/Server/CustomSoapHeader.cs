using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Server
{
    public class CustomSoapHeader:System.Web.Services.Protocols.SoapHeader
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public CustomSoapHeader()
        {
            //必须要有一个无参构造函数
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public CustomSoapHeader(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        /// <summary>
        /// 密码验证
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            return (UserName.Contains("k") && Password.Contains("1"));
        }
    }
}