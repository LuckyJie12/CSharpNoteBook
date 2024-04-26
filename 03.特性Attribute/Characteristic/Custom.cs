using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TexingAttribute
{
    public enum UserState
    {
        [StateAttribute("正常")]
        Noemal = 0,
        [State("冻结")]
        Freeze = 1,
        //[State("删除")]
        Delete = 2
    }
    public class StateAttribute : Attribute
    {
        public StateAttribute(string State)
        {
            this._State = State;
        }
        private string _State;
        public string SetState()
        {
            return this._State;
        }
    }
    public static class Custom
    {
        public static string GetRemarks(this Enum Value)
        {
            Type type = Value.GetType();
            FieldInfo field = type.GetField(Value.ToString());
            if (field.IsDefined(typeof(StateAttribute),true))
            {
                StateAttribute state= (StateAttribute)field.GetCustomAttribute(typeof(StateAttribute), true);
                return state.SetState();
            }
            else
            {
                return Value.ToString();
            }
            
        }
    }
}
