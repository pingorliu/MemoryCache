using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCache
{
    /// <summary>
    /// The implementation fo the MemoryCache based on the Eviction Policy
    /// </summary>
    /// <typeparam name="TKey">When Tkey is a Reference Type, Overriding Equals() is required in the definition of TKey class</typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class MemoryCache<TKey, TValue> : ICache<TKey, TValue>
    {
        /// <summary>
        /// Lock for thread-safe
        /// </summary>
        static readonly object _object = new object();

        /// <summary>
        /// Node Type used in Doubly Linked List
        /// </summary>
        private class CacheNode
        {
            public CacheNode previousNode = null;
            public TValue value;
            public TKey key;
            public CacheNode nextNode = null;

        }

        /// <summary>
        /// Head of Doubly Linked List
        /// </summary>
        private CacheNode head = null;

        /// <summary>
        /// Tail of Doubly Linked List
        /// </summary>
        private CacheNode tail = null;

        /// <summary>
        /// The Size Limit of the Memory Cache
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Dictionary for all CacheNode to achieve O(1) for all operations
        /// </summary>
        private Dictionary<TKey, CacheNode> CacheStore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cacheSize">The Cache Size Limit</param>
        public MemoryCache(int cacheSize)
        {
            Size = cacheSize;
            CacheStore = new Dictionary<TKey, CacheNode>();
        }

        public MemoryCache(int cacheSize, IEqualityComparer<TKey> comparer)
        {
            Size = cacheSize;
            CacheStore = new Dictionary<TKey, CacheNode>(comparer);
        }

        public int CurrentSize
        {
            get
            {
                return CacheStore.Count;
            }
        }
        /// <summary>
        /// Adds the value to the cache against the specified key.
        /// If the key already exists, its value is updated.
        /// </summary>
        public void AddOrUpdate(TKey key, TValue value)
        {
            if (Size <= 0)
            {
                Console.WriteLine("Invalid Memory Cache Size");
                return;
            }

            //For thread-safe
            lock (_object)
            {
                CacheNode node = null;
                //Check if the Cache contains this key using Dictionary to achieve O(1)
                if (!CacheStore.ContainsKey(key))
                {
                    //If the Cache does not contain this key, a new Record needs to be added to the Cache
                    //Check the Cache Size to see if we need to remove the least recently added/updated/retrieved item due to the Eviction Policy
                    if (CacheStore.Count == Size)
                    {
                        //If the current Cache is full, the tail of the Doubly Linked List need to be removed/cut due to the Eviction Policy
                        if (tail != null)
                        {
                            var keyToRemove = tail.key;
                            tail = tail.previousNode;
                            if (tail != null)
                            {
                                tail.nextNode = null;
                            }
                            //Remove the record from the Dictionary
                            CacheStore.Remove(keyToRemove);
                        }
                    }
                    //Add a new Node as the Head of the Doubly Linked List due to the Eviction Policy
                    CacheNode new_Head = new CacheNode();
                    new_Head.previousNode = null;
                    new_Head.key = key;
                    new_Head.value = value;
                    new_Head.nextNode = head;
                    if (head != null)
                    {
                        head.previousNode = new_Head;
                    }
                    head = new_Head;
                    if (tail == null)
                    {
                        tail = new_Head;
                    }
                    //Store the record in the Dictionary
                    CacheStore.Add(key, new_Head);
                }
                else
                {
                    node = CacheStore[key];
                    //Update and Change Position
                    //Update
                    node.value = value;
                    ChangeToHead(node);
                }
            }
        }

        /// <summary>
        /// Attempts to gets the value from the cache against the specified key
        /// and returns true if the key existed in the cache.
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            //For thread-safe
            lock (_object)
            {
                //Check if the Cache contains this key using Dictionary to achieve O(1)
                if (CacheStore.ContainsKey(key))
                {
                    var node = CacheStore[key];
                    value = node.value;
                    //Because this node is visisted now, it should become the Head of the Doubly Linked List because of the Eviction Policy
                    ChangeToHead(node);
                    return true;
                }
                else
                {
                    value = default(TValue);
                    return false;
                }
            }
        }

        /// <summary>
        /// Make the the node to be the new Head of the Doubly Linked List
        /// </summary>
        private void ChangeToHead(CacheNode newHead)
        {
            //If there is only ONE node, there is no need to change
            if (CacheStore.Count > 1)
            {
                //If the node is already the Head of the Doubly Linked List, there is no need to change
                if (newHead.previousNode != null)
                {
                    //Change node's previous node and next node linking details if required
                    var pre = newHead.previousNode;
                    pre.nextNode = newHead.nextNode;
                    if (newHead.nextNode != null)
                    {
                        var post = newHead.nextNode;
                        post.previousNode = newHead.previousNode;
                    }
                    else
                    {
                        tail = newHead.previousNode;
                    }

                    //Link the node as the new Head
                    newHead.previousNode = null;
                    newHead.nextNode = head;
                    head.previousNode = newHead;
                    head = newHead;
                }
            }
        }
    }
}
