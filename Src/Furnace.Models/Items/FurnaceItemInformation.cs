﻿using Furnace.Interfaces.Items;

namespace Furnace.Models.Items
{
    public class FurnaceItemInformation<TKeyType> : IFurnaceItemInformation<TKeyType>
    {
        public TKeyType Id { get; set; }
        public string ContentTypeFullName { get; set; }
        public string ContentTypeVersion { get; set; }
    }
}
