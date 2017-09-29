using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassReports.Data.Context;
using CompassReports.Data.Entities;

namespace CompassReports.Data
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        DbSet<T> GetAll();

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }

    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public DbSet<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Remove(T entity)
        {
            if (entity != null) _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
