using FluentValidation;

namespace MIS.Application.Features.Submissions;


public class CreateSubmissionDTOValidator : AbstractValidator<CreateSubmissionDTO>
{
  public CreateSubmissionDTOValidator()
  {
    RuleFor(x => x.SubmittedById).NotEmpty().WithMessage("SubmittedById is required");
  }
}