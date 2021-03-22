using Examen_Abpos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Examen_Abpos.Models.DB;
using Examen_Abpos.Connection.IRepository;
using Examen_Abpos.Connection.Repository;
using Microsoft.AspNetCore.Http;

namespace Examen_Abpos.Controllers
{
    public class HomeController : Controller
    {
        const string SessionRol = "_Rol";
        const string SessionId = "_Id";
        private readonly ILogger<HomeController> _logger;
        private IRepository<Usuario> _usuario = null;
        private abposus_dbContext _context = null;

        public HomeController(ILogger<HomeController> logger,abposus_dbContext context)
        {
            _logger = logger;
            this._context = context;
            _usuario = new BaseRepository<Usuario>(_context);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var result_login = _usuario.Get(filter: x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (result_login == null)
                return View(user);
            HttpContext.Session.SetInt32(SessionRol, result_login.Rol);
            HttpContext.Session.SetInt32(SessionId, result_login.IdUsuario);
            if (result_login.Rol == 1)
                return RedirectToAction("Index", "Agente");
            if (result_login.Rol == 2)
                return RedirectToAction("Index", "Supervisor");

            return View(user);
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
