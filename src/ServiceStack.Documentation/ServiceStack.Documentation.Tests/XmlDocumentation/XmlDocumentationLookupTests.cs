﻿namespace ServiceStack.Documentation.Tests.XmlDocumentation
{
    using System;
    using Documentation.XmlDocumentation;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class XmlDocumentationLookupTests
    {
        private readonly IXmlDocumentationReader reader = A.Fake<IXmlDocumentationReader>();

        private XmlDocumentationLookup GetSut() => new XmlDocumentationLookup(reader);

        [Fact]
        public void Ctor_ThrowsException_IfDocumentationReaderNull()
        {
            Action action = () => new XmlDocumentationLookup(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_CallsGetXmlDocumentation()
        {
            new XmlDocumentationLookup(reader);
            A.CallTo(() => reader.GetXmlDocumentation()).MustHaveHappened();
        }

        [Fact]
        public void GetXmlMember_ReturnsDefault_IfNullXmlDocumentation()
        {
            A.CallTo(() => reader.GetXmlDocumentation()).Returns(null);
            var sut = GetSut();

            var result = sut.GetXmlMember(typeof (Type));
            result.Should().Be(XmlMember.Default);
        }

        [Fact]
        public void GetXmlMember_ReturnsDefault_IfNullXmlMembers()
        {
            A.CallTo(() => reader.GetXmlDocumentation()).Returns(new XmlDocumentation());
            var sut = GetSut();

            var result = sut.GetXmlMember(typeof(Type));
            result.Should().Be(XmlMember.Default);
        }

        [Fact]
        public void GetXmlMember_ReturnsDefault_IfXmlMemberNotFound()
        {
            var xmlDocumentation = new XmlDocumentation
            {
                Members = new[] { new XmlMember { Name = "Unfindable" } }
            };

            A.CallTo(() => reader.GetXmlDocumentation()).Returns(xmlDocumentation);

            var sut = GetSut();
            var result = sut.GetXmlMember(typeof(Type));
            result.Should().Be(XmlMember.Default);
        }

        [Fact]
        public void GetXmlMember_ReturnsXmlMember_IfFound()
        {
            var xmlMember = new XmlMember { Name = "T:System.Type" };
            var xmlDocumentation = new XmlDocumentation
            {
                Members = new[] { xmlMember }
            };

            A.CallTo(() => reader.GetXmlDocumentation()).Returns(xmlDocumentation);

            var sut = GetSut();
            var result = sut.GetXmlMember(typeof(Type));
            result.Should().Be(xmlMember);
        }
    }
}
