using FluentAssertions;
using System.IO;
using Xunit;

namespace SerializerTests.UnitTests
{
    public class SerializerTests : TestBase
    {
        [Fact(DisplayName = "Serialization and Deserealization")]
        public async void WhenCreateManyListNodes_ThenSerializeAndDeserialize()
        {
            //Arrange
            var subject = GenerateListNodeWithDeep(10);
            using var stream = new MemoryStream();

            //Act
            await Serializer.Serialize(subject, stream);
            var result = await Serializer.Deserialize(new MemoryStream(stream.ToArray()));

            //Assert
            result.Should().BeEquivalentTo(subject, options => options.IgnoringCyclicReferences());
        }

        [Fact(DisplayName = "Serialization and Deserealization when one ListNode")]
        public async void WhenCreateOneListNodes_ThenSerializeAndDeserialize()
        {
            //Arrange
            var subject = GenerateListNodeWithDeep();
            using var stream = new MemoryStream();

            //Act
            await Serializer.Serialize(subject, stream);
            var result = await Serializer.Deserialize(new MemoryStream(stream.ToArray()));

            //Assert
            result.Should().BeEquivalentTo(subject, options => options.IgnoringCyclicReferences());
        }

        [Fact(DisplayName = "Serialization and Deserealization when empty ListNode")]
        public async void WhenCreateEmptyListNodes_ThenSerializeAndDeserialize()
        {
            //Arrange
            var subject = CreateEmptyListNode();
            using var stream = new MemoryStream();

            //Act
            await Serializer.Serialize(subject, stream);
            var result = await Serializer.Deserialize(new MemoryStream(stream.ToArray()));

            //Assert
            result.Should().BeEquivalentTo(subject, options => options.IgnoringCyclicReferences());
        }


        [Fact(DisplayName = "Deep copy")]
        public async void WhenCreateManyListNodes_ThenDeepCopy()
        {
            //Arrange
            var subject = GenerateListNodeWithDeep(10);

            //Act
            var result = await Serializer.DeepCopy(subject);

            //Assert
            result.Should().BeEquivalentTo(subject, options => options.IgnoringCyclicReferences());
            result.Data = result.Data + "Test";
            result.Should().NotBeEquivalentTo(subject, options => options.IgnoringCyclicReferences());
        }

        [Fact(DisplayName = "Deep copy when empty ListNode")]
        public async void WhenCreateEmptyListNodes_ThenDeepCopy()
        {
            //Arrange
            var subject = CreateEmptyListNode();

            //Act
            var result = await Serializer.DeepCopy(subject);

            //Assert
            result.Should().BeEquivalentTo(subject, options => options.IgnoringCyclicReferences());
            result.Data = result.Data + "Test";
            result.Should().NotBeEquivalentTo(subject, options => options.IgnoringCyclicReferences());
        }

        [Fact(DisplayName = "Deserealization and Serialization")]
        public async void WhenCreateMemoryStream_ThenDeserializeAndSerialize()
        {
            //Arrange
            using var subject = GenerateStream();
            using var resultStream = new MemoryStream();

            //Act
            var tmp = await Serializer.Deserialize(subject);
            await Serializer.Serialize(tmp, resultStream);

            //Assert
            resultStream.ToString().Should().BeEquivalentTo(((MemoryStream)subject).ToString());
        }

        [Fact(DisplayName = "Deserealization and Serialization when empty stream")]
        public async void WhenCreateEmptyMemoryStream_ThenDeserializeAndSerialize()
        {
            //Arrange
            using var subject = new MemoryStream();
            using var resultStream = new MemoryStream();

            //Act
            var tmp = await Serializer.Deserialize(subject);
            await Serializer.Serialize(tmp, resultStream);

            //Assert
            resultStream.ToString().Should().BeEquivalentTo(((MemoryStream)subject).ToString());
        }
    }
}
