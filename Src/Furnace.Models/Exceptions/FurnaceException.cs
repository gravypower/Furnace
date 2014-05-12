using System;

namespace Furnace.Models.Exceptions
{
    public abstract class FurnaceException : Exception
    {   
        public override string Message
        {
            get
            {
                var type = GetType();
                var logMessage = BuildLogMessage();
                return type.DeclaringType.Name + "." + type.Name + " thrown." + (logMessage != null ? " " + logMessage : string.Empty);
            }
        }

        protected virtual string BuildLogMessage()
        {
            return null;
        }
    }
}
