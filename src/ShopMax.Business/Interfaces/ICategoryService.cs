using ShopMax.Business.Models;
using System.Linq.Expressions;

namespace ShopMax.Business.Interfaces;

public interface ICategoryService
{
	Task<IEnumerable<Category>> GetAll();
	Task<Category> GetById(int id);
	Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate);
	Task Add(Category entity);
	Task Update(Category entity);
	Task Delete(int id);
}
