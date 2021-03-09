using System;
using System.Collections.Generic;
using System.Linq;
using CategoryService.Models;
using MongoDB.Driver;

namespace CategoryService.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext context;

        public CategoryRepository(CategoryContext _context)
        {
            this.context = _context;
        }
        public Category CreateCategory(Category category)
        {
            var categories = this.context.Categories.AsQueryable();
            if (categories.Count() == 0)
            {
                category.Id = 101;
            }
            else
            {
                int maxCategoryId = categories.Max(c => c.Id) +1;
                category.Id = maxCategoryId;
            }
            category.CreationDate = DateTime.Now;
            context.Categories.InsertOne(category);
            return category;
        }

        public bool DeleteCategory(int categoryId)
        {
            DeleteResult deleteResult= context.Categories.DeleteOne(c=>c.Id==categoryId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(c => c.CreatedBy, userId);
            return context.Categories.Find(filter).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);
            return context.Categories.Find(filter).FirstOrDefault();
        }

        public bool UpdateCategory(int categoryId,Category category)
        {
            ReplaceOneResult updateResult = context.Categories.ReplaceOne(filter: c=>c.Id== categoryId, replacement:category);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
