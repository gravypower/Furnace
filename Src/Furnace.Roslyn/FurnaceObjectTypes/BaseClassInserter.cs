using System.Collections.Generic;
using Furnace.Models.Exceptions;
using Microsoft.CodeAnalysis.CSharp;

namespace Furnace.ContentTypes.Roslyn.FurnaceObjectTypes
{
    public class BaseClassInserter : CSharpSyntaxRewriter
    {
        private string _baseClass;

        public BaseClassInserter(string baseClass)
        {
            GuardBaseClass(baseClass);
            _baseClass = baseClass;
            
        }

        private static void GuardBaseClass(string GuardBaseClass)
        {
            if (GuardBaseClass == null)
                throw new BaseClassException(new[] { BaseClassException.NullBaseClass });

            if (GuardBaseClass == string.Empty)
                throw new BaseClassException(new[] { BaseClassException.EmptyBaseClass });
        }


        public class BaseClassException : ReasonsFurnaceException
        {
            public const string NullBaseClass = "Base class was null";

            public const string EmptyBaseClass = "Base class was Empty";

            public BaseClassException(IEnumerable<string> reasons) : base(reasons)
            {
            }
        }
    }
}
