using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripTracker.Backservice.Models
{
    public class Repository
    {
        private List<Trip> MyTrips = new List<Trip>
        {
            new Trip()
            {
                Id = 1,
                Name = "MVP Submit",
                StartDate = new DateTime(2019,1,15),
                EndDate = new DateTime(2019,2,15)
            },

            new Trip()
            {
                Id = 2,
                Name = "Another trip",
                StartDate = new DateTime(2019,2,20),
                EndDate = new DateTime(2019,2,25)
            },

            new Trip()
            {
                Id = 3,
                Name = "Another trip 2",
                StartDate = new DateTime(2019,3,1),
                EndDate = new DateTime(2019,4,5)
            }
        };

        public List<Trip> Get()
        {
            return MyTrips;
        }

        public Trip Get(int id)
        {
            return MyTrips.First(t => t.Id == id);
        }

        public void Add(Trip newTrip)
        {
            MyTrips.Add(newTrip);
        }

        public void Update(Trip tripToUpdate)
        {
            MyTrips.Remove(MyTrips.First(t => t.Id == tripToUpdate.Id));
            Add(tripToUpdate);
        }

        public void Remove(int id)
        {
            MyTrips.Remove(MyTrips.First(t => t.Id == id));
        }
    }
}
