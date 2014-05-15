using Microsoft.CodeAnalysis;

namespace Furnace.ContentTypes.Roslyn.Models
{
    public class FurnaceType
    {
        public SyntaxTree SyntaxTree { get; set; }

        public string FullName { get; set; }
    }
}
