using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IOCTest
{
    public class FactoryCreate
    {
        public static T Create<T>()
        {
            //没创建类库，暂时无法用反射
            //string typeName = ConfigurationManager.AppSettings["PhoneType"];
            //Assembly assembly = Assembly.Load(typeName.Split(',')[0]);
            Type type =typeof(T);
            return (T)Activator.CreateInstance(type);
        }
    }
}
