using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streac.Models
{
    public class Player
    {
        public string Name { get; set; }
        public object Key { get; set; }
        public int Points { get; set; }
        public bool Active { get; set; } = false;
    }
}
