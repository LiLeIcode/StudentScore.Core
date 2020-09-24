using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class BaseRepository<T>:IBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly IUnitWork _unitWork;

        //需要封装一个sql接口，也就是context可以直接使用models层的context，也可以封装到unitwork，也可以新建一个sql模型操作类并且实现sql的接口
        private readonly StudentScoreContext _db;

        public BaseRepository(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            _db = _unitWork.GetDbContext();
        }
        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> QueryById(long id)
        {
            return await _db.Set<T>().SingleAsync(x => x.ID == id && !x.IsRemove);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<long> Add(T t)
        {
            EntityEntry<T> entityEntry = await _db.Set<T>().AddAsync(t);
            await _db.SaveChangesAsync();
            return entityEntry.Entity.ID;
        }
        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(long id)
        {
            T t = new T() {ID = id};
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemove = true;
            int saveResult = await _db.SaveChangesAsync();
            return saveResult > 0 ? true : false;
        }
        /// <summary>
        /// 根据对象删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByObj(T t)
        {
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemove = true;
            int saveResult = await _db.SaveChangesAsync();
            return saveResult > 0 ? true : false;
        }
        /// <summary>
        /// 根据对象修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> Update(T t)
        {
            _db.Entry(t).State = EntityState.Modified;
            int saveResult = await _db.SaveChangesAsync();
            return saveResult > 0 ? true : false;
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> QueryAll()
        {
            return _db.Set<T>().Where(x => !x.IsRemove).AsNoTracking();
        }

        public IQueryable<T> QueryExp(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Set<T>().Where(whereExpression);
        }

       
    }
}
