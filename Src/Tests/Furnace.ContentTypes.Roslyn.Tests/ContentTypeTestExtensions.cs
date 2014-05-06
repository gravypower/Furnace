using System.Collections.Generic;
using System.Linq;
using Furnace.Models.ContentTypes;

namespace Furnace.ContentTypes.Roslyn.Tests
{
    public static class ContentTypeTestExtensions
    {
        public static IList<string> GetPropertyNames(this IList<ContentType> types, string typeName)
        {
            return types.First(x => x.Name == typeName).Properties.Select(x=>x.Name).ToList();
        }

        public static string GetPropertyType(this IList<ContentType> types, string typeName, string propertyName)
        {
            return types.First(x => x.Name == typeName).Properties.First(x => x.Name == propertyName).Type;
        }

        public static object GetPropertyDefaultValue(this IList<ContentType> types, string typeName, string propertyName)
        {
            return types.First(x => x.Name == typeName).Properties.First(x => x.Name == propertyName).DefaultValue;
        }
    }
}
