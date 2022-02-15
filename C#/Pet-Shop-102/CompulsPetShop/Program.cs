using System;
using System.Collections.Generic;
using PetShop.Core.DomainSerivice;
using PetShop.Infrastructure.Data.Repositories;
using PetShop.Core.Entity;
using PetShop.Core.ApplicationService;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.AplicationService;

namespace CompulsPetShop
{
    class Program
    {
        static void Main(string[] args)
        { 
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IPrinter, Printer>();

            var serviceProvider = serviceCollection.BuildServiceProvider();


            var printer = serviceProvider.GetRequiredService<IPrinter>();
         
            printer.SwitchMenu();
            
            
        }
    }
}
