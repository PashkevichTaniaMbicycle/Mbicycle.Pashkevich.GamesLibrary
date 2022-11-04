namespace GamesLib.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    void BeginTransaction();
    
    void CommitTransaction();
    
    void RollbackTransaction();
    
}