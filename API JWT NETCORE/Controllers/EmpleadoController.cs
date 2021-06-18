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
        public async Task<IActionResult> Editar(UsuarioRequest usuario)
        {
            try
            {
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            try
            {
                return Ok("desactivar usuario: " + id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToLista());
            }
        }
    }
}
