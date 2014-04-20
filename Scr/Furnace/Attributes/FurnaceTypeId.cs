using System;

namespace Furnace.Attributes
{
    public class FurnaceTypeId : Attribute
    {
        private Guid _furnaceTypeId;

        public FurnaceTypeId(string furnaceTypeId)
        {
            _furnaceTypeId = Guid.Parse(furnaceTypeId);
        }
    }
}
