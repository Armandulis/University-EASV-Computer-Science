using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainSerivice;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastucture.Data.Repositories
{
   public class UserRepository : IUserRepository
    {
        readonly PetShopContext _ctx;

        public UserRepository(PetShopContext context)
        {
            _ctx = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _ctx.User.ToList();
        }

        public User Get(int id)
        {
            return _ctx.User.FirstOrDefault(b => b.Id == id);
        }

        public void Add(User entity)
        {
            _ctx.User.Add(entity);
            _ctx.SaveChanges();
        }

        public void Edit(User entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Remove(int id)
        {

            var item = _ctx.User.FirstOrDefault(b => b.Id == id);
            _ctx.User.Remove(item);
            _ctx.SaveChanges();
        }

    }
}
