using Furnace.ContentTypes.Model;
using ServiceStack.Text;

namespace Furnace.Boiler.Play.Models.Messages
{
    [Csv(CsvBehavior.FirstEnumerable)]
    public class ItemResponse
    {
        public FurnaceContentType ContentType { get; set; }
    }
}