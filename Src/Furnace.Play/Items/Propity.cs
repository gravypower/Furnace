using Furnace.Models.ContentTypes;

namespace Furnace.Play
{
    public class Propity
    {
        private Property _property;

        public Propity(Property property)
        {
            _property = property;
        }

        public object Value { get; set; }
    }
}
