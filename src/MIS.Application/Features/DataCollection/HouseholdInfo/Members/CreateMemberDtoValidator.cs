using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.DataCollection.HouseholdInfo.Members;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public class CreateMemberDtoValidator : AbstractValidator<CreateMemberDto>
{
    public CreateMemberDtoValidator()
    {
        RuleFor(x => x.FamilyId)
            .NotEmpty().WithMessage("Family id is required");

        RuleFor(x => x.FullNameEn)
            .NotEmpty().WithMessage("Full name in English is required")
            .MaximumLength(200).WithMessage("Full name in English must not exceed 200 characters");

        RuleFor(x => x.FullNameNe)
            .NotEmpty().WithMessage("Full name in Nepali is required")
            .MustBeNepali()
            .WithName("Full name in Nepali")
            .MaximumLength(200).WithMessage("Full name in Nepali must not exceed 200 characters");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required")
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of birth must be in the past");

        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile number is required")
            .MaximumLength(20).WithMessage("Mobile number must not exceed 20 characters");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
