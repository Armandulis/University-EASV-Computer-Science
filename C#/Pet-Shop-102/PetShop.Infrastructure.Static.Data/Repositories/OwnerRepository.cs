using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        
        public OwnerRepository() {
            if (FakeDB.OwnerList.Count == 0) {
                AddDefaultOwners();
            }
        }

        void AddDefaultOwners() {

            var owner1 = new Owner
            {
                ID = FakeDB.OwnerId++,
                FirstName = "Franku",
                LastName = "JOju",
                Address = "Bomziuku street",
                Email = "Waifu@gmail.com",
                PhoneNumber = "69596959"

            };
            FakeDB.OwnerList.Add(owner1);

            var owner2 = new Owner
            {
                ID = FakeDB.OwnerId++,
                FirstName = "Armandas",
                LastName = "lol",
                Address = "wat street",
                Email = "businesslmao@gmail.com",
                PhoneNumber = "no phone mate phone"

            };
            FakeDB.OwnerList.Add(owner2);

            var owner3 = new Owner
            {
                ID = FakeDB.OwnerId++,
                FirstName = "Doggu",
                LastName = "Kattu",
                Address = "Cats street",
                Email = "Gmail@gmail.com",
                PhoneNumber = "Phony phone"


            };
            FakeDB.OwnerList.Add(owner3);
        }

        public Owner GetByID(int id)
        {

            return FakeDB.OwnerList.Select(owner => new Owner {
                ID = owner.ID,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Address = owner.Address,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber
            }).FirstOrDefault(o => o.ID == id);
        }

        public IEnumerable<Owner> ListOwner()
        {
            return FakeDB.OwnerList;
        }

        public Owner OwnerCreate(Owner owner)
        {
            owner.ID = FakeDB.OwnerId++;
            FakeDB.OwnerList.Add(owner);
            return owner;

        }

        public Owner OwnerRemove(int id)
        {
            var ownerToRemove = FakeDB.OwnerList.Find( o =>  o.ID == id);
            if (ownerToRemove != null) {
                FakeDB.OwnerList.Remove(ownerToRemove);
                return ownerToRemove;
            }
            else return ownerToRemove;
        }

        public Owner OwnerSearch(string ownerName)
        {
            return FakeDB.OwnerList.Find(o => o.FirstName == ownerName);
        }

        public Owner OwnerUpdate(Owner owner)
        {
            var ownerUpdate = FakeDB.OwnerList.Find( o => o.ID == owner.ID);
            if (ownerUpdate != null) {
                ownerUpdate.FirstName = owner.FirstName;
                ownerUpdate.LastName = owner.LastName;
                ownerUpdate.PhoneNumber = owner.PhoneNumber;
                ownerUpdate.Address = owner.Address;
                ownerUpdate.Email = owner.Email;
                return ownerUpdate;
            }
           else return ownerUpdate; 
        }
    }
}
