using System;
using System.Threading.Tasks;
using GetInfra.WebApi.Abstractions;
using GetInfra.WebApi.Abstractions.Extentions;
using GetInfra.WebApi.Abstractions.Models.Responses;
using GetInfra.WebApi.Abstractions.Models.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApiExample
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IValidator<SomeRequest> _someValidator;
        private readonly ISomeService _someService;
        public SomeController(ILogger<SomeController> logger, IValidator<SomeRequest> someValidator,
            ISomeService someService)
        {
            _logger = logger;
            _someValidator = someValidator;
            _someService = someService;
        }

        
        [HttpPost]
        public async Task<ActionResult<GenericResponse<string>>> Post(SomeRequest request)
        {
            
            // validate 
            var r = await _someValidator.ValidateAsync(request);
            // convert to generic response
            var result = r.ToGenericResponse<string>();

            if (!result.IsValid)
                // return 400
                return BadRequest(result);

            try
            {
                // service call
                result = await _someService.CreateAsync(request);
                // return 201
                return Created(new Uri($"https://some.domain-com/path/{result.Id}"), result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST error: {0}, stack:", ex.Message, ex.StackTrace);
                result.Errors.Add(new ErrorItem(ex.Message, ex.StackTrace));
                // return 500
                return StatusCode(500, result);
            }

        }

    }
}