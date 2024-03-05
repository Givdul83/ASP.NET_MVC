namespace Infrastructure.Entities;

public class AddressEntity
{
    public int Id { get; set; }

    public string AddressLine { get; set; } = null!;

    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public ICollection<UserEntity> Users { get; set; } = [];
}
