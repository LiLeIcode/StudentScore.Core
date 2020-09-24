using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentScore.IRepository
{
    public interface IBaseRepository<T> where T:class
    {
        Task<T> QueryById(long id);

        Task<long> Add(T t);

        Task<bool> DeleteById(long id);
        Task<bool> DeleteByObj(T t);

        Task<bool> Update(T t);

        IQueryable<T> QueryAll();

        IQueryable<T> QueryExp(Expression<Func<T, bool>> whereExpression);
    }
}
