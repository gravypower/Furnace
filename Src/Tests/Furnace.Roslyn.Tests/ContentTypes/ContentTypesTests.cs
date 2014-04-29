﻿namespace Furnace.Roslyn.Tests.ContentTypes
{
    using Furnace.ContentTypes;
    using Furnace.Roslyn;

    using NUnit.Framework;

    [TestFixture]
    public abstract class ContentTypesTests
    {
        private const string BaseProjectPath = @"C:\GitHub\Furnace\Src\Tests\TestProjects\";
        //private const string BaseProjectPath = @"D:\GitHub\Furnace\Scr\Tests\TestProjects\";
        //private const string BaseProjectPath = @"E:\Dev\Src\Tests\TestProjects\";

        protected abstract string ProjectPath { get; }
        protected IFurnaceContentTypes Sut;

        [SetUp]
        public void SetUp()
        {
            this.Sut = new RoslynContentTypes(BaseProjectPath + this.ProjectPath);
        }
    }
}