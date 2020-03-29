using System.Threading;
using System.Threading.Tasks;
using GetInfra.WebApi.Abstractions.Models.Responses;
using GetInfra.WebApi.Abstractions.Models.Validation;
using System;

namespace WebApiExample
{
    public class SomeRequestValidator : ValidatorBase<SomeRequest>
    {
        public SomeRequestValidator(IRegularExpressions expressions) : base(expressions)
        {
        }
        public override async Task<BaseResponse> ValidateAsync(SomeRequest request, CancellationToken cancellation = default)
        {
             if (request == null) throw new ArgumentNullException(nameof(request),"entity is null");
            var validationState = new BaseResponse();

            if (string.IsNullOrWhiteSpace(request.SomeProperty))
            {
                validationState.ValiationErrors.Add(CreateValidationError(request.SomeProperty, "request.SomeProperty.RequiredError", "SomeProperty is missing."));
            }

            return await Task.FromResult(validationState);
        }
    }
}