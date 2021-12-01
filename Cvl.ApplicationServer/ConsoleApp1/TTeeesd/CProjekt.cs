using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNS;

namespace ConsoleApp1.TTeeesd
{
    public  class CProjekt : JsProject
    {
        public object Proj { get; set; } = new JsProject();
        public DateTime T { get; set; } = DateTime.Now;
    }
}
