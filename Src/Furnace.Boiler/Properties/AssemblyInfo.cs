using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using Furnace.Boiler.Models.Play;
using Furnace.Interfaces.Items;
using Furnace.Items.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;

[assembly: AssemblyTitle("Furnace.Boiler")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Furnace.Boiler")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e5e91b19-73df-48ca-ba82-179cf9597dc9")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


public static class extions
{
    public static long GetId<T>(this T test)
        where T : Page
    {
        return ((IFurnaceItemInformation<long>)test).Id;
    }

    public static void Save(this IItem<long> item, IRedisClient client)
    {
        var key = RedisBackedFurnaceItems.CreateItemKey(item.Id, item.ContentType);
        item.Propities.Add("FurnaceItemInformation", item.FurnaceItemInformation);
        client.Hashes[key].Add("en-AU", TypeSerializer.SerializeToString(item.Propities));

        var setKey = RedisBackedFurnaceItems.CreateItemChildrenKey(
            item.FurnaceItemInformation.ParentId,
            item.FurnaceItemInformation.ParentContentTypeFullName);

        client.SortedSets[setKey].Add(key);
    }
}