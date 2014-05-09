using System.Collections.Generic;
using System.IO;
using Furnace.Models.Exceptions;

namespace Furnace.ContentTypes.Roslyn.FurnaceObjectTypes
{
    public class FurnaceObjectTypeFactory
    {
        private string _templteFilePath;

        public FurnaceObjectTypeFactory(string templteFilePath)
        {
            GuardTemplatePath(templteFilePath);
            _templteFilePath = templteFilePath;
        }

        public void ParseFurnaceObjectTypeTemplate()
        {
            
        }

        private static void GuardTemplatePath(string templtePath)
        {
            if (templtePath == null)
                throw new TempltePathException(new[] {TempltePathException.NullTempltePath});

            if (templtePath == string.Empty)
                throw new TempltePathException(new[] {TempltePathException.EmptyTempltePath});

            if (!File.Exists(templtePath))
                throw new TempltePathException(new[] { TempltePathException.InvalidTempltePath });
        }

        public class TempltePathException : ReasonsFurnaceException
        {
            public const string NullTempltePath = "Templte path was null";

            public const string EmptyTempltePath = "Templte path was Empty";

            public const string InvalidTempltePath = "Templte path was Empty";

            public TempltePathException(IEnumerable<string> reasons) : base(reasons)
            {
            }
        }
    }
}
