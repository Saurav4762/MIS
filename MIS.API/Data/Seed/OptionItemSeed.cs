using MIS.API.Models;

namespace MIS.API.Data.Seed
{
    public static class OptionItemSeed
    {
        public static readonly OptionItem[] Data =
        {
            // Source of Water
            new OptionItem
            {
                Id = Guid.Parse("b1e2c3d4-f5a6-4b7c-8d9e-0f1a2b3c4d5e"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "tap_water",
                LabelEn = "Tap Water",
                LabelNe = "ट्याप पानी",
                IsActive = true
            },
            new OptionItem
            {
                Id = Guid.Parse("a6b7c8d9-e0f1-4a2b-3c4d-5e6f7a8b9c0d"),
                OptionListId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                Code = "other",
                LabelEn = "Other",
                LabelNe = "अन्य",
                IsActive = true
            },

            // Type of Toilet
            new OptionItem
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                OptionListId = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                Code = "flush_toilet",
                LabelEn = "Flush Toilet",
                LabelNe = "फ्लश टॉयलेट",
                IsActive = true
            },

            // Handwash Facility
            new OptionItem
            {
                Id = Guid.Parse("e0f1a2b3-c4d5-4e6f-7a8b-9c0d1e2f3a4b"),
                OptionListId = Guid.Parse("33333333-4444-5555-6666-777777777777"),
                Code = "handwash_station",
                LabelEn = "Handwash Station",
                LabelNe = "हात धुने स्टेशन",
                IsActive = true
            }
        };
    }
}