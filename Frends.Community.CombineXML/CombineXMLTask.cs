using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Frends.Community.CombineXML
{
    /// <summary>
    /// Combines two or more xml strings or xml documents to one xml string
    /// </summary>
    public class CombineXMLTask
    {
        /// <summary>
        /// Combines two or more xml strings or xml documents to one xml string
        /// </summary>
        /// <param name="input">Xml strings or xml documents that will be merged</param>
        /// <returns>Xml string of combined xml</returns>
        public static async Task<string> CombineXML(Input input)
        {
            var inputXmls = input.InputXmls;

            // Check invalid inputs
            var invalids = inputXmls.Where(f => f.Xml.GetType() != typeof(string) && f.Xml.GetType() != typeof(XmlDocument)).Select(f => f.ChildElementName).ToList();
            if (invalids.Any())
            {
                throw new FormatException("Unsupported input type found in ChildElements: " + string.Join(", ", invalids) + ". The supported types are XmlDocument and String.");
            }

            // Combine
            using (var sw = new UTF8StringWriter())
            {
                using (var xw = XmlWriter.Create(sw, new XmlWriterSettings { Async = true }))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement(input.XmlRootElementName);

                    foreach (var xml in inputXmls)
                    {
                        xw.WriteStartElement(xml.ChildElementName);

                        var xmlDoc = new XmlDocument();
                        if (xml.Xml.GetType() == typeof(XmlDocument))
                        {
                            xmlDoc = (XmlDocument)xml.Xml;
                        }
                        else
                        {
                            xmlDoc.LoadXml((string)xml.Xml);
                        }
                        using (var xr = new XmlNodeReader(xmlDoc))
                        {
                            xr.Read();
                            if (xr.NodeType.Equals(XmlNodeType.XmlDeclaration))
                            {
                                xr.Read();
                            }
                            await xw.WriteNodeAsync(xr, false).ConfigureAwait(false);
                        }
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                    xw.WriteEndDocument();
                }
                return sw.ToString();
            }
        }
    }
}
