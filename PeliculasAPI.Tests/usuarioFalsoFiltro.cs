using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests
{
    public class usuarioFalsoFiltro:IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Email, "ejemplo@hotmail.com"),
                new Claim(ClaimTypes.Name,"ejemplo@hotmail.com"),
                new Claim(ClaimTypes.NameIdentifier,"9722b56a-77ea-4e41-941d-e319b6eb3712"),
            }, "prueba"));

            await next();
        }
    }
}
