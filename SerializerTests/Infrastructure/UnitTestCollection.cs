using SerializerTests.Implementations;
using SerializerTests.Interfaces;
using Xunit;

namespace SerializerTests.Infrastructure
{
    [CollectionDefinition(Name, DisableParallelization = false)]
    public class UnitTestCollection : ICollectionFixture<DaniilTertenSerializer>
    {
        public const string Name = "Unit tests collection";
        public const string Category = "DaniilTertenSerializerTests";
    }

}
