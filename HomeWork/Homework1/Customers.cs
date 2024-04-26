using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    internal class Customers:Common
    {
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public long? Tel { get; set; }
    }
}
