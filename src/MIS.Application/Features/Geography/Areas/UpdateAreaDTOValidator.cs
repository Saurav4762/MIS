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

    RuleFor(x => x.Code)
      .MaximumLength(20).WithMessage("Area code must be at most 20 characters")
      .When(x => x.Code is not null);

    RuleFor(x => x.NameEn)
      .MaximumLength(200).WithMessage("Area English name must be at most 200 characters")
      .When(x => x.NameEn is not null);

    RuleFor(x => x.NameNe)
      .NotEmpty().WithMessage("Area Nepali name is required")
      .MustBeNepaliOrEmpty().WithName("Area Nepali name")
      .MaximumLength(200).WithMessage("Area Nepali name must be at most 200 characters");
  }
}