using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Core.DomainSerivice
{ 
    public interface IPetRepository
    {
        Pet PetCreate(Pet pet);
        IEnumerable<Pet> ListPets();
        Pet PetUpdate(Pet pet);
        Pet PetRemove(int id);
        Pet PetSearch(string petName);
        Pet GetByID(int id);

    }
}
