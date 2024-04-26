using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WebServices.Server
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public CustomSoapHeader CustomSoapHeader;
        //必须添加
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        //引用特性
        [System.Web.Services.Protocols.SoapHeader("CustomSoapHeader")]
        public List<User> GetUser()
        {
            if (CustomSoapHeader != null && CustomSoapHeader.Validate())
            {
                var User1 = new List<User>()
            {
               new User(){Name = "Lucky",Sex = 21},
               new User(){Name = "Jacky",Sex = 21},
            };
                return User1;
            }
            else
            {
                throw new SoapException("身份验证不通过", SoapException.ServerFaultCode);
            }
        }
    }
    public class User
    {
        public string Name { get; set; }
        public int Sex { get; set; }
    }
}
