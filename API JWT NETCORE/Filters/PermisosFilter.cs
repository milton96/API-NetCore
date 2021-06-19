using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Filters
{
    public class PermisosFilter : IActionFilter
    {
        private int[] _permisos_permitidos;
        public PermisosFilter(int[] permitidos)
        {
            _permisos_permitidos = permitidos;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Se ejecuta despues
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Se ejecuta antes
            IIdentity identity = context.HttpContext.User.Identity;
            ClaimsIdentity user = identity as ClaimsIdentity;
            Claim permiso_rol = user.FindFirst(ClaimTypes.Role);
            int rol = Int32.Parse(permiso_rol.Value);
            if (!_permisos_permitidos.Contains(rol))
                context.Result = new BadRequestObjectResult(new { errors = new { General = new string[] { "Son necesarios permisos de administrador" } } });
        }
    }
}
