using FluentValidation;

namespace MIS.Application.Features.DataCollection.HouseInfo;


public class CreateHouseDTOValidator : AbstractValidator<CreateHouseDTO>
{
  public CreateHouseDTOValidator()
  {
    RuleFor(x => x.SubmissionId)
      .NotEmpty().WithMessage("Submission is required");

    RuleFor(x => x.HouseNumber)
      .NotEmpty().WithMessage("House number is required");

    RuleFor(x => x.Location)
      .NotEmpty().WithMessage("Location is required");

    RuleFor(x => x.WardId)
      .NotEmpty().WithMessage("Ward is required");

    RuleFor(x => x.ToleId)
      .NotEmpty().WithMessage("Tole is required");

    RuleFor(x => x.HouseTypeId)
      .NotEmpty().WithMessage("House type is required");

    RuleFor(x => x.LandTypeId)
      .NotEmpty().WithMessage("Land type is required");

    RuleFor(x => x.RoofTypeId)
      .NotEmpty().WithMessage("Roof type is required");

    RuleFor(x => x.WallTypeId)
      .NotEmpty().WithMessage("Wall type is required");

    RuleFor(x => x.ImageId)
      .NotEmpty().WithMessage("House Image is required");

  }
}