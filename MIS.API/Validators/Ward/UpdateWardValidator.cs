using FluentValidation;
using MIS.API.Interfaces.IRepositories;
using MIS.API.DTOs;
using static MIS.API.DTOs.WardDTO;
using System.Data;

namespace MIS.API.Validators;

public class UpdateWardValidator : AbstractValidator<WardRequest>
{
    private readonly IWardRepo _wardRepo;
    private readonly Guid _wardId;

    public UpdateWardValidator(IWardRepo wardRepo, Guid wardId)
    {
        _wardRepo = wardRepo;
        _wardId = wardId;

        RuleFor(w => w.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long");

        RuleFor(w => w.Code)
            .NotEmpty().WithMessage("Code is required");

        RuleFor(w => w.MunicipalityId)
            .NotEmpty().WithMessage("MunicipalityId is required")
            .MustAsync(async (id, cancellation) => await _wardRepo.MunicipalityExistsAsync(id))
            .WithMessage("Municipality with the given Id does not exist")
            .When(w => w.MunicipalityId != Guid.Empty);

        RuleFor(w => w)
            .MustAsync(async (request, cancellation) =>
            {
                var exists = await _wardRepo.WardNameExistsAsync(request.Name, request.MunicipalityId, _wardId);
                return !exists;
            })
            .WithName("Ward Name")
            .WithMessage("Ward with the given name already exists in the specified municipality");
    }

}