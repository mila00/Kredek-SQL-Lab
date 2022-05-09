using MichalNajwerLab2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MichalNajwerLab2.Repository;
using Microsoft.Extensions.Configuration;

namespace MichalNajwerLab2.Controllers
{
    public class HomeController : Controller
    {
        private PizzaRepository pizzaRepository;
        private OrderRepository orderRepository;

        public HomeController(IConfiguration config)
        {
            pizzaRepository = new PizzaRepository(config);
            orderRepository = new OrderRepository(config);
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lista wszystkich pitca
        /// </summary>
        /// <returns></returns>
        public IActionResult AllPizzas()
        {
            var pizzaList = pizzaRepository.GetPizzas();

            return View(pizzaList);
        }

        /// <summary>
        /// Szczególy pizzy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult PizzaDetails(int id)
        {
            var pizzaList = pizzaRepository.GetPizzas();

            var pizza = pizzaList.FirstOrDefault(x => x.Id == id);
            return View(pizza);
        }

        /// <summary>
        /// Tworzenie nowego zamowienia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult CreateOrder(int id)
        {
            var pizzaList = pizzaRepository.GetPizzas();

            var pizza = pizzaList.FirstOrDefault(x => x.Id == id);
            ViewBag.pizza = pizza;
            return View();
        }

        /// <summary>
        /// Metoda post do obsługi składania zamówienia
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder([Bind("PizzaId,City,Adress,Phone,Email")] Order order)
        {
            var pizzaList = pizzaRepository.GetPizzas();

            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                var pizza = pizzaList.FirstOrDefault(x => x.Id == order.PizzaId);
                ViewBag.orderedPizza = pizza;
                orderRepository.InsertOrder(order);
                return View("PlacedOrder", order);
            }
            else
            {
                var pizza = pizzaList.FirstOrDefault(x => x.Id == order.PizzaId);
                ViewBag.pizza = pizza;
                return View(order);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
