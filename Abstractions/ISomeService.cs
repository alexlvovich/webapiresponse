using System.Threading.Tasks;
using GetInfra.WebApi.Abstractions.Models.Responses;

namespace WebApiExample
{
    public interface ISomeService
    {
        Task<GenericResponse<string>> CreateAsync(SomeRequest request);
    }
}