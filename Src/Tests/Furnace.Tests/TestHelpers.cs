﻿using Furnace.Models.ContentTypes;

namespace Furnace.Tests
{
    public static class TestHelpers
    {
        public static void AddPropity(this ContentType contentType, string propertyName = null, string type = null, object defaltValue = null)
        {
            var property = new Property
            {
                Name = propertyName,
                Type = type,
                DefaultValue = defaltValue
            };

            contentType.Properties.Add(property);
        }
    }
}
