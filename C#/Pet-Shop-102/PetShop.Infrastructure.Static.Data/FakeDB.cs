using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    class FakeDB
    {
        public static int PetId = 1;
        public static List<Pet> petList = new List<Pet>();

        public static int OwnerId = 1;
        public static List<Owner> OwnerList = new List<Owner>();
    }
}
