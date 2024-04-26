using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class PropDisPlayAttribute:Attribute
    {
        private string _name;
        public PropDisPlayAttribute(string Name)
        {
            this._name = Name;
        }
        public string ResultName()
        {
            return _name;
        }
    }
    public static class GetName
    {
        public static string GetPropName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(PropDisPlayAttribute),true))
            {
                PropDisPlayAttribute Dis=(PropDisPlayAttribute)prop.GetCustomAttribute(typeof(PropDisPlayAttribute), true);
                return Dis.ResultName();
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
