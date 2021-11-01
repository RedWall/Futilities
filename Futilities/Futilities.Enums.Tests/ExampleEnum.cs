using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Futilities.Enums.Tests
{
    enum ExampleEnum
    {
        [Display(Name = TestConstants.DISPLAY_NAME)]
        HasDisplayAttribute,
        [Display(Description = TestConstants.DISPLAY_DESCRIPTION)]
        HasDisplayDescriptionAttribute,
        DoesNotHaveAttribute
    }
}
