using System.Collections.Generic;
using System.Globalization;
using Furnace.Interfaces.ContentTypes;

namespace Furnace.Interfaces.Items
{
    public interface IFurnaceItems<TKeyType>
    {
        IItem<TKeyType> CreateItem(IContentType contentType);

        IItem<TKeyType> GetItem(TKeyType key, IContentType contentType);
        IItem<TKeyType> GetItem(TKeyType key, IContentType contentType, CultureInfo cultureInfo);

        TRealType GetItem<TRealType>(TKeyType key);
        TRealType GetItem<TRealType>(TKeyType key, CultureInfo cultureInfo);

        void SetItem(TKeyType key, IItem<TKeyType> item);
        IEnumerable<IItem<TKeyType>> GetItemChildren<TRealType>(TKeyType key);
    }
}
