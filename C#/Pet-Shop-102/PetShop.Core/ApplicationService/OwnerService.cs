using PetShop.Core.ApplicationService.Services;
using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public class OwnerService : IOwnerService
    {
        readonly IOwnerRepository _ownerRepo;
        readonly IPetRepository _petRepo;

        public OwnerService(IOwnerRepository ownerRepo, IPetRepository petRepo)
        {
            _ownerRepo = ownerRepo;
            _petRepo = petRepo;
        }

        public Owner CreateOwner(string firstName, string lastName, string address, string phoneNumber, string email)
        {
            var owner = new Owner
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email
           };

            return NewOwner(owner);
        }

        public Owner DeleteOwner(int id)
        {
          return  _ownerRepo.OwnerRemove(id);
        }

        public Owner FindOwnderByID(int id)
        {
            var owner =_ownerRepo.GetByID(id);
            return owner;
        }

        public List<Pet> FindOwnedPets(int id)
        {
            return null;
        }

        public Owner NewOwner(Owner owner)
        {
            return _ownerRepo.OwnerCreate(owner);
        }

        public List<Owner> ReadAllOwners()
        {
            return _ownerRepo.ListOwner().ToList();
        }

        public Owner UpdateOwner(int id, string firstName, string lastName, string address, string phoneNumber, string email)
        {
            var owner = new Owner
            {
                ID = id,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email
            };

            return _ownerRepo.OwnerUpdate(owner);
        }
    }
}
