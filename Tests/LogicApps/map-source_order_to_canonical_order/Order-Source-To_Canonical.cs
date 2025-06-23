using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicApps.Test.Utilities;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LogicApps.Tests
{

    [TestClass]
    public class Order_Source_To_Canonica
    {
        const string configPath = @"map-source_order_to_canonical_order/testSettings.config";
        string rootDirectory
        {
            get; set;
        }
        string logicAppName
        {
            get; set;
        }
        string mapPath
        {
            get; set;
        }
        string map
        {
            get; set;
        }

        [TestInitialize]
        public void Setup()
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            this.rootDirectory = configuration["TestSettings:WorkspacePath"];
            this.logicAppName = configuration["TestSettings:LogicAppName"];
            this.mapPath = configuration["TestSettings:MapPath"];
            this.map = configuration["TestSettings:MapName"];
        }


        [TestMethod]
        public void TestTransformation()
        {
            // Define paths to the XSLT and input XML files
            var xsltPath = Path.Combine(this.rootDirectory, this.logicAppName, this.mapPath, string.Concat(this.map, ".xslt"));
            var inputPath = Path.Combine(this.rootDirectory, "Tests/", this.logicAppName, string.Concat("map-",this.map,"/"),"input-1.json"); 

            // Execute the XSLT transformation
            string result = SaxonMapUtilities.ExecuteXsltTransformation(xsltPath, inputPath);


            // Assert that the result is not null or empty
            Assert.IsFalse(string.IsNullOrEmpty(result), "The transformation result should not be null or empty.");
        }
        
    }
}