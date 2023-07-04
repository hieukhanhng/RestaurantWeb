using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Repository;

public class AppUserRepository : Repository<AppUser>, IAppUserRepository
{
    private readonly AppDbContext _db;
    
    public AppUserRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

}