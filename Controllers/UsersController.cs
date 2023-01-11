using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public IActionResult Index()
        {
            var model = _usersRepository.GetList();
            return View("Index", model);
        }

        public IActionResult Edit(int id)
        {
            var model = id > 0
                ? _usersRepository.Get(id)
                : new UsersModel();
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Save([FromForm] UsersModel model)
        {
            if (model.ID == 0)
            {
                _usersRepository.Create(model);
            }
            else
            {
                _usersRepository.Update(model);
            }

            return Index();
        }

        public IActionResult Delete(int id)
        {
            _usersRepository.Delete(id);
            return Index();
        }
    }
}
