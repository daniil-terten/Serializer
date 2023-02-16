using System.IO;
using System.Threading.Tasks;
using SerializerTests.Interfaces;
using SerializerTests.Nodes;

namespace SerializerTests.Implementations
{
    public class DaniilTertenSerializer : IListSerializer
    {
        public DaniilTertenSerializer()
        {

        }

        public async Task<ListNode> DeepCopy(ListNode head)
        {
            using var stream = new MemoryStream();
            await Serialize(head, stream);
            return await Deserialize(new MemoryStream(stream.ToArray()));
        }

        public Task<ListNode> Deserialize(Stream s)
        {
            var listNode = new ListNode() { Previous = null };
            using var reader = new StreamReader(s);
            string line;

            while ((line = reader.ReadLine()) is not null)
            {
                listNode.Data = line == "" ? null : line;
                var next = new ListNode();
                listNode.Next = next;
                next.Previous = listNode;
                listNode = next;
            }
            listNode = listNode.Previous ?? listNode;
            listNode.Next = null;
            

            while(listNode.Previous is not null) listNode = listNode.Previous;

            return Task.FromResult(listNode);
        }

        public async Task Serialize(ListNode head, Stream s)
        {
            while (head.Previous is not null) head = head.Previous;

            using StreamWriter writer = new StreamWriter(s);
            do
            {
                await writer.WriteLineAsync(head.Data?.ToString());
                head = head.Next;
            } while (head != null);
        }
    }
}
