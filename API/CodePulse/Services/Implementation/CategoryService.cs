using DataAccess.Interface;
using Domain.Models;
using DTOs;
using Microsoft.AspNetCore.Http;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void CreateCategory(CreateCategoryRequestDto request)
        {
            //validations
            if (request == null)
            {
                throw new Exception("Category cannot be null");
            }

            //map to domain
            var newCategory = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            //add to db
            _categoryRepository.Add(newCategory);
        }

        public void DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID provided");
            }

            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            _categoryRepository.Delete(category);
        }

        public List<CategoryDto> GetAllCategories()
        {
            // Retrieve all categories from the repository
            var categoriesDb = _categoryRepository.GetAll();

            // Map each Category to CategoryDto
            var categoryDtos = categoriesDb.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            }).ToList();

            // Return the list of DTOs
            return categoryDtos;
        }

        public Task<CategoryDto?> GetCategoryById(Guid id)
        {
            // Check if the ID is empty
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID provided");
            }

            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                // If the category is not found, return null or throw an exception
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Task.FromResult(categoryDto);
        }

        public void UpdateCategoryRequestDto(UpdateCategoryRequestDto request, Guid id)
        {
            // Fetch the existing entity
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new Exception($"Category with category id: {id} doesn't exist");
            }
            // Validate input
            if (request == null)
            {
                throw new Exception("Category cannot be null");
            }
           
            // Update properties of the existing entity
            category.Name = request.Name;
            category.UrlHandle = request.UrlHandle;

            // Save the updated entity
            _categoryRepository.Update(category);

        }
    }
}
