using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.AplicationService
{
    public interface IPetService
    {

        Pet SearchForPet(string name);
        Pet NewPet(string name, string type, string birthday, string soldDate, string color, Owner previousOwner, double price);
        Pet CreatePet(Pet pet);
        Pet RemovePet(int pet);
        Pet UpdatePet(int id, string name, string type, double price);
        List<Pet> GetAllPets();
        Pet FindPetByID(int id);
        List<Pet> OrderByPrice();
        Pet FindByIDWithOwner(int id);
    }
}