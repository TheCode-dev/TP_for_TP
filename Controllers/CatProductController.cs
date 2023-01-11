using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Controllers
{
    [Authorize]
    public class CatProductController : Controller
    {
        private readonly ICatProductRepository _catProductRepository;
        public CatProductController(ICatProductRepository catProductRepository)
        {
            _catProductRepository = catProductRepository;
        }

        public IActionResult Index()
        {
            var model = _catProductRepository.GetList();
            return View("Index", model);
        }

        public IActionResult Edit(int id)
        {
            var model = id > 0
                ? _catProductRepository.Get(id)
                : new CatProductModel();
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Save([FromForm] CatProductModel model)
        {
            if (model.ID == 0)
            {
                _catProductRepository.Create(model);
            }
            else
            {
                _catProductRepository.Update(model);
            }

            return Index();
        }

        public IActionResult Delete(int id)
        {
            _catProductRepository.Delete(id);
            return Index();
        }
    }
}
