
using System.Collections.Generic;
using Furnace.Boiler.Models.Play;
using ServiceStack.Text;

namespace Furnace.Boiler.Play.Models.Messages
{
    [Csv(CsvBehavior.FirstEnumerable)]
    public class PageResponse
    {
        public Page Page { get; set; }

        public IList<Page> Pages { get; set; }
    }
}