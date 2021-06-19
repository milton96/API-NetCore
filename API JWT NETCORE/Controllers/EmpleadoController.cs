using API_JWT_NETCORE.Filters;
using API_JWT_NETCORE.Helpers;
using API_JWT_NETCORE.Models;
using API_JWT_NETCORE.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        [HttpPost]        
        [Route("ping")]
        public async Task<IActionResult> Ping()
        {
            try
            {
                return Ok("Usuario en sesión");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }

        [HttpPost]
        [Route("")]
        [TypeFilter(typeof(PermisosFilter), Arguments = new object[] { new int[] { (int)Roles.Administrador } })]
        public async Task<IActionResult> Crear(UsuarioRequest usuario)
        {
            try
            {
                Rol rol = await Rol.ObtenerPorCodigo(usuario.Rol);
                if (rol == null) throw new Exception("El rol del usuario es inválido");

                Usuario u = new Usuario();
                u.IdRol = rol;
                u.Nombre = usuario.Nombre;
                u.ApellidoPaterno = usuario.ApellidoPaterno;
                u.ApellidoMaterno = usuario.ApellidoMaterno;
                u.Correo = usuario.Correo;
                u.Password = usuario.Password;
                await u.Guardar();
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Usuario no válido");
                Usuario usuario = await Usuario.ObtenerPorId(id);
                if (usuario == null) throw new Exception("Usuario inexistente");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                List<Usuario> usuarios = await Usuario.ObtenerTodos();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }

        [HttpPut]
        [Route("")]
        [TypeFilter(typeof(PermisosFilter), Arguments = new object[] { new int[] { (int)Roles.Administrador } })]
        public async Task<IActionResult> Editar(EditarUsuarioRequest usuario)
        {
            try
            {
                if (usuario.Id <= 0) throw new Exception("Usuario no válido");

                Usuario u = await Usuario.ObtenerPorId(usuario.Id);
                if (usuario.Rol > 0)
                {
                    Rol rol = await Rol.ObtenerPorCodigo(usuario.Rol);
                    if (rol == null) throw new Exception("El rol del usuario es inválido");
                    if (rol.Id != u.IdRol.Id)
                        u.IdRol = rol;
                }

                u.Nombre = String.IsNullOrEmpty(usuario.Nombre) || String.IsNullOrWhiteSpace(usuario.Nombre) ? u.Nombre : usuario.Nombre;
                u.ApellidoPaterno = usuario.ApellidoPaterno;
                u.ApellidoMaterno = usuario.ApellidoMaterno;
                u.Correo = String.IsNullOrEmpty(usuario.Correo) || String.IsNullOrWhiteSpace(usuario.Correo) ? u.Correo : usuario.Correo;

                await u.Actualizar();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [TypeFilter(typeof(PermisosFilter), Arguments = new object[] { new int[] { (int)Roles.Administrador } })]
        public async Task<IActionResult> Desactivar(int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Usuario no válido");

                Usuario u = await Usuario.ObtenerPorId(id);
                if (u == null) throw new Exception("El usuario no existe");

                int total = await u.Desactivar();
                if (total <= 0) throw new Exception("Ocurrió un problema al desactivar el usuario");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }
    }
}
