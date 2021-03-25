using Examen_Abpos.Connection.IRepository;
using Examen_Abpos.Connection.Repository;
using Examen_Abpos.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Examen_Abpos.Controllers
{
    public class SupervisorController : Controller
    {
        const string SessionRol = "_Rol";
        const string SessionId = "_Id";
        private readonly ILogger<AgenteController> _logger;
        private IRepository<Actividad> _actividad = null;
        private IRepository<Usuario> _usuario = null;
        private abposus_dbContext _context = null;

        public SupervisorController(ILogger<AgenteController> logger, abposus_dbContext context)
        {
            _logger = logger;
            this._context = context;
            _actividad = new BaseRepository<Actividad>(_context);
            _usuario = new BaseRepository<Usuario>(_context);
        }
        public IActionResult Index()
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            int? id = HttpContext.Session.GetInt32(SessionId);
            if (rol != 2 || rol == null)
                return RedirectToAction("Index", "Home");
            ViewBag.CantidadLlamadas = _actividad.Get().Count();
            ViewBag.CantidadNoResuelto = _actividad.Get(filter: x => x.Resuelto == false).Count();
            ViewBag.CantidadResuelto = _actividad.Get(filter: x => x.Resuelto == true).Count();

            ViewBag.AgentesMasLLamadas = _usuario.Get(filter: x => x.Rol == 1, orderBy: x => x.OrderByDescending(x => x.Actividad.Count())).Take(3);
            ViewBag.AgentesMenosLLamadas = _usuario.Get(filter: x => x.Rol == 1, orderBy: x => x.OrderBy(x => x.Actividad.Count())).Take(3);
            return View();
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);
            Usuario new_usuario = new Usuario();
            new_usuario.Nombre = usuario.Nombre;
            new_usuario.Email = usuario.Email;
            new_usuario.Password = usuario.Password;
            new_usuario.Rol = 1;
            _usuario.Insert(new_usuario);
            _usuario.Save();
            return RedirectToAction("Index");
        }
        public IActionResult CrearSupervisor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CrearSupervisor(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);
            Usuario new_usuario = new Usuario();
            new_usuario.Nombre = usuario.Nombre;
            new_usuario.Email = usuario.Email;
            new_usuario.Password = usuario.Password;
            new_usuario.Rol = 2;
            _usuario.Insert(new_usuario);
            _usuario.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Desconectar()
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol != 2 || rol == null)
                return RedirectToAction("Index", "Home");
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32(SessionRol, 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
