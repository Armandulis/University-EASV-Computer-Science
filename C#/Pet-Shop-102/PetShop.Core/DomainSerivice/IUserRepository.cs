using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainSerivice
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id); 
        void Add(User entity);
        void Edit(User entity);
        void Remove(int id);
    }
}
