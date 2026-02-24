using MIS.API.Models;

namespace MIS.API.Data.Seed
{
    public static class OptionListSeed
    {
        public static readonly OptionList[] Data =
        {
            new OptionList
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "source_of_water",
                LabelEn = "Source of Water",
                LabelNe = "पानीको स्रोत",
                Description = "Options for source of water"
            },
            new OptionList
            {
                Id = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                Code = "type_of_toilet",
                LabelEn = "Type of Toilet",
                LabelNe = "टॉयलेटको प्रकार",
                Description = "Options for type of toilet"
            },
            new OptionList
            {
                Id = Guid.Parse("33333333-4444-5555-6666-777777777777"),
                Code = "handwash_facility",
                LabelEn = "Handwash Facility",
                LabelNe = "हात धुने सुविधा",
                Description = "Options for handwash facility"
            }
        };
    }
}