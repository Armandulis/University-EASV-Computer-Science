using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainSerivice
{
    public interface IOwnerRepository
    {

        Owner OwnerCreate(Owner owner);
        IEnumerable<Owner> ListOwner();
        Owner OwnerUpdate(Owner owner);
        Owner OwnerRemove(int id);
        Owner OwnerSearch(string ownerName);
        Owner GetByID(int id);

    }
}
