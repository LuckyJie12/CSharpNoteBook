using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpOOP.Inherit
{
    public abstract class InterFace
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private string Remark { get; set; } = "Remarks";
        protected string GetPrivateFieldValue()
        {
            return Remark;
        }
        public void UseCall()
        {
            Console.WriteLine($"Use {this.GetType().Name} CAll");
        }
        public void UseText()
        {
            Console.WriteLine($"Use {this.GetType().Name} Text");
        }
        public abstract void System();
        public virtual void Sound()
        {
            Console.WriteLine("Animal is making a sound.");
        }
    }
}
