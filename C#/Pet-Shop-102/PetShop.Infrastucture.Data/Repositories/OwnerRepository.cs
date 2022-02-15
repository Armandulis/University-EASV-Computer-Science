using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastucture.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        readonly PetShopContext _ctx;

        public OwnerRepository(PetShopContext ctx) {
            _ctx = ctx;
        }
        public Owner GetByID(int id)
        {
            var owner = _ctx.Owner.Include(own => own.OwnedPets).FirstOrDefault(own => own.ID == id);
            return owner;
        }

        public IEnumerable<Owner> ListOwner()
        {
            return _ctx.Owner.Include(owner => owner.OwnedPets);
        }

        public Owner OwnerCreate(Owner owner)
        {
            var own = _ctx.Owner.Add(owner).Entity;
            _ctx.SaveChanges();
            return own;
        }

        public Owner OwnerRemove(int id)
        {
            var own = _ctx.Owner.Remove(GetByID(id)).Entity;
            _ctx.SaveChanges();
            return own;
        }

        public Owner OwnerSearch(string ownerName)
        {
            return _ctx.Owner.Include(owner => owner.OwnedPets).FirstOrDefault(own => own.FirstName == ownerName);
        }

        public Owner OwnerUpdate(Owner owner)
        {
            _ctx.Owner.Update(owner);
            _ctx.SaveChanges();
            return owner;
        }
    }
} 
