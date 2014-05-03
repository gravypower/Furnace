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
                return ExceptionName + " thrown. " + BuildLogMessage();
            }
        }

        protected virtual string BuildLogMessage()
        {
            return string.Empty;
        }
    }
}
