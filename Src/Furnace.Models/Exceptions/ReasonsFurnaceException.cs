using System.Collections.Generic;
using ServiceStack;

namespace Furnace.Models.Exceptions
{
    public abstract class ReasonsFurnaceException : FurnaceException
    {
        public IEnumerable<string> InvalidReasons;

        protected ReasonsFurnaceException(IEnumerable<string> reasons)
        {
            InvalidReasons = reasons;
        }

        protected override string BuildLogMessage()
        {
            return "InvalidReasons: " + InvalidReasons.Join(", ");
        }
    }
}
