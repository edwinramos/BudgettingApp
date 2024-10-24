using System;
using System.Linq.Expressions;

namespace BudgettingApp.Interfaces
{

    /// <summary>
    /// Interface for caching providers - all providers should support expiring of caching items, all data add must be a reference type.
    /// </summary>
    public interface ILocalDbService<T>
    {
        public IEnumerable<T> GetAll();

        public T Get(Expression<Func<T, bool>> expression);

        public bool Add(T obj);

        public bool Update(T person);

        public bool Delete(string id);
    }
}
