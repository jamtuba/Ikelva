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

        //// GET: FurnitureController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: FurnitureController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

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

        //// GET: FurnitureController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: FurnitureController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: FurnitureController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: FurnitureController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
