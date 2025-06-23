using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Saxon.Api;

namespace LogicApps.Test.Utilities
{

    public class SaxonMapUtilities
    {
        public static string ExecuteXsltTransformation(string xsltPath, string inputXmlPath)
        {
            try
            {
                // Create Saxon processor
                var processor = new Processor();

                // Compile the XSLT stylesheet
                var compiler = processor.NewXsltCompiler();
                var xsltStream = new FileStream(xsltPath, FileMode.Open, FileAccess.Read);
                var executable = compiler.Compile(xsltStream);

                // Load the input XML document
                var documentBuilder = processor.NewDocumentBuilder();
                var inputDocument = documentBuilder.Build(new Uri(Path.GetFullPath(inputXmlPath)));

                // Create transformer and set input
                var transformer = executable.Load();
                transformer.InitialContextNode = inputDocument;

                // Create output destination
                var resultWriter = new StringWriter();
                var serializer = processor.NewSerializer(resultWriter);

                // Execute transformation
                transformer.Run(serializer);

                xsltStream.Close();

                return resultWriter.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"XSLT transformation failed: {ex.Message}", ex);
            }
        }


    }
}