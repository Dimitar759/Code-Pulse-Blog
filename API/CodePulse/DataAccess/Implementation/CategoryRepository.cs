using DataAccess.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext _dbContext;

        public CategoryRepository (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Category entity)
        {
            _dbContext.Categories.Add(entity);
            _dbContext.SaveChanges();//call to db
        }

        public void Delete(Category entity)
        {
            _dbContext.Categories.Remove(entity);
            _dbContext.SaveChanges();//call to db
        }

        public List<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetById(Guid id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Category entity)
        {
            _dbContext.Categories.Update(entity);
            _dbContext.SaveChanges(); 

        }
    }
}
