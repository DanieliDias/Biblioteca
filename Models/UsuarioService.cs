using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Controllers;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();

            }
        }

        public Usuario Listar(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);

            }
        }

        public void incluirUsuario(Usuario u)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Add(u);
                bc.SaveChanges();
            }
        }

        public void editarUsuario(Usuario u)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuarioBD = bc.Usuarios.Find(u.Id);
                usuarioBD.Login = u.Login;
                usuarioBD.Nome = u.Nome;
 
              if(usuarioBD.Senha !=u.Senha)
              {
                Criptografo.TextoCriptografado(u.Senha);
              }
              else{
                usuarioBD.Senha = u.Senha;
              }

                usuarioBD.Senha = u.Senha;

                usuarioBD.Tipo = u.Tipo;


                bc.SaveChanges();
            }
        }

        public void excluirUsuario(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }

        internal void ExcluirUsuario(int id)
        {
            throw new NotImplementedException();
        }
    }
}