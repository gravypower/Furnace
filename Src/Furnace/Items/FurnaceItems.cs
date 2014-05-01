using System.Collections.Generic;
using System.Linq;
using Furnace.Models.ContentTypes;
using Furnace.Models.Exceptions;
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

            if(!contentType.Properties.Any())
                errors.Add(InvalidContentTypeException.NoProperties);

            if (errors.Any())
                throw new InvalidContentTypeException(errors);
        }

        public class InvalidContentTypeException : FurnaceException
        {
            public const string InvalidContentTypeExceptionName = "FurnaceItems.InvalidContentTypeException";

            public const string NoName = "ContentType had no name";
            public const string NoNamespace = "ContentType had no namespace";
            public const string NoProperties = "ContentType has no properties";

            public IEnumerable<string> InvalidReasons;

            public InvalidContentTypeException(IEnumerable<string> reasons)
            {
                InvalidReasons = reasons;
            }

            public override string ExceptionName
            {
                get { return InvalidContentTypeExceptionName; }
            }

            protected override string BuildLogMessage()
            {
                return "FurnaceItems.InvalidContentTypeException thrown, InvalidReasons: " + InvalidReasons.Join(", ");
            }
        }
    }
}
