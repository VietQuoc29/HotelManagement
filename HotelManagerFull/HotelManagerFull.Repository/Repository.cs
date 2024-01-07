using Dapper;
using EFCore.BulkExtensions;
using HotelManagerFull.Share.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelManagerFull.Repository
{
    /// <summary>
    /// IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// GetAllUsingDapper
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllUsingDapper(string storedProcedure);

        /// <summary>
        /// GetByIdUsingDapper
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        Task<T> GetByIdUsingDapper(long id, string storedProcedure);

        /// <summary>
        /// InsertUsingDapper
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        Task<bool> InsertUsingDapper(string storedProcedure, object parms);

        /// <summary>
        /// BulkInsertUsingDapper
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        Task<bool> BulkInsertUsingDapper(string storedProcedure, object parms);

        /// <summary>
        /// UpdateUsingDapper
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        Task<bool> UpdateUsingDapper(T obj, string storedProcedure, object parms);

        /// <summary>
        /// DeleteUsingDapper
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        Task<bool> DeleteUsingDapper(long id, string storedProcedure);

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// GetAllNoneDeleted
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllNoneDeleted();

        /// <summary>
        /// AddRange
        /// </summary>
        /// <param name="entity"></param>
        void AddRange(List<T> entity);

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// CountAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// GetSingleNoneDeletedAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// GetSingleAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// GetSingleNoneDeleted
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetSingleNoneDeleted(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// GetSingle
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// GetSingleAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// GetSingle
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// FindBy
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="predicate"></param>
        void DeleteWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        bool Commit();

        /// <summary>
        /// CommitAsync
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();

        /// <summary>
        /// BulkInsert
        /// </summary>
        /// <param name="items"></param>
        void BulkInsert(IList<T> items);

        /// <summary>
        /// BulkUpdate
        /// </summary>
        /// <param name="items"></param>
        void BulkUpdate(IList<T> items);

        /// <summary>
        /// BulkDelete
        /// </summary>
        /// <param name="items"></param>
        void BulkDelete(IList<T> items);
    }

    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _context;

        /// <summary>
        /// Repository
        /// </summary>
        /// <param name="unitOfWork"></param>
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = unitOfWork.DbContext;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _context.Entry(entity);
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// AddRange
        /// </summary>
        /// <param name="entity"></param>
        public void AddRange(List<T> entity)
        {
            _context.Set<T>().AddRange(entity);
        }

        /// <summary>
        /// BulkDelete
        /// </summary>
        /// <param name="items"></param>
        public virtual void BulkDelete(IList<T> items) => _context.BulkDelete(items);

        /// <summary>
        /// BulkInsert
        /// </summary>
        /// <param name="items"></param>
        public virtual void BulkInsert(IList<T> items) => _context.BulkInsert(items);

        /// <summary>
        /// BulkInsertUsingDapper
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task<bool> BulkInsertUsingDapper(string storedProcedure, object parms)
        {
            await _unitOfWork.Connection.ExecuteAsync
                 (storedProcedure, parms, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return true;
        }

        /// <summary>
        /// BulkUpdate
        /// </summary>
        /// <param name="items"></param>
        public virtual void BulkUpdate(IList<T> items) => _context.BulkUpdate(items);

        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            var recordsCommittedCount = _context.SaveChanges();
            return (recordsCommittedCount > 0);
        }

        /// <summary>
        /// CommitAsync
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommitAsync()
        {
            var recordsCommittedCount = await _context.SaveChangesAsync();
            return (recordsCommittedCount > 0);
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _context.Set<T>().Count();
        }

        /// <summary>
        /// CountAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <summary>
        /// DeleteUsingDapper
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUsingDapper(long id, string storedProcedure)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            await _unitOfWork.Connection.ExecuteAsync
                (storedProcedure, p, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return true;
        }

        /// <summary>
        /// DeleteWhere
        /// </summary>
        /// <param name="predicate"></param>
        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// FindBy
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.AsQueryable();
        }

        /// <summary>
        /// GetAllNoneDeleted
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAllNoneDeleted()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.AsQueryable();
        }

        /// <summary>
        /// GetAllUsingDapper
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllUsingDapper(string storedProcedure)
        {
            var entities = await _unitOfWork.Connection.QueryAsync<T>
                 (storedProcedure, commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// GetByIdUsingDapper
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public async Task<T> GetByIdUsingDapper(long id, string storedProcedure)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entity = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<T>
                (storedProcedure, p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// GetSingle
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// GetSingle
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// GetSingleAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        /// <summary>
        /// GetSingleAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// GetSingleNoneDeleted
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetSingleNoneDeleted(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// GetSingleNoneDeletedAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// InsertUsingDapper
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task<bool> InsertUsingDapper(string storedProcedure, object parms)
        {
            await _unitOfWork.Connection.ExecuteAsync
             (storedProcedure, parms, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// UpdateUsingDapper
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUsingDapper(T obj, string storedProcedure, object parms)
        {
            await _unitOfWork.Connection.ExecuteAsync
              (storedProcedure, parms, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return true;
        }
    }
}
