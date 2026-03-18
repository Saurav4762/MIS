using FluentValidation;

namespace MIS.Application.Features.Options.OptionItems;


public class CreateOptionItemDTOValidator : AbstractValidator<CreateOptionItemDTO>
{
  public CreateOptionItemDTOValidator()
  {

    RuleFor(x => x.OptionListId)
      .NotEmpty().WithMessage("Option list id is a required field");


    RuleFor(x => x.LabelEn)
      .NotEmpty().WithMessage("English Label for Optionitem is required");

    RuleFor(x => x.LabelNe)
    .NotEmpty().WithMessage("Nepali Label for Optionitem is required");

  }
}