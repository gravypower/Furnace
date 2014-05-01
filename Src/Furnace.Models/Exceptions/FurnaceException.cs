using System;

namespace Furnace.Models.Exceptions
{
    public abstract class FurnaceException : Exception
    {   
        public abstract string ExceptionName { get; }

        public override string Message
        {
            get
            {
                return BuildLogMessage();
            }
        }

        protected abstract string BuildLogMessage();
    }
}
