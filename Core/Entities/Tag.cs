using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public bool IsRemoved { get; set; }
    }
}
