using System;
using Ikelva.ClassLibrary.Helpers;
using Ikelva.ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ikelva.MVC.Controllers
{
    public class FurnitureController : Controller
    {
        private readonly IProducer _producer;

        public FurnitureController(IProducer producer)
        {
            _producer = producer;
        }

        // GET: FurnitureController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FurnitureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            customer.CustomerId = Guid.NewGuid().ToString();

            _producer.CreateProducer(customer);

            return View("OrderReceived", customer);
        }
    }
}
