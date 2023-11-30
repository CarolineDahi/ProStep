using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.SharedKernel.OperationResult
{
    public sealed class OperationR
    {
        public static OperationResult<T> SetSuccess<T>(T result)
        {
            return new OperationResult<T>().SetSuccess(result);
        }

        public static OperationResult<T> SetFailed<T>(string message, OperationResultType type = OperationResultType.Failed)
        {
            return new OperationResult<T>().SetFailed(message, type);
        }

        public static OperationResult<T> SetException<T>(Exception exception)
        {
            return new OperationResult<T>().SetException(exception);
        }
    }
}
