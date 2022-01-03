using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EcommerceCore.IRepository
{
    /// <summary>
    /// IGenericRepository will have insert,get,getall,update,delete methods that we will then implement 
    /// in GenericRepository. Make this IGenericRepository take a generic parameter T. Where T will be class
    /// its basically saying that it will take any type of class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T: class
    {
        //return IList<T> class objects.Expression represents lambda expression
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        Task<T> Get(Expression<Func<T, bool>> expression = null,
            string includeProperties = "");
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
