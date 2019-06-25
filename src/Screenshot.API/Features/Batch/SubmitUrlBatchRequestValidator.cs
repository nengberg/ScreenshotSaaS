using System;

using FluentValidation;

namespace Screenshot.API.Features.Batch
{
    public class SubmitUrlBatchRequestValidator : AbstractValidator<SubmitUrlBatchRequest>
    {
        public SubmitUrlBatchRequestValidator()
        {
            RuleFor(p => p.Urls)
                .NotEmpty();

            RuleForEach(p => p.Urls)
                .NotEmpty()
                .WithMessage("Please enter a URL")
                .Must(BeAValidUrl)
                .WithMessage("Invalid URL format");
        }

        private static bool BeAValidUrl(string arg)
        {
            return Uri.TryCreate(arg, UriKind.Absolute, out var result);
        }
    }
}