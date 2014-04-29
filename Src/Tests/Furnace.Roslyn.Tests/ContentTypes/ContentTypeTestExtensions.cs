namespace Furnace.Roslyn.Tests.ContentTypes
{
    using System.Collections.Generic;
    using System.Linq;

    using Furnace.ContentTypes.Model;

    public static class ContentTypeTestExtensions
    {
        public static IList<string> GetPropertyNames(this IList<FurnaceContentType> types, string typeName)
        {
            return types.First(x => x.Name == typeName).Properties.Select(x=>x.Name).ToList();
        }

        public static string GetPropertyType(this IList<FurnaceContentType> types, string typeName, string propertyName)
        {
            return types.First(x => x.Name == typeName).Properties.First(x => x.Name == propertyName).Type;
        }

        public static object GetPropertyDefaultValue(this IList<FurnaceContentType> types, string typeName, string propertyName)
        {
            return types.First(x => x.Name == typeName).Properties.First(x => x.Name == propertyName).DefaultValue;
        }
    }
}
