using Microsoft.AspNetCore.Http;
using ProStep.DataTransferObject.Profile.Portfolio;
using ProStep.DataTransferObject.Shared.File;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Profile.PortfolioRepository
{
    public interface IPortfolioRepository
    {
        Task<OperationResult<GetMyPortfolioDto>> GetMyPortfolio();
        Task<OperationResult<GetPortfolioDto>> GetPortfolio(Guid id);
        Task<OperationResult<string>> UploadMyImage(IFormFile file);
        Task<OperationResult<string>> UploadMyCover(IFormFile file);
        Task<OperationResult<GetMyPortfolioDto>> UpateMyPortfolio(UpdatePortfolioDto dto);
        Task<OperationResult<bool>> UpdateMyCareers(UpdateDto dto);
        Task<OperationResult<bool>> UpdateMySkills(UpdateDto dto);
        Task<OperationResult<IEnumerable<GetFileDto>>> GetCertificates(Guid? id);
        Task<OperationResult<bool>> UpdateMyCertificates(UpdateCertificateDto dto);
    }
}
