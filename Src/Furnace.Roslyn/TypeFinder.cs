using System;
using System.Linq;

namespace Furnace.ContentTypes.Roslyn
{
    public interface ITypeFinder
    {
        Type FindType(string fullName);
    }

    public class TypeFinder : ITypeFinder
    {
        public Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
        }
    }
}
