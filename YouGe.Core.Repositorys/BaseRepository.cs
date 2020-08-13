using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using YouGe.Core.Commons;
using YouGe.Core.DBEntitys;
using YouGe.Core.Interface.IDbContexts;
using YouGe.Core.Interface.IRepositorys;
using YouGe.Core.Models;

namespace YouGe.Core.Repositorys
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : BaseModel<TKey>
    {
        protected readonly IDbContextCore DbContext;      

        protected DbSet<T> DbSet => DbContext.GetDbSet<T>();

        protected BaseRepository(IDbContextCore dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbContext.EnsureCreated();
        }         

        #region Insert

        public virtual int Add(T entity, bool useTran = false)
        {
            return DbContext.Add(entity, useTran);
        }


        public virtual async Task<int> AddAsync(T entity, bool useTran = false)
        {
            return await DbContext.AddAsync(entity, useTran);
        }

        public virtual int AddRange(ICollection<T> entities, bool useTran = false)
        {
            return DbContext.AddRange(entities, useTran);
        }

        public virtual async Task<int> AddRangeAsync(ICollection<T> entities, bool useTran = false)
        {
            return await DbContext.AddRangeAsync(entities, useTran);
        }

        public virtual void BulkInsert(IList<T> entities, string destinationTableName = null)
        {
            DbContext.BulkInsert<T>(entities, destinationTableName);
        }

        public int AddBySql(string sql, bool useTran = false)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql);
        }

        public int AddBySql(string sql, bool useTran = false, params object[] parameters)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql, parameters);
        }
        #endregion

        #region Update

        public int DeleteBySql(string sql, bool useTran = false)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql);
        }

        public int DeleteBySql(string sql, bool useTran = false, params object[] parameters)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql, parameters);
        }
        public virtual int Edit(T entity, bool useTran = false)
        {
            return DbContext.Edit<T>(entity, useTran);
        }

        public virtual int EditRange(ICollection<T> entities, bool useTran = false)
        {
            return DbContext.EditRange(entities, useTran);
        }
        /// <summary>
        /// update query datas by columns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        public virtual int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp, bool useTran = false)
        {
            return DbContext.Update(where, updateExp, useTran);
        }

        public virtual async Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp, bool useTran = false)
        {
            return await DbContext.UpdateAsync(@where, updateExp, useTran);
        }
        public virtual int Update(T model, bool useTran = false, params string[] updateColumns)
        {
            DbContext.Update(model, useTran, updateColumns);
            return DbContext.SaveChanges();
        }

        public virtual int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory, bool useTran = false)
        {
            return DbContext.Update(where, updateFactory, useTran);
        }

        public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory, bool useTran = false)
        {
            return await DbContext.UpdateAsync(where, updateFactory, useTran);
        }

        public int UpdateBySql(string sql, bool useTran = false)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql);
        }
        public int UpdateBySql(string sql, bool useTran = false, params object[] parameters)
        {
            return DbContext.ExecuteSqlWithNonQuery(sql, parameters);
        }
        #endregion

        #region Delete

        public virtual int Delete(TKey key, bool useTran = false)
        {
            return DbContext.Delete<T, TKey>(key, useTran);
        }

        public virtual int Delete(Expression<Func<T, bool>> @where, bool useTran = false)
        {
            return DbContext.Delete(where, useTran);
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> @where, bool useTran = false)
        {
            return await DbContext.DeleteAsync(where, useTran);
        }


        #endregion

        #region Query

        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.Count(where);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.CountAsync(where);
        }


        public virtual bool Exist(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.Exist(where);
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.ExistAsync(where);
        }

        /// <summary>
        /// 根据主键获取实体。建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T GetSingle(TKey key)
        {
            return DbContext.Find<T, TKey>(key);
        }

        public T GetSingle(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        {
            if (includeFunc == null) return GetSingle(key);
            return includeFunc(DbSet.Where(m => m.Id.Equal(key))).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// 根据主键获取实体。建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleAsync(TKey key)
        {
            return await DbContext.FindAsync<T, TKey>(key);
        }

        /// <summary>
        /// 获取单个实体。建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        public virtual T GetSingleOrDefault(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.GetSingleOrDefault(@where);
        }

        /// <summary>
        /// 获取单个实体。建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.GetSingleOrDefaultAsync(where);
        }

        /// <summary>
        /// 获取实体列表。建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        public virtual IList<T> Get(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.GetByCompileQuery(where);
        }

        /// <summary>
        /// 获取实体列表。建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.GetByCompileQueryAsync(where);
        }

        /// <summary>
        /// 分页获取实体列表。建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        public virtual IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] @orderby)
        {
            var filter = DbContext.Get(where);
            if (orderby != null)
            {
                foreach (var func in orderby)
                {
                    filter = asc ? filter.OrderBy(func).AsQueryable() : filter.OrderByDescending(func).AsQueryable();
                }
            }
            return filter.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        public List<T> GetBySql(string sql)
        {
            return DbContext.SqlQuery<T, T>(sql);
        }

        public List<T> GetBySql(string sql, params object[] parms)
        {
            return DbContext.SqlQuery<T, T>(sql, parms);
        }

        public List<TView> GetViews<TView>(string sql)
        {
            var list = DbContext.SqlQuery<T, TView>(sql);
            return list;
        }


        public List<TView> GetViews<TView>(string viewName, Func<TView, bool> @where)
        {
            var list = DbContext.SqlQuery<T, TView>($"select * from {viewName}");
            if (where != null)
            {
                return list.Where(where).ToList();
            }

            return list;
        }

        #endregion

        public void Dispose()
        {
            DbContext?.Dispose();
        }

        public IDbContextCore GetDBContext()
        {
            return this.DbContext;
        }

        public int ExecuteSql(string sql, params object[] parameters)
        {
            return this.DbContext.GetDatabase().ExecuteSqlCommand(sql, parameters);
        }

        public Task<int> ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return this.DbContext.GetDatabase().ExecuteSqlCommandAsync(sql, parameters);
        }
    }
}

