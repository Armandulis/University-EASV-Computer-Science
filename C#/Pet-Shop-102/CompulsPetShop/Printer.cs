using PetShop.Core.AplicationService;
using PetShop.Core.ApplicationService;
using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using PetShop.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompulsPetShop
{
    

    public class Printer : IPrinter
    {
        readonly IPetService _petService;
        

        public Printer(IPetService petService)
        {
            _petService = petService;

        }

     

        public void SwitchMenu()
        {
            Console.WriteLine("                              ");
            Console.WriteLine("-----------------------------|");
            Console.WriteLine("PetShop Menu:                |");
            Console.WriteLine("1: List all viable Pets      |");
            Console.WriteLine("2: Change pet's information  |");
            Console.WriteLine("3: Remove pet                |");
            Console.WriteLine("4: Add pet                   |");
            Console.WriteLine("5: search for a pet by name  |");
            Console.WriteLine("6: search for a pet by id    |");
            Console.WriteLine("7: List pets by price        |");
            Console.WriteLine("8: List first 5 cheapst pets |");
            Console.WriteLine("-----------------------------|");
            Console.WriteLine("                              ");

            int selectedMenu;
            while (!int.TryParse(Console.ReadLine(), out selectedMenu))
            {
                Console.WriteLine("Please insert a number");
            }

            switch (selectedMenu)
            {

                //List all pets
                case 1: {
                        Console.WriteLine("Listing pets: ");
                        ListPets();
                        break; }

                //Change pet's info
                case 2: {
                        int id;
                        while (!int.TryParse(AskQuestion("Please enter the id of a pet that you want to change"), out id))
                        {
                            Console.WriteLine("Please insert a number");
                        }
                        var name = AskQuestion("Please enter pet's new name");
                        var type = AskQuestion("Please enter pet's new type");

                        double priceOfPet;
                        while (!double.TryParse(AskQuestion("Please enter pet's new price"), out priceOfPet))
                        {
                            Console.WriteLine("Please insert a number");
                        }
                        ChangePetInfo(id, name, type, priceOfPet);

                        break; }

                //Remove pet
                case 3: {
                        int petID;
                        while (!int.TryParse(AskQuestion("Write an ID of a pet you want to delete:"), out petID))
                        {
                            Console.WriteLine("Please insert a number");
                        }

                        
                        RemovePet(petID);
                        break; }

                //Create pet
                case 4: {
                        Console.WriteLine("Adding a pet:");
                        AddPet();
                        
                        break; }

                //search pet
                case 5: {
                        var name = AskQuestion("Search by the Name: ");
                        var foundPet = SearchPet(name);

                        if (foundPet != null)
                        {
                            Console.WriteLine("Pet was found: ");

                            WritePetToUser(foundPet);
                        }
                        else Console.WriteLine("That type of pet was not found");

                        SwitchMenu();
                        break; }
                //search by id
                case 6:
                    {
                        int id;
                        while (!int.TryParse(AskQuestion("Search by the id: "), out id))
                        {
                            Console.WriteLine("Please insert a number");
                        }
                        
                        WritePetToUser(SearchPetByID(id));

                        SwitchMenu();
                        break;
                    }

                //List all pets by price
                case 7:
                    {
                        Console.WriteLine("Listing pets: ");
                        OrderPetsByPrice();
                        break;
                    }

                case 8:
                    {
                        Console.WriteLine("Listing pets: ");
                        OrderPetsByPrice5();
                        break;
                    }

                default: { Console.WriteLine("option is not valid"); break; }
            }
        }

        void OrderPetsByPrice() {
           
            foreach (var pet in _petService.OrderByPrice())
            {
                WritePetToUser(pet);
            }
            SwitchMenu();
        }

        void OrderPetsByPrice5()
        {

            foreach (var pet in _petService.OrderByPrice().Take(5))
            { 
                WritePetToUser(pet);
              
            }
            SwitchMenu();
        }

        void ChangePetInfo(int id, string name, string type, double price)
        {  
            _petService.UpdatePet(id, name, type, price);
            SwitchMenu();
        }

        void RemovePet(int petID)
        {
            _petService.RemovePet(petID);


            SwitchMenu();
        }


        void ListPets()
        {
            foreach (var pet in _petService.GetAllPets())
            {
                WritePetToUser(pet);
            }
            
            SwitchMenu();

        }
        Pet SearchPet(String name)
        {
           return _petService.SearchForPet(name); 
           
        }


        Pet SearchPetByID(int id)
        {
            return _petService.FindPetByID(id);

        }

        void AddPet()
        {
            var newPetName = AskQuestion("Enter pet's Name:");
            var newPetType = AskQuestion("enter pet's type");
            var newPetBirthDay = AskQuestion("Enter pet's Birthday:");
            var newPetSoldDate = AskQuestion("enter pet's sold date");
            var newPetColor = AskQuestion("Enter pet's color:");
           
            var newPetPrice = double.Parse(AskQuestion("enter pet's price"));

            //_petService.NewPet(newPetName, newPetType, newPetBirthDay, newPetSoldDate, newPetColor,  newPetPrice);

             SwitchMenu();
        }

        void WritePetToUser(Pet pet)
        {

            Console.WriteLine("ID: {0}, Name: {1}, Type: {2}, Birth date: {3}, Sold date: {4}," +
                    " Color: {5}, Previous Owner: {6}, Price: {7} ", pet.ID, pet.Name, pet.Type,
                    pet.Birthdate, pet.SoldDate, pet.Color, pet.PreviousOwner, pet.Price);
        }

        string AskQuestion(String message)
        {

            Console.WriteLine(message);
            var usersInput = Console.ReadLine();
            return usersInput;
        }
    }
}

