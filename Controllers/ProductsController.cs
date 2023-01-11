using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var model = _productsRepository.GetList();
            return View("Index", model);
        }

        public IActionResult Edit(int id)
        {
            var model = id > 0
                ? _productsRepository.Get(id)
                : new ProductsModel();
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Save([FromForm] ProductsModel model)
        {
            if (model.ID == 0)
            {
                _productsRepository.Create(model);
            }
            else
            {
                _productsRepository.Update(model);
            }

            return Index();
        }

        public IActionResult Delete(int id)
        {
            _productsRepository.Delete(id);
            return Index();
        }
    }
}
