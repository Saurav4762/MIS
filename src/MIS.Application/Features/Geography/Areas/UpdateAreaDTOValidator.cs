using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Areas;

public class UpdateAreaDTOValidator : AbstractValidator<UpdateAreaDTO>
{
  public UpdateAreaDTOValidator()
  {
    RuleFor(x => x.DistrictId)
      .NotEmpty().WithMessage("District is required")
      .When(x => x.DistrictId.HasValue);

    RuleFor(x => x.Number)
      .GreaterThan(0).WithMessage("Area number must be greater than 0")
      .When(x => x.Number.HasValue);

  }
}