using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PeliculasAPI.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedorDevalores = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if (proveedorDevalores == ValueProviderResult.None) return Task.CompletedTask;

            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<T>(proveedorDevalores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }catch (Exception ex)
            {
                var type = typeof(T);
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, $"Valor invalido para tipo {type}");
            }
            return Task.CompletedTask;
        }
    }
}
