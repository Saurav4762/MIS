using FluentValidation;
using MIS.Application.Common.Extensions;
using MIS.Application.Features.DataCollection.HouseholdInfo.Members;

namespace MIS.Application.Features.DataCollection.HouseholdInfo.Members;

public class UpdateMemberDtoValidator : AbstractValidator<UpdateMemberDto>
{
    public UpdateMemberDtoValidator()
    {
        RuleFor(x => x.FullNameEn)
            .NotEmpty().WithMessage("Full name in English cannot be empty")!
            .MaximumLength(200).WithMessage("Full name in English must not exceed 200 characters")
            .When(x => x.FullNameEn is not null);

        RuleFor(x => x.FullNameNe)
            .NotEmpty().WithMessage("Full name in Nepali cannot be empty")!
            .MustBeNepali()
            .WithName("Full name in Nepali")
            .MaximumLength(200).WithMessage("Full name in Nepali must not exceed 200 characters")
            .When(x => x.FullNameNe is not null);

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of birth must be in the past")!
            .When(x => x.DateOfBirth.HasValue);

        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile number cannot be empty")!
            .MaximumLength(20).WithMessage("Mobile number must not exceed 20 characters")
            .When(x => x.MobileNumber is not null);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format")!
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
