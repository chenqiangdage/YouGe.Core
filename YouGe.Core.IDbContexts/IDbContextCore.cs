﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using YouGe.Core.Models;
using YouGe.Core.DBEntitys;

namespace YouGe.Core.Interface.IDbContexts
{
    public interface IDbContextCore : IDisposable
    {
        DbContextOption Option { get; }
        DatabaseFacade GetDatabase();
        int Add<T>(T entity, bool useTran) where T : class;

        Task<int> AddAsync<T>(T entity, bool useTran) where T : class;
        int AddRange<T>(ICollection<T> entities, bool useTran) where T : class;
        Task<int> AddRangeAsync<T>(ICollection<T> entities, bool useTran) where T : class;
        int Count<T>(Expression<Func<T, bool>> @where = null) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;
        int Delete<T, TKey>(TKey key, bool useTran) where T : BaseModel<TKey>;
        bool EnsureCreated();
        Task<bool> EnsureCreatedAsync();
        int ExecuteSqlWithNonQuery(string sql, params object[] parameters);
        Task<int> ExecuteSqlWithNonQueryAsync(string sql, params object[] parameters);
        int Edit<T>(T entity, bool useTran) where T : class;
        int EditRange<T>(ICollection<T> entities, bool useTran) where T : class;
        bool Exist<T>(Expression<Func<T, bool>> @where = null) where T : class;
        IQueryable<T> FilterWithInclude<T>(Func<IQueryable<T>, IQueryable<T>> include, Expression<Func<T, bool>> where) where T : class;
        Task<bool> ExistAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;
        T Find<T>(object key) where T : class;
        T Find<T, TKey>(TKey key) where T : BaseModel<TKey>;
        Task<T> FindAsync<T>(object key) where T : class;
        Task<T> FindAsync<T, TKey>(TKey key) where T : BaseModel<TKey>;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> @where = null, bool asNoTracking = false) where T : class;
        List<IEntityType> GetAllEntityTypes();
        DbSet<T> GetDbSet<T>() where T : class;
        T GetSingleOrDefault<T>(Expression<Func<T, bool>> @where = null) where T : class;
        Task<T> GetSingleOrDefaultAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;
        int Update<T>(T model, bool useTran = false, params string[] updateColumns) where T : class;
        int Update<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory, bool useTran) where T : class;
        Task<int> UpdateAsync<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory, bool useTran)
            where T : class;
        int Delete<T>(Expression<Func<T, bool>> @where, bool useTran) where T : class;
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> @where, bool useTran) where T : class;
        void BulkInsert<T>(IList<T> entities, string destinationTableName = null)
            where T : class;
        List<TView> SqlQuery<T, TView>(string sql, params object[] parameters)
            where T : class;
        PaginationResult SqlQueryByPagnation<T, TView>(string sql, string[] orderBys, int pageIndex, int pageSize, Action<TView> eachAction = null)
            where T : class
            where TView : class;
        Task<List<TView>> SqlQueryAsync<T, TView>(string sql, params object[] parameters)
            where T : class
            where TView : class;
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));

        DataTable GetDataTable(string sql, params DbParameter[] parameters);
        List<DataTable> GetDataTables(string sql, params DbParameter[] parameters);
        T GetByCompileQuery<T, TKey>(TKey id) where T : BaseModel<TKey>;
        Task<T> GetByCompileQueryAsync<T, TKey>(TKey id) where T : BaseModel<TKey>;
        IList<T> GetByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        Task<List<T>> GetByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        T FirstOrDefaultByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        Task<T> FirstOrDefaultByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        T FirstOrDefaultWithTrackingByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        Task<T> FirstOrDefaultWithTrackingByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        int CountByCompileQuery<T>(Expression<Func<T, bool>> filter) where T : class;
        Task<int> CountByCompileQueryAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        public List<T> SelectBySql<T>(string sql, params object[] parms) where T : class, new();
    }
}