using System;
using System.Threading.Tasks;
using GetInfra.WebApi.Abstractions.Models.Responses;

namespace WebApiExample
{
    public class SomeService : ISomeService
    {
        public async Task<GenericResponse<string>> CreateAsync(SomeRequest request)
        {
            var r = new GenericResponse<string>();

            r.Id = Guid.NewGuid().ToString("N").Substring(0, 8);

            return await Task.FromResult(r);
        }
    }
}