using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        { 

            if(HttpContext.Session.GetString("login") != "admin"){
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        } 

        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {  

            UsuarioService usuario = new UsuarioService();

            usuario.Cadastrar(u);

            return RedirectToAction("Listagem");

        }

        public IActionResult Listagem()
        {

            ICollection<Usuario> usuarios;

            UsuarioService usuario = new UsuarioService();

            usuarios = usuario.Listar();

            if(HttpContext.Session.GetString("login") != "admin"){
                return RedirectToAction("Index", "Home");
            }

            return View(usuarios);
            
        } 

        
        [HttpGet]
        public IActionResult Editar(int id)
        {

            UsuarioService user = new UsuarioService();

            Usuario usuario = new Usuario();

            usuario = user.ObterPorId(id);

            return View(usuario);

        }

        [HttpPost]

        public IActionResult Editar(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = bc.Usuario.Find(u.Id);

                usuario.Login = u.Login;
                
                usuario.Senha = u.Senha;

                bc.SaveChanges();

                return RedirectToAction("Listagem");
            }
        }

        public IActionResult Remover(int id)
        {
            Usuario usuario = new Usuario();

            UsuarioService user = new UsuarioService();

            usuario = user.ObterPorId(id);

            user.Remover(usuario);

            return RedirectToAction("Listagem");
        }
    }
}