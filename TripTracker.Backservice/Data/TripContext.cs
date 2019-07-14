using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripTracker.Backservice.Models;

namespace TripTracker.Backservice.Data
{
    public class TripContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }

        public TripContext(DbContextOptions options) : base(options) { }

        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider
                     .GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>().CreateScope())
                            {
                                var context = serviceScope
                                              .ServiceProvider.GetService<TripContext>();

                                //Ensure that the Database is created.
                                context.Database.EnsureCreated();

                                if (context.Trips.Any()) return;
                               
                                context.Trips.AddRange( new Trip[]
                                {
                                    new Trip()
                                    {
                                        
                                        Name = "MVP Submit",
                                        StartDate = new DateTime(2019,1,15),
                                        EndDate = new DateTime(2019,2,15)
                                    },

                                    new Trip()
                                    {
                                       
                                        Name = "Another trip",
                                        StartDate = new DateTime(2019,2,20),
                                        EndDate = new DateTime(2019,2,25)
                                    },

                                    new Trip()
                                    {
                             
                                        Name = "Another trip 2",
                                        StartDate = new DateTime(2019,3,1),
                                        EndDate = new DateTime(2019,4,5)
                                    }

                                });
                                
                                context.SaveChanges();
                                
                            }
            
        }
    }
}
