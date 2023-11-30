using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class CommonQuestion : BaseEntity
    {
        public CommonQuestion() { }

        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
