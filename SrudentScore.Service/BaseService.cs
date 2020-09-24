using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;

namespace StudentScore.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        public IBaseRepository<T> BaseDal;

        public async Task<T> QueryById(long id)
        {
            return await BaseDal.QueryById(id);
        }

        public async Task<long> Add(T t)
        {
            return await BaseDal.Add(t);
        }

        public async Task<bool> DeleteById(long id)
        {
            return await BaseDal.DeleteById(id);
        }

        public async Task<bool> DeleteByObj(T t)
        {
            return await BaseDal.DeleteByObj(t);
        }

        public async Task<bool> Update(T t)
        {
            return await BaseDal.Update(t);
        }

        public IQueryable<T> QueryAll()
        {
            return BaseDal.QueryAll();
        }

        public IQueryable<T> QueryExp(Expression<Func<T, bool>> whereExpression)
        {
            return BaseDal.QueryExp(whereExpression);
        }
    }
}
