
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class OptionalInfoRepository(DataContext context) : BaseRepo<OptionalInfoEntity, DataContext>(context)
{
    private readonly DataContext _context = context;
}
