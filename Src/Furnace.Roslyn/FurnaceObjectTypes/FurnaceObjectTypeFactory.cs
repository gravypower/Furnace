using System.Collections.Generic;
using Furnace.Models.Exceptions;

namespace Furnace.ContentTypes.Roslyn.FurnaceObjectTypes
{
    public class FurnaceObjectTypeFactory
    {
        public void ParseFurnaceObjectTypeTemplate(string templtePath)
        {
            throw new TempltePathException(new [] {""});
        }

        public class TempltePathException : ReasonsFurnaceException
        {
            public TempltePathException(IEnumerable<string> reasons) : base(reasons)
            {
            }
        }
    }
}
