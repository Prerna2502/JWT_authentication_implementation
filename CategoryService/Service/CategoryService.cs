using System;
using System.Collections.Generic;
using CategoryService.Models;
using CategoryService.Repository;
using CategoryService.Exceptions;
using MongoDB.Driver;
using System.Linq;

namespace CategoryService.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository repository;
        private string NotFoundText = "This category id not found";
        public CategoryService(ICategoryRepository _repository)
        {
            this.repository = _repository;
        }

        public Category CreateCategory(Category _category)
        {
            var category = repository.GetAllCategoriesByUserId(_category.CreatedBy).Where(c=>c.Name==_category.Name).FirstOrDefault();
            if(category==null)
            {
                return repository.CreateCategory(_category);
            }
            else
            {
                throw new CategoryNotCreatedException("This category already exists");
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            var deleteResult= repository.DeleteCategory(categoryId);
            if (deleteResult == false)
            {
                throw new CategoryNotFoundException(this.NotFoundText);
            }
            return deleteResult;
        }

        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return repository.GetAllCategoriesByUserId(userId);
        }

        public Category GetCategoryById(int categoryId)
        {
           var category=this.GetCategory(categoryId);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new CategoryNotFoundException(this.NotFoundText);
            }
        }

        public bool UpdateCategory(int categoryId, Category category)
        {
            var _category = this.GetCategoryById(categoryId);
            if (_category == null)
            {
                throw new CategoryNotFoundException(this.NotFoundText);
            }
            else
            {
                return repository.UpdateCategory(categoryId, category);
            }
        }

        private Category GetCategory(int categoryId)
        {
            return repository.GetCategoryById(categoryId);
        }
    }
}
