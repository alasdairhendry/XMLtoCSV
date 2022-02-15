using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace XMLtoCSV
{
    public static class Convertor
    {
        public static void Run(string filePath)
        {
            // Load xml
            Console.WriteLine("Run");
            XDocument xDocument = XDocument.Load(filePath);

            // Convert
            string data = GenerateString(xDocument);

            // Do whatever it is you want to do with the results
            if (string.IsNullOrWhiteSpace(data))
            {
                Log.WriteLineColour("No data found", ConsoleColor.Red);
            }
            else
            {
                Log.WriteLineColour("Read success", ConsoleColor.Green);
                Console.WriteLine(data);

            }

            var testpath = Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)} Generated File.csv");
            File.WriteAllText(testpath, data);
        }

        private static string GenerateString(XDocument xDoc)
        {
            var data = new StringBuilder();

            List<string> items = new List<string>();

            int count = 0;
            int maxCount = int.MaxValue;
            string nullValue = "";

            items.Add(string.Join(",", 
                "Record Reference",
                "ISBN",
                "Person Name",
                "Key Name",
                "Language",
                "Subject Scheme Identifier",
                "Subject Scheme Version", 
                "Subject Heading Text",
                "Audience Code Type",
                "Audience Code Value",
                // "Text",
                "Imprint Name",
                "Publisher Name",
                "Publication Country"
                ));

            foreach (var item in xDoc.Descendants("Product"))
            {
                List<string> innerString = new List<string>();

                innerString.Add((string)item.Element("RecordReference") ?? nullValue);
                innerString.Add((string)item.Element("ProductIdentifier")?.Element("IDValue") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Contributor")?.Element("PersonName") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Contributor")?.Element("KeyNames") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Language")?.Element("LanguageCode") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Subject")?.Element("SubjectSchemeIdentifier") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Subject")?.Element("SubjectSchemeVersion") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Subject")?.Element("SubjectHeadingText") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Audience")?.Element("AudienceCodeType") ?? nullValue);
                innerString.Add((string)item.Element("DescriptiveDetail")?.Element("Audience")?.Element("AudienceCodeValue") ?? nullValue);
                // innerString.Add((string)item.Element("CollateralDetail")?.Element("TextContent")?.Element("Text") ?? nullValue);
                innerString.Add((string)item.Element("PublishingDetail")?.Element("Imprint")?.Element("ImprintName") ?? nullValue);
                innerString.Add((string)item.Element("PublishingDetail")?.Element("Publisher")?.Element("PublisherName") ?? nullValue);
                innerString.Add((string)item.Element("PublishingDetail")?.Element("CountryOfPublication") ?? nullValue);

                for (int i = 0; i < innerString.Count; i++)
                {
                    innerString[i] = innerString[i].Replace(",", "");
                }
                
                items.Add(string.Join(",", innerString));
                count++;

                if (count >= maxCount) break;
            }


            return string.Join(Environment.NewLine, items);
        }
    }
}
