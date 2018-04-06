using System.ComponentModel;

namespace Frends.Community.CombineXML
{

    /// <summary>
    /// Input class for Task
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Array of xmls with child element names
        /// </summary>
        public InputXml[] InputXmls { set; get; }

        /// <summary>
        ///  Name for root element
        /// </summary>
        [DefaultValue("\"Root\"")]
        public string XmlRootElementName { set; get; }
    }

    /// <summary>
    /// XML input
    /// </summary>
    public class InputXml
    { /// <summary>
        /// Xml input as string or xml document
        /// </summary>
        public object Xml { set; get; }
        /// <summary>
        /// Child element name where the xml document will be written in
        /// </summary>
        [DefaultValue("\"ChildElement\"")]
        public string ChildElementName { set; get; }
    }
}
