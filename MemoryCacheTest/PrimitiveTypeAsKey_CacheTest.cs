using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryCache;

namespace MemoryCacheTest
{
    [TestClass]
    public class PrimitiveTypeAsKey_CacheTest
    {
        [TestMethod]
        public void Size_0_Cache_Add_1_Cache_Size_Should_Be_0()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(0);
            cache.AddOrUpdate(1, 1);
            Assert.AreEqual(0, cache.CurrentSize);
        }
        [TestMethod]
        public void Size_1_Cache_Add_1_Cache_Size_Should_Be_1()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(1);
            cache.AddOrUpdate(1, 1);
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_1_Cache_Add_2_Cache_Size_Should_Be_1()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(1);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_2_Cache_Add_1_Cache_Size_Should_Be_1()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(2);
            cache.AddOrUpdate(1, 1);
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_2_Cache_Add_2_Cache_Size_Should_Be_2()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(2);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            Assert.AreEqual(2, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_2_Cache_Add_1_Update_Value_Cache_Size_Should_Be_1()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(2);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(1, 2);
            Assert.AreEqual(1, cache.CurrentSize);
        }

        [TestMethod]
        public void Size_1_Cache_Add_1_Update_Value_To_5_Value_Should_Be_5()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(1);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(1, 5);
            int value;
            cache.TryGetValue(1, out value);
            Assert.AreEqual(5, value);
        }

        [TestMethod]
        public void Size_1_Cache_Add_2_Get_Value_1_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(1);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            int value;
            Assert.IsFalse(cache.TryGetValue(1, out value));
        }
        [TestMethod]
        public void Size_1_Cache_Add_2_Get_Value_2_Should_Return_2()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(1);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            int value;
            Assert.IsTrue(cache.TryGetValue(2, out value));
            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Size_3_Cache_Add_2_Get_Value_1_Should_Return_1()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            int value;
            Assert.IsTrue(cache.TryGetValue(1, out value));
            Assert.AreEqual(1, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_2_Update_1_To_11_Get_Value_1_Should_Return_11()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(1, 11);
            int value;
            Assert.IsTrue(cache.TryGetValue(1, out value));
            Assert.AreEqual(11, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_3_Update_1_To_11_Get_Value_1_Should_Return_11()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(1, 11);
            int value;
            Assert.IsTrue(cache.TryGetValue(1, out value));
            Assert.AreEqual(11, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Get_Value_1_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            int value;
            Assert.IsFalse(cache.TryGetValue(1, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Get_Value_2_Should_Return_True()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            int value;
            Assert.IsTrue(cache.TryGetValue(2, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Get_Value_3_Should_Return_True()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            int value;
            Assert.IsTrue(cache.TryGetValue(3, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Get_Value_4_Should_Return_True()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            int value;
            Assert.IsTrue(cache.TryGetValue(4, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_4_To_44_Get_Value_4_Should_Return_44()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(4, 44);
            int value;
            Assert.IsTrue(cache.TryGetValue(4, out value));
            Assert.AreEqual(44, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Get_Value_4_Should_Return_4()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            int value;
            Assert.IsTrue(cache.TryGetValue(4, out value));
            Assert.AreEqual(4, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Get_Value_2_Should_Return_2()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            int value;
            Assert.IsTrue(cache.TryGetValue(2, out value));
            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Get_Value_3_Should_Return_33()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            int value;
            Assert.IsTrue(cache.TryGetValue(3, out value));
            Assert.AreEqual(33, value);
        }

        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Get_Value_1_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            int value;
            Assert.IsFalse(cache.TryGetValue(1, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_2_To_22_Update_3_To_33_Get_Value_1_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(2, 22);
            int value;
            Assert.IsFalse(cache.TryGetValue(1, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_2_To_22_Update_3_To_33_Get_Value_2_Should_Return_22()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(2, 22);
            int value;
            Assert.IsTrue(cache.TryGetValue(2, out value));
            Assert.AreEqual(22, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_2_To_22_Update_3_To_33_Get_Value_3_Should_Return_33()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(2, 22);
            int value;
            Assert.IsTrue(cache.TryGetValue(3, out value));
            Assert.AreEqual(33, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_2_To_22_Update_3_To_33_Get_Value_4_Should_Return_4()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(2, 22);
            int value;
            Assert.IsTrue(cache.TryGetValue(4, out value));
            Assert.AreEqual(4, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Get_Value_1_Should_Return_11()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            int value;
            Assert.IsTrue(cache.TryGetValue(1, out value));
            Assert.AreEqual(11, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Get_Value_2_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            int value;
            Assert.IsFalse(cache.TryGetValue(2, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Get_Value_3_Should_Return_33()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            int value;
            Assert.IsTrue(cache.TryGetValue(3, out value));
            Assert.AreEqual(33, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Get_Value_4_Should_Return_4()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            int value;
            Assert.IsTrue(cache.TryGetValue(4, out value));
            Assert.AreEqual(4, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_4_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            Assert.IsFalse(cache.TryGetValue(4, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_5_Should_Return_5()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            Assert.IsTrue(cache.TryGetValue(5, out value));
            Assert.AreEqual(5, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_1_Should_Return_11()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            Assert.IsTrue(cache.TryGetValue(1, out value));
            Assert.AreEqual(11, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_3_Should_Return_33()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            Assert.IsTrue(cache.TryGetValue(3, out value));
            Assert.AreEqual(33, value);
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_2_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            Assert.IsFalse(cache.TryGetValue(2, out value));
        }

        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_3_Add_6th_Get_1_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            cache.TryGetValue(3, out value);
            cache.AddOrUpdate(6, 6);
            
            Assert.IsFalse(cache.TryGetValue(1,out value));
        }

        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_3_Add_6th_Get_5_Should_Return_5()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            cache.TryGetValue(3, out value);
            cache.AddOrUpdate(6, 6);

            Assert.IsTrue(cache.TryGetValue(5, out value));
            Assert.AreEqual(5, value);
        }

        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_3_Add_6th_Get_5_Get_3_Update_1_To_11_Get_6_Should_Return_False()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            cache.TryGetValue(3, out value);
            cache.AddOrUpdate(6, 6);
            cache.TryGetValue(5, out value);
            cache.TryGetValue(3, out value);
            cache.AddOrUpdate(1, 11);
            Assert.IsFalse(cache.TryGetValue(6, out value));
        }
        [TestMethod]
        public void Size_3_Cache_Add_4_Update_3_To_33_Update_1_To_11_Add_5th_Get_Value_3_Add_6th_Get_5_Get_3_Update_1_To_11_Get_5_Should_Return_5()
        {
            MemoryCache<int, int> cache = new MemoryCache<int, int>(3);
            cache.AddOrUpdate(1, 1);
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, 3);
            cache.AddOrUpdate(4, 4);
            cache.AddOrUpdate(3, 33);
            cache.AddOrUpdate(1, 11);
            cache.AddOrUpdate(5, 5);
            int value;
            cache.TryGetValue(3, out value);
            cache.AddOrUpdate(6, 6);
            cache.TryGetValue(5, out value);
            cache.TryGetValue(3, out value);
            cache.AddOrUpdate(1, 11);
            Assert.IsTrue(cache.TryGetValue(5, out value));
            Assert.AreEqual(5, value);
        }
    }
}
