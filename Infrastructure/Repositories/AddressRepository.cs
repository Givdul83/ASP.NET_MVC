
using Infrastructure.Contexts;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AddressRepository(DataContext context) : BaseRepo<AddressEntity, DataContext>(context)
{
    private readonly DataContext _context = context;

    public override Task<AddressEntity> CreateAsync(AddressEntity entity)
    {
        return base.CreateAsync(entity);
    }

    public override Task<bool> DeleteAsync(Expression<Func<AddressEntity, bool>> expression)
    {
        return base.DeleteAsync(expression);
    }

    public override Task<bool> ExistAsync(Expression<Func<AddressEntity, bool>> expression)
    {
        return base.ExistAsync(expression);
    }

    public override Task<IEnumerable<AddressEntity>> GetAllAsync()
    {
        return base.GetAllAsync();
    }

    public override Task<AddressEntity> GetOneAsync(Expression<Func<AddressEntity, bool>> expression)
    {
        return base.GetOneAsync(expression);
    }

    public override Task<AddressEntity> UpdateAsync(Expression<Func<AddressEntity, bool>> expression, AddressEntity updatedEntity)
    {
        return base.UpdateAsync(expression, updatedEntity);
    }
}
