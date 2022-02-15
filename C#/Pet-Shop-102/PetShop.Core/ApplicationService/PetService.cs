using PetShop.Core.AplicationService;
using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public class PetService : IPetService
    {
        readonly IPetRepository _petRepo;
        readonly IOwnerRepository _ownerRepo;

        public PetService(IPetRepository petRepo, IOwnerRepository  ownerRepo)
        {
            _petRepo = petRepo;
            _ownerRepo = ownerRepo;
        }
        

        public Pet CreatePet(Pet pet)
        {
            return _petRepo.PetCreate(pet);
        }

        public Pet FindByIDWithOwner(int id)
        {
            var pet = FindPetByID(id);
            pet.PreviousOwner = _ownerRepo.ListOwner().ToList().Find(own => own.ID == pet.PreviousOwner.ID);
            return pet;
        }

        public Pet FindPetByID(int id)
        {
            return _petRepo.GetByID(id);
        }

        public List<Pet> GetAllPets()
        {
            return _petRepo.ListPets().ToList();
        }

        public Pet NewPet(string name, string type, string birthday, string soldDate, string color, Owner previousOwner, double price)
        {
            var newPet = new Pet
            {
                Name = name,
                Type = type,
                Birthdate = new DateTime(2005, 5, 6),
                SoldDate = new DateTime(2007, 6, 5),
                Color = color,
                PreviousOwner = previousOwner,
                Price = price
            };
            return _petRepo.PetCreate(newPet);
        }
        

        public List<Pet> OrderByPrice()
        {
            var list = _petRepo.ListPets().OrderBy(pet => pet.Price);
            return list.ToList();
        }

        public Pet RemovePet(int pet)
        {
           return  _petRepo.PetRemove(pet);
        }

        public Pet SearchForPet(string name)
        {
            return _petRepo.PetSearch(name);
        }

        public Pet UpdatePet(int id, string name, string type, double price)
        {
            var petToUpdate =  FindPetByID(id);

            petToUpdate.Name = name;
            petToUpdate.Type = type;
            petToUpdate.Price = price;

             return _petRepo.PetUpdate(petToUpdate); 
        }
        
    }
}
