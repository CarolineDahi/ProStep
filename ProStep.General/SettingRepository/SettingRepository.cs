using ProStep.Base;
using ProStep.DataSourse.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.SettingRepository
{
    public class SettingRepository : ProStepRepository, ISettingRepository
    {
        public SettingRepository(ProStepDBContext context) : base(context)
        {
        }
    }
}
