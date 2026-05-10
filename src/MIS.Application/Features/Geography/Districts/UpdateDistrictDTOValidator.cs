using FluentValidation;
using MIS.Application.Common.Extensions;

namespace MIS.Application.Features.Geography.Districts;

public class UpdateDistrictDTOValidator : AbstractValidator<UpdateDistrictDTO>
{
  public UpdateDistrictDTOValidator()
  {
    RuleFor(x => x.ProvinceId)
      .NotEmpty().WithMessage("Province is required")
      .When(x => x.ProvinceId.HasValue);

    RuleFor(x => x.Code)
      .MaximumLength(20).WithMessage("District code must be at most 20 characters")
      .When(x => x.Code is not null);

    RuleFor(x => x.NameEn)
      .MaximumLength(200).WithMessage("District English name must be at most 200 characters")
      .When(x => x.NameEn is not null);

    RuleFor(x => x.NameNe)
      .NotEmpty().WithMessage("District Nepali name is required")
      .MustBeNepaliOrEmpty().WithName("District Nepali name")
      .MaximumLength(200).WithMessage("District Nepali name must be at most 200 characters");
  }
}
