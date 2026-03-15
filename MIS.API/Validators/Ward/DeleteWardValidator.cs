using FluentValidation;
using MIS.API.Interfaces.IRepositories;

namespace MIS.API.Validators;

public class DeleteWardValidator : AbstractValidator<Guid>
{
    private readonly IWardRepo _wardRepo;

    public DeleteWardValidator(IWardRepo wardRepo)
    {
        _wardRepo = wardRepo;

        RuleFor(id => id)
            .NotEmpty().WithMessage("Ward Id is required")
            .MustAsync(async (id, cancellation) => await _wardRepo.WardExistsByIdAsync(id))
            .WithMessage("Ward with the given Id does not exist");
    }
    
}