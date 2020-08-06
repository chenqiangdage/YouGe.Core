using YouGe.Core.DBEntitys;
using YouGe.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YouGe.Core.Interface.IDbContexts;

namespace YouGe.Core.Interface.IRepositorys
{
    //[FromDbContextFactoryInterceptor]
    public interface IRepository<T, in TKey> : ITransientDependency, IDisposable where T : IBaseModel<TKey>
    {
        #region Insert

        int Add(T entity, bool useTran = false);
        Task<int> AddAsync(T entity, bool useTran = false);
        int AddRange(ICollection<T> entities, bool useTran = false);
        Task<int> AddRangeAsync(ICollection<T> entities, bool useTran = false);
        void BulkInsert(IList<T> entities, string destinationTableName = null);
        int AddBySql(string sql, bool useTran = false);

        int AddBySql(string sql, bool useTran = false, params object[] parameters);
        #endregion

        #region Delete

        int Delete(TKey key, bool useTran = false);
        int Delete(Expression<Func<T, bool>> @where, bool useTran = false);
        Task<int> DeleteAsync(Expression<Func<T, bool>> @where, bool useTran = false);
        int DeleteBySql(string sql, bool useTran = false);

        int DeleteBySql(string sql, bool useTran = false, params object[] parameters);
        #endregion

        #region Update

        int Edit(T entity, bool useTran = false);
        int EditRange(ICollection<T> entities, bool useTran = false);
        int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp, bool useTran = false);
        Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp, bool useTran = false);
        int Update(T model, bool useTran = false, params string[] updateColumns);
        int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory, bool useTran = false);
        Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory, bool useTran = false);
        int UpdateBySql(string sql, bool useTran = false);
        int UpdateBySql(string sql, bool useTran = false, params object[] parameters);

        #endregion

        #region Query

        int Count(Expression<Func<T, bool>> @where = null);
        Task<int> CountAsync(Expression<Func<T, bool>> @where = null);
        bool Exist(Expression<Func<T, bool>> @where = null);
        Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null);
        T GetSingle(TKey key);
        T GetSingle(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc);
        Task<T> GetSingleAsync(TKey key);
        T GetSingleOrDefault(Expression<Func<T, bool>> @where = null);
        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null);
        IList<T> Get(Expression<Func<T, bool>> @where = null);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null);
        IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true,
            params Func<T, object>[] @orderby);

        List<T> GetBySql(string sql);

        List<T> GetBySql(string sql, params object[] parms);
        List<TView> GetViews<TView>(string sql);
        List<TView> GetViews<TView>(string viewName, Func<TView, bool> where);
        public IDbContextCore GetDBContext();
        int ExecuteSql(string sql, params object[] parameters);

        Task<int> ExecuteSqlAsync(string sql, params object[] parameters);

        #endregion
    }
}
