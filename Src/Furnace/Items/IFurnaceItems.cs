﻿using System;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Items
{
    public interface IFurnaceItems
    {
        Item CreateItem(ContentType contentType);
        Item GetItem(Guid guid, ContentType contentType);
    }
}
