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
        // 扩展方法GetRemarks，用于Enum类型
        public static string GetRemarks(this Enum Value)
        {
            // 获取枚举值的类型
            Type type = Value.GetType();
            // 获取枚举值对应的字段信息
            FieldInfo field = type.GetField(Value.ToString());
            // 检查字段是否定义了StateAttribute特性，第二个参数true表示查找继承链
            if (field.IsDefined(typeof(StateAttribute), true))
            {
                // 如果定义了StateAttribute特性，获取该特性的实例
                StateAttribute state = (StateAttribute)field.GetCustomAttribute(typeof(StateAttribute), true);
                // 调用StateAttribute的SetState方法，返回相关的状态描述
                return state.SetState();
            }
            else
            {
                // 如果没有定义StateAttribute特性，返回枚举值的名称
                return Value.ToString();
            }
        }
    }
}
