using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;

namespace Restaurant.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    internal DbSet<T> dbSet;
    
    public Repository(AppDbContext db)
    {
        _db = db;
        this.dbSet = db.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>,IOrderedQueryable<T>>? orderby = null,string? includeProperties=null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties !=null)
        {
            foreach (var includeProperty in includeProperties.Split(
                         new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderby!=null)
        {
            return orderby(query).ToList();
        }
        return query.ToList();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties=null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties !=null)
        {
            foreach (var includeProperty in includeProperties.Split(
                         new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query.FirstOrDefault();
    }
}