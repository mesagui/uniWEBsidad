using System;
using System.Collections.Generic;
using System.Linq;
using Uniwebsidad.BaseDatos;
using Uniwebsidad.Models;
using Microsoft.AspNetCore.Mvc;
namespace Uniwebsidad.Controllers
{
    public class CursoController : Controller
    {
        private readonly CalidadContext _context;

        public CursoController(CalidadContext _context)
        {
            this._context = _context;
        }

        public IActionResult Index()
        {
            Usuario user = LoggedUser();
            
            var indicesDeMisCursos = _context._DetalleUsuarioCursos
                                     .Where(o => o.IdUsuario == user.Id)
                                     .Select(o => o.IdCurso)
                                     .ToList();
            
            ViewBag.UsuarioId = user.Id;
            
            List<Curso> MisCursos = new List<Curso>();
            
            var cursos = _context._Cursos.ToList();
            
            ViewBag.Cantidad = cursos.Count();
            
            for (int i = 0; i < cursos.Count; i++)
            {
                if (indicesDeMisCursos.Contains(cursos[i].Id))
                {
                    MisCursos.Add(cursos[i]);
                }
            }

            Dictionary<int, String> categorias = new Dictionary<int, string>();
            
           // var categoriasa = _context._Categorias.ToList();
            
            foreach(var item in MisCursos)
            {
                categorias.Add(item.Id, _context._Categorias.Where(o => o.Id == item.IdCategoria).FirstOrDefault().Nombre);
            }
            
            ViewBag.Categorias = categorias;
            ViewBag.Cantidad = MisCursos.Count();
            ViewBag.MisCursos = MisCursos;
         
            
            return View();
        }


        public IActionResult CrearCursoInterface()
        {
            Usuario user = LoggedUser();
            
            ViewBag.UsuarioId = user.Id;
            
            var categorias = _context._Categorias.ToList();
            
            ViewBag.Categorias = categorias;
            
            return View();
        }


        public IActionResult CrearCursoForm(String Curso,int categoria)
        {
            Usuario user = LoggedUser();
            
            ViewBag.UsuarioId = user.Id;
            
            Curso CursoCreado = new Curso();
            
            CursoCreado.FechaCreacion= DateTime.Now;
            CursoCreado.IdCategoria = categoria;
            CursoCreado.IdCreador = user.Id;
            CursoCreado.Nombre = Curso;
            
            _context._Cursos.Add(CursoCreado);
            _context.SaveChanges();
            
            //if (CursoCreado.IdCategoria == 0) HttpContext.Response.StatusCode = 500;
            
            return RedirectToAction("CursosCreadosPorUsuario", "Curso");
        }


        public IActionResult DetalleCurso(int Id)
        {
            Usuario user = LoggedUser();
            ViewBag.UsuarioId = user.Id;
            
            var cursoDetalle = _context._Cursos.FirstOrDefault(o => o.Id == Id);
            ViewBag.Curso = cursoDetalle;
            
            var Usuario = _context._Usuarios.FirstOrDefault(o => o.Id == cursoDetalle.IdCreador);
            ViewBag.Usuario = Usuario;
            
            var videosCurso = _context._Videos.Where(o => o.IdCurso == Id).ToList();
            ViewBag.Videos = videosCurso;
            
            return View();
        }

        public IActionResult DetalleCursoLogout(int Id)
        {
            var cursoDetalle = _context._Cursos.FirstOrDefault(o => o.Id == Id);
            ViewBag.Curso = cursoDetalle;
            
            var Usuario = _context._Usuarios.FirstOrDefault(o => o.Id == cursoDetalle.IdCreador);
            ViewBag.Usuario = Usuario;
            
            var videosCurso = _context._Videos.Where(o => o.IdCurso == Id).ToList();
            ViewBag.Videos = videosCurso;
            
            return View();
        }

        public IActionResult TodosLosCursos()
        {
            Usuario user = LoggedUser();
            ViewBag.UsuarioId = user.Id;

            var cursos = _context._Cursos.ToList();
            ViewBag.Cursos = cursos;

            Dictionary<int, String> categorias = new Dictionary<int, string>();
            
            //var categoriasa = _context._Categorias.ToList();
            
            foreach (var item in cursos)
            {
                categorias.Add(item.Id, _context._Categorias.Where(o => o.Id == item.IdCategoria).FirstOrDefault().Nombre);
            }

            ViewBag.Categorias = categorias;

            return View();
        }

        public IActionResult TodosLosCursosLogout()
        {
            var cursos = _context._Cursos.ToList();
            ViewBag.Cursos = cursos;

            Dictionary<int, String> categorias = new Dictionary<int, string>();
            
            //var categoriasa = _context._Categorias.ToList();
            
            foreach (var item in cursos)
            {
                categorias.Add(item.Id, _context._Categorias.Where(o => o.Id == item.IdCategoria).FirstOrDefault().Nombre);
            }

            ViewBag.Categorias = categorias;

            return View();
        }



        public IActionResult agregarCurso(int Id)
        {
            Usuario user = LoggedUser();
            
            if(user != null)
            {
                ViewBag.UsuarioId = user.Id;
                
                DetalleUsuarioCurso nuevoDetalleUsuarioCurso = new DetalleUsuarioCurso();
                
                nuevoDetalleUsuarioCurso.Estado = true;
                nuevoDetalleUsuarioCurso.IdCurso = Id;
                nuevoDetalleUsuarioCurso.IdUsuario = user.Id;

                _context._DetalleUsuarioCursos.Add(nuevoDetalleUsuarioCurso);
                _context.SaveChanges();
                
                Video nuevo = new Video();
                
                nuevo.Link = "";
                nuevo.IdCurso = Id;
                
                _context._Videos.Add(nuevo);
                _context.SaveChanges();
                
                return RedirectToAction("TodosLosCursos", "Curso");
            }
            
            return RedirectToAction("Login", "Auth");
        }


        public IActionResult desagregarCurso(int Id)
        {
            Usuario user = LoggedUser();
            ViewBag.UsuarioId = user.Id;
            
            var cursoQuitar = _context._DetalleUsuarioCursos
                              .FirstOrDefault(o => o.IdCurso == Id && o.IdUsuario == user.Id);
          
            _context._DetalleUsuarioCursos.Remove(cursoQuitar);
            _context.SaveChanges();

            return RedirectToAction("Index", "Curso");
        }


        public IActionResult CursosCreadosPorUsuario()
        {
            Usuario user = LoggedUser();
            ViewBag.UsuarioId = user.Id;
            
            var cursos = _context._Cursos.Where(o => o.IdCreador == user.Id).ToList();
            
            ViewBag.Usuario = user.Nombres;
            ViewBag.CursosCreadosPorUsuario = cursos;
            ViewBag.Cantidad = cursos.Count();

            Dictionary<int, String> categorias = new Dictionary<int, string>();
            
            //var categoriasa = _context._Categorias.ToList();
            
            foreach (var item in cursos)
            {
                categorias.Add(item.Id, _context._Categorias.Where(o => o.Id == item.IdCategoria).FirstOrDefault().Nombre);
            }

            ViewBag.Categorias = categorias;

            return View();
        }


        public IActionResult AgregarVideoAlCurso(int Id)
        {
            Usuario user = LoggedUser();
            
            ViewBag.UsuarioId = user.Id;
            ViewBag.IdVideo = Id;
            
            var cursos = _context._Cursos.Where(o => o.IdCreador == user.Id).ToList();
            
            ViewBag.CursosCreadosPorUsuario = cursos;
            
            return View();
        }


        public IActionResult AgregarVideoAlCursoForm(int Idvideo, String video)
        {
            Usuario user = LoggedUser();
            ViewBag.UsuarioId = user.Id;

            Video nuevoVideo = new Video();
            
            nuevoVideo.Link = video;
            nuevoVideo.IdCurso = Idvideo;
            
            _context._Videos.Add(nuevoVideo);
            _context.SaveChanges();
            
            return RedirectToAction("CursosCreadosPorUsuario", "Curso");
        }
        
        
        private Usuario LoggedUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault();
            
            if(claim != null)
            {
                var user = _context._Usuarios.Where(o => o.Username == claim.Value).First();
                return user;
            }
            
            return null;
        }
    }
}











