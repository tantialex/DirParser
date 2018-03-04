using DeepEqual.Syntax;
using DirParser.Core;
using DirParser.Database.Core;
using DirParser.Project.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.NET.Test {
    [TestClass]
    public class DatabaseParserTest {
        [TestMethod]
        public void TEST_NETCORE_CSPROJ() {
            //Assign
            string input_Content = "<Project Sdk=\"Microsoft.NET.Sdk\">\r\n\r\n  <PropertyGroup>\r\n    <OutputType>Library</OutputType>\r\n    <TargetFramework>netcoreapp2.0</TargetFramework>\r\n    <ApplicationIcon />\r\n    <StartupObject />\r\n  </PropertyGroup>\r\n\r\n  <ItemGroup>\r\n    <ProjectReference Include=\"..\\..\\Core\\DirParser.Core\\DirParser.Core.csproj\" />\r\n  </ItemGroup>\r\n\r\n</Project>\r\n";
            string input_Name = "test.csproj";
            DirFile input = new DirFile(input_Name, input_Content);

            ProjectParseReport output;
            ProjectParseReport expectedOutput = new ProjectParseReport("test", new ReferenceParseReport[] { new ReferenceParseReport("DirParser.Core", "csproj") });

            IProjectParser actor = new NETCoreProjectParser();

            //Act
            output = actor.Parse(input);

            //Assert
            output.ShouldDeepEqual(expectedOutput);
        }
    }
}
