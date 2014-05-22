using System.Globalization;
using Furnace.Interfaces.Configuration;

namespace Furnace.Boiler.Play
{
    public class FurnaceSiteConfiguration : IFurnaceSiteConfiguration
    {
        public CultureInfo DefaultSiteCulture { get; set; }

        public FurnaceSiteConfiguration(CultureInfo culture)
        {
            DefaultSiteCulture = culture;
        }
    }
}
