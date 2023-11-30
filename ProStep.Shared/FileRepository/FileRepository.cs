using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProStep.Model.Shared;
using ProStep.DataTransferObject.Shared.File;

namespace ProStep.Shared.FileRepository
{
    public partial class FileRepository : ProStepRepository, IFileRepository
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public FileRepository(ProStepDBContext context, IHostingEnvironment hostingEnvironment) : base(context)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<OperationResult<List<GetFileDto>>> Add(string foldername, List<IFormFile> files)
        {
            try
            {
                TryUploadFile(foldername, files, out List<string> paths, out List<string> names);
                List<Model.Shared._File> FilesToAdd = new List<_File>();
                foreach (var path in paths)
                {
                    var file = new _File()
                    {
                        Name = names[paths.IndexOf(path)],
                        Path = path,
                        Type = ExtensionMethods.TypeOfFile(files.First()),
                    };
                    FilesToAdd.Add(file);
                }
                context.AddRange(FilesToAdd);
                await context.SaveChangesAsync();

                return OperationR.SetSuccess(FilesToAdd.Select(d => new GetFileDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Path = d.Path,
                    Type = d.Type,
                }).ToList());
            }
            catch (Exception ex)
            {
                return OperationR.SetException<List<GetFileDto>>(ex);
            }
        }

        public async Task<OperationResult<bool>> Delete(List<Guid> ids)
        {
            var operation = new OperationResult<bool>();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var files = new List<_File>();//await context.Documents.Where(d => ids.Contains(d.Id)).ToListAsync();
                    TryDeleteFile(files.Select(d => d.Path).ToList());
                    context.RemoveRange(files);
                    await context.SaveChangesAsync();
                    operation.SetSuccess(true);
                }
                catch (Exception ex)
                {
                    operation.SetException(ex);
                }
            }
            return operation;
        }

        public async Task<OperationResult<string>> Upload(string foldername, IFormFile file)
        {
            var operation = new OperationResult<string>();
            try
            {
                if (file == null)
                {
                    operation.SetSuccess("");
                }

                TryUploadFile(foldername, new List<IFormFile>() { file }, out List<string> paths, out List<string> names);

                operation.SetSuccess(paths.First());
            }
            catch (Exception ex)
            {
                operation.SetException(ex);
            }
            return operation;
        }

        public async Task<OperationResult<bool>> Remove(string path)
        {
            TryDeleteFile(new List<string>() { path });

            return OperationR.SetSuccess(true);
        }
    }
}
