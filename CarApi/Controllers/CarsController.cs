using CarModel.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarApi.Controllers
{
    public class CarsController : ApiController
    {
        IList<Car> cars = new List<Car>() { new Car(10,10), new Car(20, 20), new Car(10, 20)};

        public IEnumerable<Car> GetAllProducts()
        {
            return cars;
        }

        public IHttpActionResult GetCar()
        {
            Car car = cars.FirstOrDefault(x => x.EngineIsRunning);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }
    }
}
