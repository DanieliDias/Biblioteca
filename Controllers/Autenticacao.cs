using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {
            if (string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }

        }

        public static bool verificaLoginSenha(string Login, string senha, Controller controller)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                VerificaSeUsuarioAdminExiste(bc);
                senha = Criptografo.TextoCriptografado(senha);
                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login == Login && u.Senha == senha);

                List<Usuario> listaUsuarioEncontrado = UsuarioEncontrado.ToList();

                if (listaUsuarioEncontrado.Count == 0)
                {
                    return false;
                }

                else
                {
                    controller.HttpContext.Session.SetString("Login", listaUsuarioEncontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome", listaUsuarioEncontrado[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", listaUsuarioEncontrado[0].Tipo);
                    return true;
                }
            }

        }
        public static void VerificaSeUsuarioAdminExiste(BibliotecaContext bc)
        {
            IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login == "admin");
            List<Usuario> listaUsuarioEncontrado = UsuarioEncontrado.ToList();

            if (listaUsuarioEncontrado.Count == 0)
            {
                Usuario Admin = new Usuario();
                Admin.Login = "admin";
                Admin.Senha = Criptografo.TextoCriptografado("123");
                Admin.Nome = "Administrador";
                Admin.Tipo = Usuario.ADMIN;

                bc.Add(Admin);
                bc.SaveChanges();
            }
        }

        internal static bool VerificarSeUsuarioEAdmin(Controller controller)
        {

            if (controller.HttpContext.Session.GetInt32("Tipo") != Usuario.ADMIN)
            {
        
                controller.Request.HttpContext.Response.Redirect("/Usuarios/administrador");
            }

            return false;
         }

    }
}
