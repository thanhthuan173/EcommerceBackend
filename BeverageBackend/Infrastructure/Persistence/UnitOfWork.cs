using BeverageBackend.Application.Interfaces;

namespace BeverageBackend.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BeverageDbContext _context;

        public UnitOfWork(BeverageDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
