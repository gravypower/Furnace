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
                var logMessage = BuildLogMessage();
                return ExceptionName + " thrown." + (logMessage != null ? " " + logMessage : string.Empty);
            }
        }

        protected virtual string BuildLogMessage()
        {
            return null;
        }
    }
}
