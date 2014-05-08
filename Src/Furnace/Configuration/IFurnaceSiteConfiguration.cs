using System.Globalization;

namespace Furnace.Configuration
{
    public interface IFurnaceSiteConfiguration
    {
        CultureInfo DefaultSiteCulture { get; set; }
    }
}
