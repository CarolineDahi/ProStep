using ProStep.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class Setting : BaseEntity
    {
        public Setting()
        {
        }

        public string TemplateUrl { get; set; }
        public string Email { get; set; }
    }
}
