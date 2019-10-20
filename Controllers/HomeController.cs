using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Data.Repositories;
using Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository _repository;

        public HomeController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllWithInclude(re => re.Supplier)
                                            .ConfigureAwait(true);
            return View(products.ToList());
        }
    }
}