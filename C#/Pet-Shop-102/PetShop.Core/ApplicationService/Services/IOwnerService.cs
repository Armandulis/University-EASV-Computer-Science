using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService.Services
{
    public interface IOwnerService
    {
        //CRUD

        Owner CreateOwner(string FirstName, string LastName, string Address, string PhoneNumber, string Email);
        Owner NewOwner(Owner owner);
        Owner DeleteOwner(int id);
        Owner UpdateOwner(int id, string FirstName, string LastName, string Address, string PhoneNumber, string Email);
        List<Owner> ReadAllOwners();
        Owner FindOwnderByID(int id);
        List<Pet> FindOwnedPets(int id);
    }
}
