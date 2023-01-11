using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Bcpg.OpenPgp;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;
using TP_KP_Belyshev.Repositories;

namespace TP_KP_Belyshev.Controllers
{
    [Authorize (Roles = "Admin, Manager")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IUsersRepository _usersRepository;

        public OrdersController(IOrdersRepository ordersRepository,
            IProductsRepository productsRepository,
            IUsersRepository usersRepository)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _usersRepository = usersRepository;
            _usersRepository = usersRepository;
        }

        public IActionResult Index()
        {
            var model = _ordersRepository.GetList();
            return View("Index", model);
        }
        public IActionResult UserOrders(int id)
        {
            var model = _ordersRepository.GetHistory(id);
            return View("UserOrders", model);
        }

        public IActionResult Edit(int id)
        {
            var ordersModel = id > 0
                ? _ordersRepository.Get(id)
                : new OrdersModel();

            var productModel = _productsRepository.GetList()
                .Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = Convert.ToString(a.ID)
                });

            var userModel = _usersRepository.GetList()
                .Select(a => new SelectListItem
                {
                    Text = a.FirstName + " " + a.LastName,
                    Value = Convert.ToString(a.ID)
                });

            var managerModel = _usersRepository.GetManagers()
                .Select(a => new SelectListItem
                {
                    Text = a.FirstName + " " + a.LastName,
                    Value = Convert.ToString(a.ID)
                });

            var firstItem = productModel.FirstOrDefault();
            if (firstItem != null)
            {
                firstItem.Selected = true;
            }
            var firstUser = userModel.FirstOrDefault();
            if (firstUser != null)
            {
                firstUser.Selected = true;
            }

            ordersModel.ProductsModel = productModel;
            ordersModel.UsersModel = userModel;
            ordersModel.ManagerModel = managerModel;

            return View("Edit", ordersModel);
        }

        [HttpPost]
        public IActionResult Save([FromForm] OrdersModel model)
        {
            if (model.ID == 0)
            {
                _ordersRepository.Create(model);
            }
            else
            {
                _ordersRepository.Update(model);
            }

            return Index();
        }

        public IActionResult Delete(int id)
        {
            _ordersRepository.Delete(id);
            return Index();
        }
    }
}
