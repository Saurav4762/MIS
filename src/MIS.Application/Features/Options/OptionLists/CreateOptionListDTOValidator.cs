
using System.Security.Cryptography.X509Certificates;
using FluentValidation;

namespace MIS.Application.Features.Options.OptionLists;

public class CreateOptionListDTOValidator : AbstractValidator<CreateOptionListDTO>
{
  public CreateOptionListDTOValidator()
  {
    RuleFor(x => x.LabelEn).NotEmpty().WithMessage("English Label is required");

    RuleFor(x => x.LabelNe)
      .NotEmpty()
      .WithMessage("Nepali Label is required");

    RuleFor(x => x.Description)
      .NotEmpty()
      .WithMessage("Description is required");
  }
}