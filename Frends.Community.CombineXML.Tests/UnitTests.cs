using System;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;

namespace Frends.Community.CombineXML.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private string _xmlString1;
        private string _xmlString2;
        private XmlDocument _xmlDoc1 = new XmlDocument();
        private XmlDocument _xmlDoc2 = new XmlDocument();

        private Input _input;
        private InputXml[] _inputXmls = new InputXml[2];

        [SetUp]
        public void Setup()
        {
            _xmlString1 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><bar1>foo1</bar1>";
            _xmlString2 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><bar2>foo2</bar2>";
            _xmlDoc1.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><foo1>bar1</foo1>");
            _xmlDoc2.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><foo2>bar2</foo2>");

            _inputXmls[0] = new InputXml { ChildElementName = "XML1" };
            _inputXmls[1] = new InputXml { ChildElementName = "XML2" };

            _input = new Input {InputXmls = _inputXmls , XmlRootElementName = "Root"};
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public async Task ShouldCombineXmlStrings()
        {
            _inputXmls[0].Xml = _xmlString1;
            _inputXmls[1].Xml = _xmlString2;

            var result = await CombineXMLTask.CombineXML(_input);
            Assert.That(result, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><Root><XML1><bar1>foo1</bar1></XML1><XML2><bar2>foo2</bar2></XML2></Root>"));
        }

        [Test]
        public async Task ShouldCombineXmlDocs()
        {
            _inputXmls[0].Xml = _xmlDoc1;
            _inputXmls[1].Xml = _xmlDoc2;

            var result = await CombineXMLTask.CombineXML(_input);
            Assert.That(result, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><Root><XML1><foo1>bar1</foo1></XML1><XML2><foo2>bar2</foo2></XML2></Root>"));
        }

        [Test]
        public async Task ShouldCombineXmlStringAndXmlDoc()
        {
            _inputXmls[0].Xml = _xmlString1;
            _inputXmls[1].Xml = _xmlDoc1;

            var result = await CombineXMLTask.CombineXML(_input);
            Assert.That(result, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><Root><XML1><bar1>foo1</bar1></XML1><XML2><foo1>bar1</foo1></XML2></Root>"));
        }

        [Test]
        public async Task ShouldCombineWithNewRootAndElementNames()
        {
            _inputXmls[0].Xml = _xmlString1;
            _inputXmls[0].ChildElementName = "NEW_ELEMENT1";
            _inputXmls[1].Xml = _xmlString2;
            _inputXmls[1].ChildElementName = "NEW_ELEMENT2";
            _input.XmlRootElementName = "NEW_ROOT";

            var result = await CombineXMLTask.CombineXML(_input);
            Assert.That(result, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\"?><NEW_ROOT><NEW_ELEMENT1><bar1>foo1</bar1></NEW_ELEMENT1><NEW_ELEMENT2><bar2>foo2</bar2></NEW_ELEMENT2></NEW_ROOT>"));
        }

        [Test]
        public void ShouldNotCombineOtherObjects()
        {
            _inputXmls[0].Xml = _xmlString1;
            _inputXmls[1].Xml = 123456;
            Assert.ThrowsAsync<FormatException>(() =>  CombineXMLTask.CombineXML(_input));

            _inputXmls[0].Xml = new object();
            _inputXmls[1].Xml = _xmlDoc2;
            Assert.ThrowsAsync<FormatException>(() => CombineXMLTask.CombineXML(_input));

        }

    }
}
