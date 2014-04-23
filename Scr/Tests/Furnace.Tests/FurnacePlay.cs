using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using NUnit.Framework;
using ServiceStack.Redis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceStack.Text;

namespace Furnace.Tests
{
    [TestFixture]
    public class FurnacePlay
    {
        private Assembly _assembly;
        private Project _project;
        private MSBuildWorkspace _workspace;

        //[TestFixtureSetUp]
        //public void TestFixtureSetUp()
        //{
        //    _workspace = MSBuildWorkspace.Create();
        //    _project = _workspace.OpenProjectAsync(@"../../../Furnace.Tests.Models/Furnace.Tests.Models.csproj").Result;

        //    if (_assembly == null)
        //        _project.GetCompilationAsync().Result.Emit(TestContext.CurrentContext.WorkDirectory + "\\" + _project.AssemblyName + ".dll");

        //    _assembly = Assembly.LoadFile(TestContext.CurrentContext.WorkDirectory + "\\" + _project.AssemblyName + ".dll");
        //}


        //[Test]
        public void SomeTest()
        {
            MSBuildWorkspace.Create();
            using (var redisClient = new RedisClient("localhost"))
            {
                redisClient.FlushAll();

                var typeHash = redisClient.Hashes["Types"];

                foreach (var documentId in _project.DocumentIds)
                {
                    var document = _project.GetDocument(documentId);
                    var root = document.GetSyntaxRootAsync().Result;
                    var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

                    if(!classes.Any())
                        continue;
                    
                    var model = document.GetSemanticModelAsync().Result;
                    var structType = model.GetDeclaredSymbol(classes.Single());

                    var typeName = structType.ToDisplayString( );

                    typeHash.Add(Guid.NewGuid().ToString(), typeName);

                    var set = redisClient.Hashes["Types:Model:" + typeName];
                    var mockType = new Dictionary<string, object>();
                    foreach (var property in root.DescendantNodes().OfType<PropertyDeclarationSyntax>())
                    {
                        Console.WriteLine(property.ExplicitInterfaceSpecifier);
                        set.Add(property.Identifier.Text, property.Type.ToString());
                        mockType[property.Identifier.Text] = property.Identifier.Text;
                    }

                    var redisHash = redisClient.Hashes["Types:Values" + typeName];
                    
                    redisHash.Add(Guid.NewGuid().ToString(), JsonSerializer.SerializeToString(mockType));
                }

                foreach (var hashKey in redisClient.GetHashKeys("Types"))
                {
                    var typeName = typeHash[hashKey];
                    var redisHash = redisClient.Hashes["Types:Values" + typeName];
                    
                    foreach (var key in redisHash.Keys)
                    {
                        Console.WriteLine(key);
                        Console.WriteLine(typeName);
                        Console.WriteLine(redisHash[key]);
                        Console.WriteLine(JsonSerializer.DeserializeFromString(redisHash[key], _assembly.GetType(typeName)));
                    }   
                }                
            }
        }
    }
}
