// 참고:
// LINQ to XML - msdn
// http://msdn.microsoft.com/en-us/library/bb387098

using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class LinqToXmlTests
    {
        [Fact]
        public void FunctionalConstructionTest()
        {
            XElement srcTree = new XElement("Root",
                new XElement("Element", 1),
                new XElement("Element", 2),
                new XElement("Element", 3),
                new XElement("Element", 4),
                new XElement("Element", 5));

            XElement xmlTree = new XElement("Root",
                new XElement("Child", 1),
                new XElement("Child", 2),
                (from el in srcTree.Elements()
                 where (int)el > 2
                 select el));

            Assert.Equal(
                "<Root><Child>1</Child><Child>2</Child><Element>3</Element><Element>4</Element><Element>5</Element></Root>",
                xmlTree.ToString(SaveOptions.DisableFormatting));
        }

        [Fact]
        public void ValidateUsingXsdTest()
        {
            string xsdMarkup =
                @"<xsd:schema xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                   <xsd:element name='Root'>
                    <xsd:complexType>
                     <xsd:sequence>
                      <xsd:element name='Child1' minOccurs='1' maxOccurs='1'/>
                      <xsd:element name='Child2' minOccurs='1' maxOccurs='1'/>
                     </xsd:sequence>
                    </xsd:complexType>
                   </xsd:element>
                  </xsd:schema>";

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(new StringReader(xsdMarkup)));

            bool errors = false;
            XDocument doc1 = new XDocument(
                new XElement("Root",
                    new XElement("Child1", "content1"),
                    new XElement("Child2", "content1")));
            doc1.Validate(
                schemas,
                (o, e) =>
                {
                    errors = true;
                });
            Assert.False(errors);

            XDocument doc2 = new XDocument(
                new XElement("Root",
                    new XElement("Child1", "content1"),
                    new XElement("Child3", "content1")));
            doc2.Validate(
                schemas,
                (o, e) =>
                {
                    errors = true;
                });

            Assert.True(errors, "Child1, 2만 element로 가질 수 있게 XSD에 정의");
        }

        XElement CreateTestXml()
        {
            string rawXml =
                @"<?xml version='1.0'?>
                    <PurchaseOrder PurchaseOrderNumber='99503' OrderDate='1999-10-20'>
                      <Address Type='Shipping'>
                        <Name>Ellen Adams</Name>
                        <Street>123 Maple Street</Street>
                        <City>Mill Valley</City>
                        <State>CA</State>
                        <Zip>10999</Zip>
                        <Country>USA</Country>
                      </Address>
                      <Address Type='Billing'>
                        <Name>Tai Yee</Name>
                        <Street>8 Oak Avenue</Street>
                        <City>Old Town</City>
                        <State>PA</State>
                        <Zip>95819</Zip>
                        <Country>USA</Country>
                      </Address>
                      <DeliveryNotes>Please leave packages in shed by driveway.</DeliveryNotes>
                      <Items>
                        <Item PartNumber='872-AA'>
                          <ProductName>Lawnmower</ProductName>
                          <Quantity>1</Quantity>
                          <USPrice>148.95</USPrice>
                          <Comment>Confirm this is electric</Comment>
                        </Item>
                        <Item PartNumber='926-AA'>
                          <ProductName>Baby Monitor</ProductName>
                          <Quantity>2</Quantity>
                          <USPrice>39.98</USPrice>
                          <ShipDate>1999-05-21</ShipDate>
                        </Item>
                      </Items>
                    </PurchaseOrder>";

            return XElement.Parse(rawXml);
        }

        [Fact]
        public void ElementsCollectionTest()
        {
            XElement po = CreateTestXml();

            IEnumerable<XElement> children =
                from el in po.Elements()
                select el;

            IEnumerator<XElement> etor = children.GetEnumerator();

            etor.MoveNext();
            Assert.Equal("Address", etor.Current.Name);

            etor.MoveNext();
            Assert.Equal("Address", etor.Current.Name);

            etor.MoveNext();
            Assert.Equal("DeliveryNotes", etor.Current.Name);

            etor.MoveNext();
            Assert.Equal("Items", etor.Current.Name);

            Assert.False(etor.MoveNext());
        }

        [Fact]
        public void ElementValueTest()
        {
            XElement po = CreateTestXml();
            Assert.Equal("Ellen Adams", (string)po.Element("Address").Element("Name"));
            Assert.True(
                10999 == (int)po.Element("Address").Element("Zip"),
                "XElement는 cast operator를 구현했다. Value property는 string. 바로 cast해서 쓰는게 편리");

            // cast 지원 타입
            // string, bool, bool?, int, int?, uint, uint?, long, long?, 
            // ulong, ulong?, float, float?, double, double?, decimal, decimal?, 
            // DateTime, DateTime?, TimeSpan, TimeSpan?, GUID, GUID?.

            // Element는 child element가 여러개일때, 첫번째 element를 리턴
            // "nullable type으로 캐스팅하는 게 안전하다. Element가 없는 경우에도 대처"
            Assert.Null(
                (int?)po.Element("Address").Element("ohyecloudy"));
        }
        
        [Fact]
        public void ElementValueExceptionTest()
        {
            XElement po = CreateTestXml();
            // "ValueType으로 캐스팅하면 Element가 없는 경우 예외를 던진다"
            Assert.Throws<ArgumentNullException>(() => (int)po.Element("Address").Element("ohyecloudy"));
        }

        [Fact]
        public void FilterOnElementNamesTest()
        {
            XElement po = CreateTestXml();
            IEnumerator<XElement> etor =
                (from el in po.Descendants("ProductName")
                 select el).GetEnumerator();

            etor.MoveNext();
            // "child가 아니라 자손임을 명심. leaf까지 검색해서 동일한 이름을 가진 Element를 소스 시퀀스에 넣는다."
            Assert.Equal(
                "Lawnmower",
                (string)etor.Current);

            etor.MoveNext();
            Assert.Equal("Baby Monitor", (string)etor.Current);

            Assert.False(etor.MoveNext());

            // "Elements()는 자식만 순회"
            Assert.Empty((from el in po.Elements("ProductName") select el));
        }

        [Fact]
        public void ChainAxisMethodCallsTest()
        {
            XElement po = CreateTestXml();

            // XElement로 로드하면 Root Element를 XElement로 할당
            IEnumerable<string> names =
                from el in po
                    .Elements("Address")
                    .Elements("Name")
                select (string)el;

            string[] expected = { "Ellen Adams", "Tai Yee" };
            Assert.Equal(expected, names.ToArray());
        }

        [Fact]
        public void AttributesCollectionTest()
        {
            XElement val =
                new XElement("Value",
                    new XAttribute("ID", "1243"),
                    new XAttribute("Type", "int"),
                    new XAttribute("ConvertableTo", "double"),
                    "100");

            IEnumerable<XAttribute> attrs =
                from att in val.Attributes()
                select att;

            // cast 지원 타입 (XElement와 동일)
            // string, bool, bool?, int, int?, uint, uint?, long, long?, 
            // ulong, ulong?, float, float?, double, double?, decimal, decimal?, 
            // DateTime, DateTime?, TimeSpan, TimeSpan?, GUID, GUID?.

            IEnumerator<XAttribute> etor = attrs.GetEnumerator();
            etor.MoveNext();
            Assert.Equal("ID", etor.Current.Name);
            Assert.Equal(1243, (int)etor.Current);

            // 2개 더 있어.
            etor.MoveNext();
            etor.MoveNext();
            Assert.False(etor.MoveNext());

            Assert.True(100 == (int)val, "XAttribute 뒤에 온 100은 value로 저장");
        }

        [Fact]
        public void DictionaryToXmlTest()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Child1", "Value1");
            dict.Add("Child2", "Value2");
            dict.Add("Child3", "Value3");
            dict.Add("Child4", "Value4");

            XElement root =
                new XElement("Root",
                    from keyValue in dict
                    select new XElement(keyValue.Key, keyValue.Value));
            Assert.Equal(
                "<Root><Child1>Value1</Child1><Child2>Value2</Child2><Child3>Value3</Child3><Child4>Value4</Child4></Root>",
                root.ToString(SaveOptions.DisableFormatting));
        }

        [Fact]
        public void UsingXPathTest()
        {
            XElement root = new XElement("Root",
                new XElement("Child1", 1),
                new XElement("Child1", 2),
                new XElement("Child1", 3),
                new XElement("Child2", 4),
                new XElement("Child2", 5),
                new XElement("Child2", 6)
            );

            IEnumerable<XElement> list = root.XPathSelectElements("./Child2");
            Assert.Equal(3, list.Count());
        }
    }
}
