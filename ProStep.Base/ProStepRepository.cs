using ProStep.DataSourse.Context;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Base
{
    public class ProStepRepository
    {
        protected readonly ProStepDBContext context;

        protected ProStepRepository(ProStepDBContext context)
        {
            this.context = context;
        }

    }
}
