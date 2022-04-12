﻿using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, IQueryable<T> queryable, int CantidadRegistrosPorPagina){
            double cantidad = await queryable.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad/ CantidadRegistrosPorPagina);
            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
        }
    }
}
