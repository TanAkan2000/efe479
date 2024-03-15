using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Business.Services
{
    public interface ICategoryService
    {
        IQueryable<CategoryModel> Query();
        Result Add(CategoryModel model);
        Result Update(CategoryModel model);
        Result Delete(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly Db _db;

        public CategoryService(Db db)
        {
            _db = db;
        }

        // Read
        public IQueryable<CategoryModel> Query()
        {
            return _db.Categories.Select(c => new CategoryModel()
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                PostCountOutput = c.Posts.Count,
                PostNamesOutput = string.Join(", ", c.Posts.Select(p => p.Title))
            });
        }

        // Create
        public Result Add(CategoryModel model)
        {
            if (_db.Categories.Any(c => c.CategoryName.ToLower() == model.CategoryName.ToLower().Trim()))
                return new ErrorResult("Category with the same name exists!");
            Category entity = new Category()
            {
                CategoryName = model.CategoryName.Trim()
            };
            _db.Categories.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Category added successfully.");
        }

        // Update
        public Result Update(CategoryModel model)
        {
            if (_db.Categories.Any(c => c.Id != model.Id && c.CategoryName.ToLower() == model.CategoryName.ToLower().Trim()))
                return new ErrorResult("Category with the same name exists!");
            Category entity = _db.Categories.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Category not found!");
            entity.CategoryName = model.CategoryName.Trim();
            _db.Categories.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Category updated successfully.");
        }

        // Delete
        public Result Delete(int id)
        {
            Category entity = _db.Categories.Find(id);
            if (entity is null)
                return new ErrorResult("Category not found!");
            _db.Categories.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Category deleted successfully.");
        }
    }
}
