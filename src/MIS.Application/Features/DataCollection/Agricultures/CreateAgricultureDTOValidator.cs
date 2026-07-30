using FluentValidation;
namespace MIS.Application.Features.DataCollection.Agricultures;

public class CreateAgricultureDTOValidator : AbstractValidator<CreateAgricultureDTO>
{
    public CreateAgricultureDTOValidator()
    {
        RuleFor(x => x.FamilyId)
            .NotEmpty().WithMessage("Family is required.");

        RuleFor(x => x.LandUnitId)
            .NotEmpty().WithMessage("Land unit is required.");

            RuleFor(x => x.OwnershipStatusId)
            .NotEmpty().WithMessage("Ownership status is required.");

        RuleFor(x => x.TotalArea)
            .GreaterThan(0).WithMessage("Total area must be greater then 0.");

        RuleForEach(x => x.SelectedCrops)
            .ChildRules(crop =>
            {
                crop.RuleFor(x => x.CropId)
                    .NotEmpty().WithMessage("Crop is required.");

                crop.RuleFor(x => x.AreaInHectares)
                    .GreaterThan(0).WithMessage("Area in hectares must be greater than 0.");

                crop.RuleFor(x => x.EstimatedYield)
                    .GreaterThan(0).WithMessage("Estimated yield must be greater than 0.");
            });

        RuleForEach(x => x.Equipments)
            .ChildRules(equipment =>
            {
                equipment.RuleFor(x => x.EquipmentId)
                    .NotEmpty().WithMessage("Equipment is required.");

                equipment.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            });


        RuleForEach(x => x.ProblemsFaced)
              .ChildRules(problem =>
              {
                  problem.RuleFor(x =>x.ProblemId)
                       .NotEmpty().WithMessage("Problem is required.");
                  problem.RuleFor(x => x.Details)
                          .NotEmpty().WithMessage("Details are required.");
              });

        RuleForEach(x => x.LandTypes)
            .ChildRules(landType =>
            {
                landType.RuleFor(x => x.LandTypeId)
                    .NotEmpty().WithMessage("Land type is required.");

                landType.RuleFor(x => x.Area)
                    .GreaterThan(0).WithMessage("Area in hectares must be greater then 0.");
            });   
    }
}