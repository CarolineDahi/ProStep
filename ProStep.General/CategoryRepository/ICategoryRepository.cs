using ProStep.DataTransferObject.General.Category;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<OperationResult<IEnumerable<GetCategoryDashDto>>> GetAll();
        Task<OperationResult<DetailsCategoryDto>> GetById(Guid id);
        Task<OperationResult<DetailsCategoryDto>> Add(AddCategoryDto categoryDto);
        Task<OperationResult<DetailsCategoryDto>> Update(UpdateCategoryDto categoryDto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);

        Task<OperationResult<IEnumerable<GetCategoryDto>>> GetMain();
        Task<OperationResult<IEnumerable<GetCategoryDto>>> GetSub();
        Task<OperationResult<bool>> ChooseMyCategories(IEnumerable<Guid> ids);
    }
}
