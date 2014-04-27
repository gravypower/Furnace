using ServiceStack;

namespace Furnace.Boiler.Play.Models.Messages
{
    [Route("/Item")]
    [Route("/Item/{Id}")]
    public class ItemRequest
    {
        public int Id { get; set; }
    }
}