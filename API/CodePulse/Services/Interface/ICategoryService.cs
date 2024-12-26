using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICategoryService
    {
        void CreateCategory(CreateCategoryRequestDto request);

        List<CategoryDto> GetAllCategories();

        Task<CategoryDto?> GetCategoryById(Guid id);

        void UpdateCategoryRequestDto(UpdateCategoryRequestDto request, Guid id);

        void DeleteCategory(Guid id);
    }
}
