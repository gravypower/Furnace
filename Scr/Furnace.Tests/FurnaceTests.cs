using System;
using System.Collections.Generic;
using System.IO;
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
    public class FurnaceTests
    {
        private Assembly _assembly;
        private Project _project;
        private MSBuildWorkspace _workspace;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _workspace = MSBuildWorkspace.Create();
            _project = _workspace.OpenProjectAsync(@"../../../Furnace.Tests.Models/Furnace.Tests.Models.csproj").Result;

            if (_assembly == null)
                _project.GetCompilationAsync().Result.Emit(TestContext.CurrentContext.WorkDirectory + "\\" + _project.AssemblyName + ".dll");

            _assembly = Assembly.LoadFile(TestContext.CurrentContext.WorkDirectory + "\\Furnace.Tests.Models.dll");
        }

        [Test]
        public void SomeTest()
        {
            using (var redisClient = new RedisClient("localhost"))
            {
                redisClient.FlushAll();

                foreach (var documentId in _project.DocumentIds)
                {
                    var document = _project.GetDocument(documentId);
                    var root = document.GetSyntaxRootAsync().Result;
                    var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

                    if(!classes.Any())
                        continue;
                    
                    var model = document.GetSemanticModelAsync().Result;
                    var structType = model.GetDeclaredSymbol(classes.Single());

                    var typeName = structType.ToDisplayString();

                    var set = redisClient.Hashes["Types:" + typeName + ":Model"];
                    var d = new Dictionary<string, object>();
                    foreach (var property in root.DescendantNodes().OfType<PropertyDeclarationSyntax>())
                    {
                        set.Add(property.Identifier.Text, property.Type.ToString());
                        d[property.Identifier.Text] = property.Identifier.Text;
                    }

                    var redisHash = redisClient.Hashes["Types:" + typeName + ":Values"];
                    
                    redisHash.Add(Guid.NewGuid().ToString(), JsonSerializer.SerializeToString(d));

                    foreach (var item in redisClient.GetHashKeys("Types:" + typeName + ":Values"))
                    {
                        Console.WriteLine(item);
                        Console.WriteLine(typeName);
                        Console.WriteLine(redisHash[item]); 
                        Console.WriteLine(JsonSerializer.DeserializeFromString(redisHash[item], _assembly.GetType(typeName)));
                    }
                }
            }
        }
    }
}
