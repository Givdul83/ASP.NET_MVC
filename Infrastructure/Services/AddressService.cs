using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository)
{
   private readonly AddressRepository _addressRepository = addressRepository;



    public async Task<AddressEntity> CreateAddressAsync(AddressEntity address)
    {
        try
        {
            var addressEntity = await _addressRepository.GetOneAsync(x => x.AddressLine == address.AddressLine && x.City == address.City
            && x.PostalCode == address.PostalCode);
            if (addressEntity == null)
            {
                var newAddressEntity = await _addressRepository.CreateAsync(new AddressEntity
                {
                    AddressLine = address.AddressLine,
                    PostalCode = address.PostalCode,
                    City = address.City,
                });
                if (newAddressEntity != null)
                {

                    return newAddressEntity;
                }
            }

            else
                if (addressEntity != null)
            {
                return addressEntity;
            }

            return null!;
        }


        catch (Exception ex)
        {
            Debug.WriteLine(" ERROR CreateAddressAsync " + ex.Message);
            return null!;
        }
    }

    public async Task<AddressEntity> UpdateAddressAsync(AddressEntity address)
    {
        try
        {
            var addressToUpdate = await _addressRepository.GetOneAsync(x => x.AddressLine == address.AddressLine && x.City == address.City
                && x.PostalCode == address.PostalCode);

            if (addressToUpdate == null)
            {
                var newAddress = await CreateAddressAsync(address);

               
                return newAddress;


            }
            else
            {


                var updatedAddress = await _addressRepository.UpdateAsync(x => x.Id == addressToUpdate.Id, addressToUpdate);

               
                return updatedAddress;
            }

        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error UpdateAddressAsync " + ex.Message);
            return null!;
        }
    }
}
