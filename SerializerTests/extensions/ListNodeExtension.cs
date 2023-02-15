using SerializerTests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializerTests.extensions
{
    public static class ListNodeExtension
    {
        public static ListNode AddFirst(this ListNode head, string data)
        {
            ListNode toAdd = new ListNode()
            {
                Data = data,
                Next = head,
                Previous = null,
                Random = head.Random
            };
            head.Previous = toAdd;
            return toAdd;
        }

    }
}
