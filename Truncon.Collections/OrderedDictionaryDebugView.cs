using System.Collections.Generic;
using System.Diagnostics;

namespace Truncon.Collections
{
    internal class OrderedDictionaryDebugView<TKey, TValue>
    {
        private readonly OrderedDictionary<TKey, TValue> _dictionary;

        public OrderedDictionaryDebugView(OrderedDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object Items
        {
            get
            {
                KeyValuePair<TKey, TValue>[] items = new KeyValuePair<TKey, TValue>[_dictionary.Count];
                ICollection<KeyValuePair<TKey, TValue>> pairs = _dictionary;
                pairs.CopyTo(items, 0);
                return items;
            }
        }
    }
}
