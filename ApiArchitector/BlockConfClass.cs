using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiArchitector
{
    public class BlockConfClass
    {
        public string URL { get; set; }
        public string Method { get; set; }
        public List<Headers> Headers { get; set; }
        public string RequestBody { get; set; }
    }
}
