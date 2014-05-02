using System;

namespace Furnace.Items
{
    public interface IFurnaceItemRepository<T>
    {
        //void Insert(T entity);
        //void Delete(Item entity);
        //IQueryable<Item> SearchFor(Expression<Func<Item, bool>> predicate);
        //IQueryable<Item> GetAll();
        T GetById(Guid id);
    }
}
