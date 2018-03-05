using DirParser.Database.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeepEqual;
using DeepEqual.Syntax;
using System.Linq;
using DirParser.NET;

namespace DirParser.Database.NET.Test {
    [TestClass]
    public class DatabaseParserTest {
        [TestMethod]
        public void TEST_EMPTY_REPORT() {
            //Assign
            string input_Content = "";

            DatabaseParseReport output;
            DatabaseParseReport expectedOutput = new DatabaseParseReport(null);

            IDatabaseParser actor = new EntityFrameworkDatabaseParser();

            //Act
            //output = actor.Parse(input_Content);

            //Assert
            //output.ShouldDeepEqual(expectedOutput);
        }

        [TestMethod]
        public void TEST_ONLY_ONE_TABLE_NAME() {
            //Assign
            string input_Content = "ToTable(\"testtablename\",\"informix\");";

            string output;
            string expectedOutput = "testtablename";

            IDatabaseParser actor = new EntityFrameworkDatabaseParser();

            //Act
            //DatabaseParseReport dpr = actor.Parse(input_Content);
            //output = dpr.Tables.First().Name;

            //Assert
            //Assert.AreEqual(expectedOutput, output);
        }
    }
}

