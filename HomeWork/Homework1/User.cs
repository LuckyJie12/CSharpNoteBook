using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    internal  class User:Common
    {
        public string UserName { get; set; }
        [PropDisPlay("Gender")]
        public bool? Sex { get; set; }
        public string Remark { get; set; }
    }
}
