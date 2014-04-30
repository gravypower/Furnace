using System;
using System.Collections.Generic;
using System.Linq;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;
using ServiceStack;

namespace Furnace.Items
{
    public class FurnaceItems : IFurnaceItems
    {
        public Item CreateItem(ContentType contentType)
        {
            GuardContenType(contentType);
            return null;
        }

        private static void GuardContenType(ContentType contentType)
        {
            var errors = new List<string>();
            if (contentType.Namespace.IsNullOrEmpty())
                errors.Add(InvalidContentTypeException.NoNamespace);

            if(contentType.Name.IsNullOrEmpty())
                errors.Add(InvalidContentTypeException.NoName);

            if (errors.Any())
                throw new InvalidContentTypeException(errors);
        }

        public class InvalidContentTypeException : Exception
        {
            public const string NoName = "ContentType had no name";
            public const string NoNamespace = "ContentType had no namespace";

            public IList<string> InvalidReasons;

            public InvalidContentTypeException(IEnumerable<string> reasons)
            {
                InvalidReasons = new List<string>(reasons);

            }
        }
    }
}
