using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Examen_Abpos.Connection.IRepository;
using Examen_Abpos.Models.DB;
using Examen_Abpos.Connection.Repository;

namespace Examen_Abpos.Controllers
{
    public class AgenteController : Controller
    {
        const string SessionRol = "_Rol";
        const string SessionId = "_Id";
        private readonly ILogger<AgenteController> _logger;
        private IRepository<Actividad> _actividad = null;
        private IRepository<Usuario> _usuario = null;
        private IRepository<TipoLlamada> _tipo = null;
        private abposus_dbContext _context = null;

        public AgenteController(ILogger<AgenteController> logger, abposus_dbContext context)
        {
            _logger = logger;
            this._context = context;
            _actividad = new BaseRepository<Actividad>(_context);
            _usuario = new BaseRepository<Usuario>(_context);
            _tipo = new BaseRepository<TipoLlamada>(_context);
        }

        public IActionResult Index(string orden)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            int? id = HttpContext.Session.GetInt32(SessionId);
            if (rol != 1 || rol==null)
                return RedirectToAction("Index", "Home");
            var result_activadad = _actividad.Get(filter: x => x.IdUsuario == id,orderBy:null,includeProperties: "IdTipoNavigation");
            ViewBag.ordenResuelto = String.IsNullOrEmpty(orden)?"resuelto_desc":"";
            ViewBag.ordenConsulta = orden == "consulta" ? "consulta" : "consulta";
            ViewBag.ordenReclamo = orden == "reclamo" ? "reclamo" : "reclamo";
            switch (orden)
            {
                case "resuelto_desc":
                    result_activadad = result_activadad.OrderByDescending(x => x.Resuelto);
                    break;
                case "consulta":
                    result_activadad = result_activadad.OrderBy(x => x.IdTipo);
                    break;
                case "reclamo":
                    result_activadad = result_activadad.OrderByDescending(x => x.IdTipo);
                    break;
                default:
                    result_activadad = result_activadad.OrderBy(x => x.Resuelto);
                    break;
            }
            return View(result_activadad);
        }
        public IActionResult Detalle(int id)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol != 1 || rol == null)
                return RedirectToAction("Index", "Home");
            var result_activadad = _actividad.GetById(id);
            return View(result_activadad);
        }
        public IActionResult Editar(int id)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol != 1 || rol == null)
                return RedirectToAction("Index", "Home");
            var result_activadad = _actividad.GetById(id);
            ViewBag.tipo_llamada = _tipo.Get();
            return View(result_activadad);
        }
        [HttpPost]
        public IActionResult Editar(Actividad actividad)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol != 1 || rol == null)
                return RedirectToAction("Index", "Home");
            if (!ModelState.IsValid)
                return View(actividad);
            Actividad new_actividad = _actividad.GetById(actividad.IdActividad);
            new_actividad.Resuelto = actividad.Resuelto;
            new_actividad.IdTipo = actividad.IdTipo;
            new_actividad.DescripLlamada = actividad.DescripLlamada;
            new_actividad.DuracionLlamada = actividad.DuracionLlamada;
            _actividad.Update(new_actividad);
            _actividad.Save();
            return RedirectToAction("Index");
        }
        public IActionResult AgregarRegistro()
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol != 1 || rol == null)
                return RedirectToAction("Index", "Home");
            int? id = HttpContext.Session.GetInt32(SessionId);
            ViewBag.tipo_llamada = _tipo.Get();
            return View();
        }
        [HttpPost]
        public IActionResult AgregarRegistro(Actividad actividad)
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            int? id = HttpContext.Session.GetInt32(SessionId);
            if (rol != 1 || rol == null)
                return RedirectToAction("Index", "Home");
            if (!ModelState.IsValid)
            {
                ViewBag.tipo_llamada = _tipo.Get();
                return View(actividad);
            }
            Actividad new_actividad = new Actividad();
            new_actividad.DuracionLlamada = actividad.DuracionLlamada;
            new_actividad.DescripLlamada = actividad.DescripLlamada;
            new_actividad.IdTipo = actividad.IdTipo;
            new_actividad.Resuelto = actividad.Resuelto;
            new_actividad.IdUsuario = id;
            _actividad.Insert(new_actividad);
            _actividad.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Desconectar()
        {
            int? rol = HttpContext.Session.GetInt32(SessionRol);
            if (rol != 1 || rol == null)
                return RedirectToAction("Index", "Home");
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32(SessionRol, 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
