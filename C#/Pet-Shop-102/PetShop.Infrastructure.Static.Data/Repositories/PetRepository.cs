using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        public PetRepository() {
            if (FakeDB.petList.Count == 0) {
                PetsDefault();
            }
        }
        void PetsDefault() {

            var pet1 = new Pet
            {
                ID = FakeDB.PetId++,
                Name = "Fonald",
                Type = "duck",
                Birthdate = new DateTime(2002, 5, 6),
                SoldDate = new DateTime(2017, 6, 5),
                PreviousOwner = new Owner() { ID=1},
                Color = "Black/White",
                Price = 160.00
            };
            FakeDB.petList.Add(pet1);

            var pet2 = new Pet
            {
                ID = FakeDB.PetId++,
                Name = "Doggo",
                Type = "Dog",
                Birthdate = new DateTime(2018, 5, 6),
                SoldDate = new DateTime(2018, 6, 5),
                PreviousOwner = new Owner() { ID = 1 },
                Color = "Brown",
                Price = 260.00
            };
            FakeDB.petList.Add(pet2);

            var pet3 = new Pet
            {
                ID = FakeDB.PetId++,
                Name = "Horsey",
                Type = "Cat",
                Birthdate = new DateTime(2005, 5, 6),
                SoldDate = new DateTime(2007, 6, 5),
                PreviousOwner = new Owner() { ID = 2 },
                Color = "Black",
                Price = 20
            };
            FakeDB.petList.Add(pet3);

        }

        public Pet GetByID(int id)
        {
            var petSearched = FakeDB.petList.Find(pets => pets.ID == id);
            if (petSearched != null)
            {
                return petSearched;
            }
            else return null;
        }

        public IEnumerable<Pet> ListPets()
        {
            return FakeDB.petList;
        }

        public Pet PetCreate(Pet pet)
        {
            pet.ID = FakeDB.PetId++;
            FakeDB.petList.Add(pet);
            return pet;
        }

        public Pet PetRemove(int id)
        {
            var petToRemove = FakeDB.petList.Find(pet => pet.ID == id);
            if (petToRemove!=null) {
                FakeDB.petList.Remove(petToRemove);
                return petToRemove;
            }
            else return null;

        }
        public Pet PetSearch(String petname)
        {
            var petSearched = FakeDB.petList.Find(pets => pets.Name == petname);
            if (petSearched != null)
            {
                return petSearched;
            }
            else return null;
        }

        public Pet PetUpdate(Pet petUpdate)
        {
            var petFromDB = this.GetByID(petUpdate.ID);
            if (petFromDB != null) {
                petFromDB.Name = petUpdate.Name;
                petFromDB.Type = petUpdate.Type;
                petFromDB.Birthdate = petUpdate.Birthdate;
                petFromDB.SoldDate = petUpdate.SoldDate;
                petFromDB.Color = petUpdate.Color;
                petFromDB.PreviousOwner = petUpdate.PreviousOwner;
                petFromDB.Price = petUpdate.Price;

                return petFromDB;
            }
            else return null;
        }
        
    }
}
