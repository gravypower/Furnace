using Furnace.ContentTypes.Model;

namespace Furnace.Play
{
    public class Propity
    {
        private FurnaceContentTypeProperty _property;

        public Propity(FurnaceContentTypeProperty property)
        {
            _property = property;
        }

        public object Value { get; set; }
    }
}
