using Microsoft.EntityFrameworkCore;

namespace GamesLib.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        _dbContext.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _dbContext.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _dbContext.Database.RollbackTransaction();
    }
}