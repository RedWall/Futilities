using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Xml.Linq;

namespace Futilities.XmlExtensions.Tests
{
    [TestClass]
    public class XmlExtensionTests
    {
        [TestMethod]
        public void GetValue_ReturnsDefaultValue_WhenElementIsNull()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement></RootElement>");

            var result = xml.Elements().FirstOrDefault().Elements().FirstOrDefault().GetValue("empty");

            Assert.AreEqual(result, "empty");
        }

        [TestMethod]
        public void GetValue_ReturnsStringValue_WhenElementHasValue()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement>Some Value</RootElement>");

            var result = xml.Elements().FirstOrDefault().GetValue("empty");

            Assert.AreEqual(result, "Some Value");
        }

        [TestMethod]
        public void GetValueT_ReturnsDefaultValue_WhenElementIsNull()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement></RootElement>");

            var result = xml.Elements().FirstOrDefault().Elements().FirstOrDefault().GetValue<int>((elem) => 1);

            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void GetValueT_ReturnsTransformedValue_WhenElementHasValue()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement>5</RootElement>");

            var result = xml.Elements().FirstOrDefault().GetValue<int>((elem) => int.Parse(elem.Value));

            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void GetValue_ReturnsDefaultValue_WhenAttributeIsNull()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement></RootElement>");

            var result = xml.Elements().FirstOrDefault().Attributes().FirstOrDefault().GetValue("empty");

            Assert.AreEqual(result, "empty");
        }

        [TestMethod]
        public void GetValue_ReturnsStringValue_WhenAttributeHasValue()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement SomeAttribute=\"Some Value\"></RootElement>");

            var result = xml.Elements().FirstOrDefault().Attributes().FirstOrDefault().GetValue("empty");

            Assert.AreEqual(result, "Some Value");
        }

        [TestMethod]
        public void GetValueT_ReturnsDefaultValue_WhenAttributeIsNull()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement></RootElement>");
            var result = xml.Elements().FirstOrDefault().Attributes().FirstOrDefault().GetValue<int>((attr) => 1);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void GetValueT_ReturnsTransformedValue_WhenAttributeHasValue()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement SomeAttr=\"5\"></RootElement>");

            var result = xml.Elements().FirstOrDefault().Attributes().FirstOrDefault().GetValue<int>((attr) => int.Parse(attr.Value));

            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void GetText_ReturnsNull_WhenTextDoesNotExist()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement><NestedElement></NestedElement></RootElement>");

            var result = xml.Elements().FirstOrDefault().GetText();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetText_ReturnsNodeText_WhenTextExists()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement>Some Text<NestedElement></NestedElement></RootElement>");

            var result = xml.Elements().FirstOrDefault().GetText();

            Assert.AreEqual(result, "Some Text");
        }

        [TestMethod]
        public void GetText_ReturnsFirstNodeText_WhenMultipleTextExists()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement>Pre Text<NestedElement></NestedElement>Post Text</RootElement>");

            var result = xml.Elements().FirstOrDefault().GetText();

            Assert.AreEqual(result, "Pre Text");
        }

        [TestMethod]
        public void GetAllText_ReturnsEmptyCollection_WhenNoTextExists()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement><NestedElement></NestedElement></RootElement>");

            var result = xml.Elements().FirstOrDefault().GetAllText().ToList();

            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void GetAllText_ReturnsAllNodeText_WhenMultipleTextExists()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement>Pre Text<NestedElement></NestedElement>Post Text</RootElement>");

            var result = xml.Elements().FirstOrDefault().GetAllText().ToList();

            Assert.AreEqual(result.Count(), 2);

            Assert.AreEqual(result[0], "Pre Text");

            Assert.AreEqual(result[1], "Post Text");
        }

        [TestMethod]
        public void GetAllText_ReturnsText_WhenOnlyPostTextExists()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement><NestedElement></NestedElement>Post Text</RootElement>");

            var result = xml.Elements().FirstOrDefault().GetAllText().ToList();

            Assert.AreEqual(result.Count(), 1);

            Assert.AreEqual(result[0], "Post Text");
        }

        [TestMethod]
        public void GetAllText_ReturnsAllNodeText_WhenMultipleTextAndMultipleElementsExists()
        {
            var xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf - 8\"?><RootElement>Pre Text<NestedElement></NestedElement>Middle Text<NestedElement></NestedElement>Post Text</RootElement>");

            var result = xml.Elements().FirstOrDefault().GetAllText().ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Pre Text");
            Assert.AreEqual(result[1], "Middle Text");
            Assert.AreEqual(result[2], "Post Text");
        }
    }
}
