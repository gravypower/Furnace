using System.Globalization;

namespace Furnace.Interfaces.Configuration
{
    public interface IFurnaceSiteConfiguration
    {
        CultureInfo DefaultSiteCulture { get; set; }
    }
}
