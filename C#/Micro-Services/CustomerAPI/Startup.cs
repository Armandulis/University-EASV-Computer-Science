using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.Data;
using CustomerAPI.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomerAPI
{
    //https://localhost:44396/product
    //https://localhost:44393/api/orders
    //https://localhost:44380/Customers

    public class Startup
    {
        // RabbitMQ connection string
        string cloudAMQPConnectionString =
           "host=macaw.rmq.cloudamqp.com;virtualHost=feucbiev;username=feucbiev;password=M6vR0J4GkImLWm5dQbRzAqxldhcQFk0F";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In-memory database:
            services.AddDbContext<CustomerAPIContext>(opt => opt.UseInMemoryDatabase("CustomersDb"));
            // Register repositories for dependency injection.
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Register database initializer for dependency injection.
            services.AddTransient<IDbInitializer, DbInitializer>();

            //services.AddControllers();
            services.AddControllers(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                // Initialize the database
                var services = scope.ServiceProvider;
                var dbContext = services.GetService<CustomerAPIContext>();
                var dbInitializer = services.GetService<IDbInitializer>();
                dbInitializer.Initializer(dbContext);
            }
            // Create a message listener in a separate thread.
            Task.Factory.StartNew(() =>
               new MessageListener(app.ApplicationServices, cloudAMQPConnectionString).Start());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
