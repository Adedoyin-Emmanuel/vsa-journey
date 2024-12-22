namespace vsa_journey.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task CompleteAsync();
}