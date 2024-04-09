namespace Intex2024.Data
{
    public class EFIntexRepository : IIntexRepository
    {

        private IntexDbContext _context;
        public EFIntexRepository(IntexDbContext ctx)
        {
            _context = ctx;
        }

        /* public IQueryable<Project> Projects => _context.Projects;*/
    }
}
