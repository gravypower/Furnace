using System.Collections.Generic;
using System.Linq;
using Furnace.Models.ContentTypes;
using Furnace.Models.Exceptions;
using Furnace.Models.Items;
using ServiceStack;
using Property = Furnace.Models.ContentTypes.Property;


namespace Furnace.Items
{
    public class FurnaceItems : IFurnaceItems
    {
        public Item CreateItem(ContentType contentType)
        {
            new Guard().GuardContenType(contentType);
            return new Item(contentType);
        }        

        private class Guard
        {
            private readonly List<string> _reasons;

            public Guard()
            {
                _reasons = new List<string>();
            }

            public void GuardContenType(ContentType contentType)
            {
                if (contentType.Namespace.IsNullOrEmpty())
                    _reasons.Add(InvalidContentTypeException.NoNamespace);

                if (contentType.Name.IsNullOrEmpty())
                    _reasons.Add(InvalidContentTypeException.NoName);

                if (!contentType.Properties.Any())
                    _reasons.Add(InvalidContentTypeException.NoProperties);
                else
                    GuardProperties(contentType.Properties);

                if (_reasons.Any())
                    throw new InvalidContentTypeException(_reasons);
            }

            private void GuardProperties(IEnumerable<Property> properties)
            {
                foreach (var property in properties)
                {
                    if(property.Type == null)
                        _reasons.Add(InvalidContentTypeException.PropertyHasNoType.FormatWith(property.Name));

                    if(property.Name.IsNullOrEmpty())
                        _reasons.Add(InvalidContentTypeException.PropertyHasNoName.FormatWith(property.Type));
                }
            }
        }

        public class InvalidContentTypeException : FurnaceException
        {
            public const string InvalidContentTypeExceptionName = "FurnaceItems.InvalidContentTypeException";

            public const string NoName = "ContentType had no name";
            public const string NoNamespace = "ContentType had no namespace";
            public const string NoProperties = "ContentType has no properties";

            public const string PropertyHasNoType = "Propity {0} has no type";
            public const string PropertyHasNoName = "Propity of type {0} has no name";

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
