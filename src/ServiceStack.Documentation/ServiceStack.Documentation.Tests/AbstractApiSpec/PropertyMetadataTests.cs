﻿namespace ServiceStack.Documentation.Tests.AbstractApiSpec
{
    using Documentation.AbstractApiSpec;
    using FluentAssertions;
    using Xunit;

    public class PropertyMetadataTests
    {
        private static PropertyMetadata GetPropertyMetadata()
            => new PropertyMetadata(typeof (MetadataTestClass).GetProperty("Name"));

        [Fact]
        public void With_ReturnsSelf()
        {
            var metadata = GetPropertyMetadata();
            var result = metadata.With(x => x.Title, "whatever");

            result.Should().Be(metadata);
        }

        [Fact]
        public void With_SetsTitle()
        {
            var metadata = GetPropertyMetadata();
            const string title = "My Title";
            metadata.With(x => x.Title, title);

            metadata.Title.Should().Be(title);
        }

        [Fact]
        public void With_SetsDescription()
        {
            var metadata = GetPropertyMetadata();
            const string desc = "My Generation";
            metadata.With(x => x.Description, desc);

            metadata.Description.Should().Be(desc);
        }

        [Fact]
        public void With_SetsIsRequired()
        {
            var metadata = GetPropertyMetadata();
            metadata.With(x => x.IsRequired, true);

            metadata.IsRequired.Should().BeTrue();
        }
    }

    public class MetadataTestClass
    {
        public string Name { get; set; }
    }
}
