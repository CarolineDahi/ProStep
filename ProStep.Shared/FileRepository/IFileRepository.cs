using Microsoft.AspNetCore.Http;
using ProStep.DataTransferObject.Shared.File;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Shared.FileRepository
{
    public interface IFileRepository
    {
        Task<OperationResult<List<GetFileDto>>> Add(string foldername, List<IFormFile> files);
        Task<OperationResult<bool>> Delete(List<Guid> ids);
        Task<OperationResult<string>> Upload(string foldername, IFormFile file);
        Task<OperationResult<bool>> Remove(string path);
    }
}
