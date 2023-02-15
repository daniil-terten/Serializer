using AutoFixture;
using AutoFixture.AutoMoq;
using SerializerTests.extensions;
using SerializerTests.Implementations;
using SerializerTests.Infrastructure;
using SerializerTests.Nodes;
using System.IO;
using System.Text;
using Xunit;

namespace SerializerTests.UnitTests
{
    [Trait("Category", UnitTestCollection.Category)]
    [Collection(UnitTestCollection.Name)]
    public abstract class TestBase
    {
        protected readonly IFixture AutoFixture = new Fixture().Customize(new AutoMoqCustomization());
        protected readonly DaniilTertenSerializer Serializer = new DaniilTertenSerializer();

        public TestBase()
        {
            Serializer = new DaniilTertenSerializer();
        }

        protected ListNode GenerateListNodeWithDeep(int count = 1)
        {
            var node = new ListNode()
            {
                Data = AutoFixture.Create<string>()
            };

            for (int i = 0; i < count-1; i++)
            {
                node = node.AddFirst(AutoFixture.Create<string>());
            }
            return node;
        }

        protected Stream GenerateStream()
        {
            var bytes = Encoding.UTF8.GetBytes(AutoFixture.Create<string>());
            return new MemoryStream(bytes);
        }

        protected ListNode CreateEmptyListNode() => new ListNode();
    }
}
