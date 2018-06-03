using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryCache;
using System.Collections.Generic;

namespace MemoryCacheTest
{
    public class Key
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    public class KeyComparer : IEqualityComparer<Key>
    {
        public bool Equals(Key l, Key r)
        {
            return (l.x == r.x && l.y == r.y);
        }

        public int GetHashCode(Key obj)
        {
            return obj.x ^ obj.y;
        }
    }
    public class Value
    {
        public int a { get; set; }
        public int b { get; set; }
    }
    [TestClass]
    public class UserDefinedClassAsKey_CacheTest
    {
        [TestMethod]
        public void Size_0_Cache_Add_1_Cache_Size_Should_Be_0()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(0, new KeyComparer());
            cache.AddOrUpdate(new Key { x=1,y=1}, new Value { a = 1, b=1});
            Assert.AreEqual(0, cache.CurrentSize);
        }
        [TestMethod]
        public void Size_1_Cache_Add_1_Cache_Size_Should_Be_1()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(1, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_1_Cache_Add_2_Cache_Size_Should_Be_1()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(1, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 2 });
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_2_Cache_Add_1_Cache_Size_Should_Be_1()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(2, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_2_Cache_Add_2_Cache_Size_Should_Be_2()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(2, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 2 });
            Assert.AreEqual(2, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_2_Cache_Add_1_Update_Value_Cache_Size_Should_Be_1()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(2, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 2, b = 1 });
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_1_Cache_Add_1_Update_Value_To_5_Value_Should_Be_5()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(1, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 5, b = 1 });
            Value value;
            cache.TryGetValue(new Key { x = 1, y = 1 }, out value);
            Assert.AreEqual(5, value.a);
        }

        [TestMethod]
        public void Size_1_Cache_Add_2_Get_Value_1_Should_Return_False()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(1, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            Value value;
            Assert.IsFalse(cache.TryGetValue(new Key { x = 1, y = 1 }, out value));
        }
        [TestMethod]
        public void Size_1_Cache_Add_2_Get_Value_2_Should_Return_2()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(1, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 2, y = 1 }, out value));
            Assert.AreEqual(2, value.a);
        }

        [TestMethod]
        public void Size_3_Cache_Add_2_Get_Value_1_Should_Return_1()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 1, y = 1 }, out value));
            Assert.AreEqual(1, value.a);
        }
        [TestMethod]
        public void Size_3_Cache_Add_2_Update_1_To_11_Get_Value_1_Should_Return_11()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 11, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 1, y = 1 }, out value));
            Assert.AreEqual(11, value.a);
        }
        [TestMethod]
        public void Size_3_Cache_Add_3_Update_1_To_11_Get_Value_1_Should_Return_11()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 3, b = 1 });
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 11, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 1, y = 1 }, out value));
            Assert.AreEqual(11, value.a);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Get_Value_1_Should_Return_False()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 3, b = 1 });
            cache.AddOrUpdate(new Key { x = 4, y = 1 }, new Value { a = 4, b = 1 });
            Value value;
            Assert.IsFalse(cache.TryGetValue(new Key { x = 1, y = 1 }, out value));
        }
        
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_4_To_44_Get_Value_4_Should_Return_44()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 3, b = 1 });
            cache.AddOrUpdate(new Key { x = 4, y = 1 }, new Value { a = 4, b = 1 });
            cache.AddOrUpdate(new Key { x = 4, y = 1 }, new Value { a = 44, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 4, y = 1 }, out value));
            Assert.AreEqual(44, value.a);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Get_Value_4_Should_Return_4()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 3, b = 1 });
            cache.AddOrUpdate(new Key { x = 4, y = 1 }, new Value { a = 4, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 33, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 4, y = 1 }, out value));
            Assert.AreEqual(4, value.a);
        }
        
        
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_1_Should_Return_11()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 3, b = 1 });
            cache.AddOrUpdate(new Key { x = 4, y = 1 }, new Value { a = 4, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 33, b = 1 });
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 11, b = 1 });
            cache.AddOrUpdate(new Key { x = 5, y = 1 }, new Value { a = 5, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 1, y = 1 }, out value));
            Assert.AreEqual(11, value.a);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_3_Should_Return_33()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 3, b = 1 });
            cache.AddOrUpdate(new Key { x = 4, y = 1 }, new Value { a = 4, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 33, b = 1 });
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 11, b = 1 });
            cache.AddOrUpdate(new Key { x = 5, y = 1 }, new Value { a = 5, b = 1 });
            Value value;
            Assert.IsTrue(cache.TryGetValue(new Key { x = 3, y = 1 }, out value));
            Assert.AreEqual(33, value.a);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_2_Should_Return_False()
        {
            MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1 });
            cache.AddOrUpdate(new Key { x = 2, y = 1 }, new Value { a = 2, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 3, b = 1 });
            cache.AddOrUpdate(new Key { x = 4, y = 1 }, new Value { a = 4, b = 1 });
            cache.AddOrUpdate(new Key { x = 3, y = 1 }, new Value { a = 33, b = 1 });
            cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 11, b = 1 });
            cache.AddOrUpdate(new Key { x = 5, y = 1 }, new Value { a = 5, b = 1 });
            Value value;
            Assert.IsFalse(cache.TryGetValue(new Key { x = 2, y = 1 }, out value));
        }
       
    }
}
