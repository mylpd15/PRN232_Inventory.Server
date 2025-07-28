// IGenericRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WareSync.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Xoá thực thể và trả về chính thực thể đó (nếu cần).
        /// </summary>
        Task<T> Remove(T entity);

        /// <summary>
        /// Tách entity khỏi ChangeTracker (nếu dùng caching hoặc track nhiều context).
        /// </summary>
        void Detach(T entity);

        /// <summary>
        /// Cho phép query thêm các filter, include, skip/take… trước khi ToList/FirstAsync.
        /// </summary>
        IQueryable<T> Query();

        /// <summary>
        /// Lưu mọi thay đổi đang pending
        /// </summary>
        Task SaveChangesAsync();
    }
}
