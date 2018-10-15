using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Truncon.Collections.Tests
{
    /// <summary>
    /// Tests the OrderedDictionary class.
    /// </summary>
    [TestClass]
    public class OrderedDictionaryTester
    {
        #region Real World Examples

        /// <summary>
        ///  An OrderedDictionary remembers the order that items are added.
        /// </summary>
        [TestMethod]
        public void TestOrderedDictionary_ItemsReturnedInSameOrder()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            dictionary.Add("Bob", 100);
            dictionary.Add("Sally", 99);
            dictionary.Add("Mike", 98);
            dictionary.Add("Sam", 75);
            dictionary.Add("Carl", 72);

            Assert.AreEqual(5, dictionary.Count, "The wrong number of items were added.");

            Assert.AreEqual("Bob", dictionary.GetKey(0), "Did not find Bob at the first index.");
            Assert.AreEqual(100, dictionary[0], "Did not find Bob's value at the first index.");

            Assert.AreEqual("Sally", dictionary.GetKey(1), "Did not find Sally at the second index.");
            Assert.AreEqual(99, dictionary[1], "Did not find Sally's value at the second index.");

            Assert.AreEqual("Mike", dictionary.GetKey(2), "Did not find Mike at the third index.");
            Assert.AreEqual(98, dictionary[2], "Did not find MIke's value at the third index.");

            Assert.AreEqual("Sam", dictionary.GetKey(3), "Did not find Sam at the fourth index.");
            Assert.AreEqual(75, dictionary[3], "Did not find Sam's value at the fourth index.");

            Assert.AreEqual("Carl", dictionary.GetKey(4), "Did not find Bob at the fifth index.");
            Assert.AreEqual(72, dictionary[4], "Did not find Carl's value at the fifth index.");
        }

        #endregion

        #region Ctor

        /// <summary>
        /// If we create a new OrderedDictionary, it shouldn't have any items in it.
        /// </summary>
        [TestMethod]
        public void TestCtor_Default_CreatesEmptyDictionary()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            Assert.AreEqual(0, dictionary.Count, "The count should have been zero.");
            ICollection<KeyValuePair<int, int>> collection = dictionary;
            Assert.IsFalse(collection.IsReadOnly, "The dictionary should not have been read-only.");
        }

        /// <summary>
        /// We can create a new OrderedDictionary specifying the initial capacity.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithCapacity_CreatesEmptyDictionary()
        {
            int capacity = 100;
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>(capacity);
            Assert.AreEqual(0, dictionary.Count);
        }

        /// <summary>
        /// We can create a new OrderedDictionary specifying a negative capacity, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCapacity_NegativeCapacity_ThrowsException()
        {
            int capacity = -1;
            new OrderedDictionary<int, int>(capacity);
        }

        /// <summary>
        /// We can create a new OrderedDictionary specifying the key comparer to use.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithComparer_CreatesEmptyDictionary()
        {
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>(comparer);
            Assert.AreEqual(0, dictionary.Count, "The count was not zero.");
            Assert.AreSame(comparer, dictionary.Comparer, "The comparer was not set.");
        }

        /// <summary>
        /// We can create a new OrderedDictionary specifying the initial capacity and the key comparer to use.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithCapacityAndComparer_CreatesEmptyDictionary()
        {
            int capacity = 100;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>(capacity, comparer);
            Assert.AreEqual(0, dictionary.Count, "The count was not zero.");
            Assert.AreSame(comparer, dictionary.Comparer, "The comparer was not set.");
        }

        /// <summary>
        /// If we try to start with a negative capacity, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCapacityAndComparer_NegativeCapacity_ThrowsException()
        {
            int capacity = -1;
            IEqualityComparer<int> comparer = null;
            new OrderedDictionary<int, int>(capacity, comparer);
        }

        #endregion

        #region Add

        /// <summary>
        /// If we try to add a key that is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd_NullKey_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            string key = null;
            int value = 0;
            dictionary.Add(key, value);
        }

        /// <summary>
        /// If we try to add a key twice, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAdd_DuplicateKey_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            int key = 0;
            int value = 0;
            dictionary.Add(key, value);
        }

        /// <summary>
        /// If we try to add a key/value pair, it should be stored in the dictionary.
        /// </summary>
        [TestMethod]
        public void TestAdd_AssociatesKeyWithValue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            int key = 0;
            int value = 0;
            dictionary.Add(key, value);
            Assert.AreEqual(1, dictionary.Count, "The count was not increased.");
            Assert.IsTrue(dictionary.ContainsKey(key), "The key was not added to the dictionary.");
            Assert.AreEqual(value, dictionary[key], "The wrong value was associated with the key.");
        }

        /// <summary>
        /// An exception should be thrown if we try to add a null key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd_ICollection_NullKey_ThrowsException()
        {
            ICollection<KeyValuePair<string, int>> collection = new OrderedDictionary<string, int>();
            collection.Add(new KeyValuePair<string, int>(null, 0));
        }

        /// <summary>
        /// An exception should be thrown if we try to add a the same key twice.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAdd_ICollection_DuplicateKey_ThrowsException()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>() { { 0, 0 } };
            collection.Add(new KeyValuePair<int, int>(0, 0));
        }

        /// <summary>
        /// If we try to add a key/value pair, it should be stored in the dictionary.
        /// </summary>
        [TestMethod]
        public void TestAdd_ICollection_AssociatesKeyWithValue()
        {
            var dictionary = new OrderedDictionary<int, int>();
            ICollection<KeyValuePair<int, int>> collection = dictionary;
            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(0, 0);
            collection.Add(pair);
            Assert.AreEqual(1, dictionary.Count, "The count was not increased.");
            Assert.IsTrue(dictionary.ContainsKey(0), "The key was not added to the dictionary.");
            Assert.AreEqual(0, dictionary[0], "The wrong value was associated with the key.");
        }

        #endregion

        #region Insert

        /// <summary>
        /// An exception should be thrown if we try to insert at a negative index.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_NegativeIndex_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.Insert(-1, 0, 0);
        }

        /// <summary>
        /// An exception should be thrown if we try to insert at an index past the size of the dictionary.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_IndexTooBig_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.Insert(1, 0, 0);
        }

        /// <summary>
        /// An exception should be thrown if we try to insert a null key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsert_NullKey_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            dictionary.Insert(0, null, 0);
        }

        /// <summary>
        /// An exception should be thrown if we try to insert a duplicate key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInsert_DuplicateKey_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            dictionary.Insert(0, 0, 0);
        }

        /// <summary>
        /// If we insert into the front of the dictionary, it should be accessible by index.
        /// </summary>
        [TestMethod]
        public void TestInsert_InFront_InsertsInFront()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int,int>() { { 1, 1 }, { 2, 2 } };
            dictionary.Insert(0, 0, 0);
            Assert.AreEqual(3, dictionary.Count, "The count was not increased.");
            Assert.AreEqual(0, dictionary.GetKey(0), "The pair was not placed at the right index.");
            Assert.AreEqual(0, dictionary[0], "The value was not associated with the key.");
        }

        /// <summary>
        /// If we insert into the middle of the dictionary, it should be accessible by index.
        /// </summary>
        [TestMethod]
        public void TestInsert_InMiddle_InsertsInFront()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 }, { 2, 2 } };
            dictionary.Insert(1, 1, 1);
            Assert.AreEqual(3, dictionary.Count, "The count was not increased.");
            Assert.AreEqual(1, dictionary.GetKey(1), "The pair was not placed at the right index.");
            Assert.AreEqual(1, dictionary[1], "The value was not associated with the key.");
        }

        /// <summary>
        /// If we insert into the back of the dictionary, it should be accessible by index.
        /// </summary>
        [TestMethod]
        public void TestInsert_InBack_InsertsInFront()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 }, { 1, 1 } };
            dictionary.Insert(2, 2, 2);
            Assert.AreEqual(3, dictionary.Count, "The count was not increased.");
            Assert.AreEqual(2, dictionary.GetKey(2), "The pair was not placed at the right index.");
            Assert.AreEqual(2, dictionary[2], "The value was not associated with the key.");
        }

        /// <summary>
        /// An exception should be thrown if we try to insert at a negative index.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_IList_NegativeIndex_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>();
            list.Insert(-1, new KeyValuePair<int, int>(0, 0));
        }

        /// <summary>
        /// An exception should be thrown if we try to insert at an index past the size of the dictionary.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_IList_IndexTooBig_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>();
            list.Insert(1, new KeyValuePair<int, int>(0, 0));
        }

        /// <summary>
        /// An exception should be thrown if we try to insert a null key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsert_IList_NullKey_ThrowsException()
        {
            IList<KeyValuePair<string, int>> list = new OrderedDictionary<string, int>();
            list.Insert(0, new KeyValuePair<string, int>(null, 0));
        }

        /// <summary>
        /// An exception should be thrown if we try to insert a duplicate key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInsert_IList_DuplicateKey_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>() { { 0, 0 } };
            list.Insert(0, new KeyValuePair<int, int>(0, 0));
        }

        /// <summary>
        /// If we insert into the front of the dictionary, it should be accessible by index.
        /// </summary>
        [TestMethod]
        public void TestInsert_IList_InFront_InsertsInFront()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 1, 1 }, { 2, 2 } };
            IList<KeyValuePair<int, int>> list = dictionary;
            list.Insert(0, new KeyValuePair<int, int>(0, 0));
            Assert.AreEqual(3, dictionary.Count, "The count was not increased.");
            Assert.AreEqual(0, dictionary.GetKey(0), "The pair was not placed at the right index.");
            Assert.AreEqual(0, dictionary[0], "The value was not associated with the key.");
        }

        /// <summary>
        /// If we insert into the middle of the dictionary, it should be accessible by index.
        /// </summary>
        [TestMethod]
        public void TestInsert_IList_InMiddle_InsertsInFront()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 }, { 2, 2 } };
            IList<KeyValuePair<int, int>> list = dictionary;
            list.Insert(1, new KeyValuePair<int, int>(1, 1));
            Assert.AreEqual(3, dictionary.Count, "The count was not increased.");
            Assert.AreEqual(1, dictionary.GetKey(1), "The pair was not placed at the right index.");
            Assert.AreEqual(1, dictionary[1], "The value was not associated with the key.");
        }

        /// <summary>
        /// If we insert into the back of the dictionary, it should be accessible by index.
        /// </summary>
        [TestMethod]
        public void TestInsert_IList_InBack_InsertsInFront()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 }, { 1, 1 } };
            IList<KeyValuePair<int, int>> list = dictionary;
            list.Insert(2, new KeyValuePair<int, int>(2, 2));
            Assert.AreEqual(3, dictionary.Count, "The count was not increased.");
            Assert.AreEqual(2, dictionary.GetKey(2), "The pair was not placed at the right index.");
            Assert.AreEqual(2, dictionary[2], "The value was not associated with the key.");
        }

        #endregion

        #region ContainsKey

        /// <summary>
        /// If we pass null to ContainsKey, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsKey_NullKey_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string,int>();
            dictionary.ContainsKey(null);
        }

        /// <summary>
        /// If the dictionary contains a key, ContainsKey should return true.
        /// </summary>
        [TestMethod]
        public void TestContainsKey_HasKey_ReturnsTrue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.Add(0, 0);
            bool result = dictionary.ContainsKey(0);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// If the dictionary is missing the given key, ContainsKey should return false.
        /// </summary>
        [TestMethod]
        public void TestContainsKey_KeyMissing_ReturnsFalse()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            bool result = dictionary.ContainsKey(0);
            Assert.IsFalse(result);
        }

        #endregion

        #region GetKey

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetKey_NegativeIndex_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.GetKey(-1);
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetKey_IndexTooBig_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.GetKey(1);
        }

        /// <summary>
        /// The key at the given index should be returned.
        /// </summary>
        [TestMethod]
        public void TestGetKey_ReturnsKeyAtIndex()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.Add(0, 0);
            int key = dictionary.GetKey(0);
            Assert.AreEqual(0, key);
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// An exception should be thown if the key is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_NullKey_ThrowsException()
        {
            OrderedDictionary<string, string> dictionary = new OrderedDictionary<string, string>();
            dictionary.IndexOf(null);
        }

        /// <summary>
        /// If the key is not in the dictionary, -1 should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MissingKey_NegativeOneReturned()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            int index = dictionary.IndexOf(0);
            Assert.AreEqual(-1, index);
        }

        /// <summary>
        /// If the key is at the beginning of the dictionary, zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_InFront_ReturnsZero()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            int index = dictionary.IndexOf(0);
            Assert.AreEqual(0, index);
        }

        /// <summary>
        /// If the key is in the middle of the dictionary, the index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_InMiddle_ReturnsIndex()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            int index = dictionary.IndexOf(1);
            Assert.AreEqual(1, index);
        }

        /// <summary>
        /// If the key is at the end of the dictionary, the index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_InBack_ReturnsIndex()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            int index = dictionary.IndexOf(2);
            Assert.AreEqual(2, index);
        }

        /// <summary>
        /// An exception should be thown if the key is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_IList_NullKey_ThrowsException()
        {
            IList<KeyValuePair<string, string>> list = new OrderedDictionary<string, string>();
            list.IndexOf(new KeyValuePair<string, string>(null, null));
        }

        /// <summary>
        /// If the key is not in the dictionary, -1 should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_IList_MissingKey_NegativeOneReturned()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>();
            int index = list.IndexOf(new KeyValuePair<int, int>(0, 0));
            Assert.AreEqual(-1, index);
        }

        /// <summary>
        /// If the key is found but the value does not match, -1 is returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_IList_DifferentValue_ReturnsNegativeOne()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>() { { 0, 0 } };
            int index = list.IndexOf(new KeyValuePair<int, int>(0, 1));
            Assert.AreEqual(-1, index);
        }

        /// <summary>
        /// If the key/value pair is at the beginning of the dictionary, zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_IList_InFront_ReturnsZero()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            int index = list.IndexOf(new KeyValuePair<int, int>(0, 0));
            Assert.AreEqual(0, index);
        }

        /// <summary>
        /// If the key/value pair is in the middle of the dictionary, the index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_IList_InMiddle_ReturnsIndex()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            int index = list.IndexOf(new KeyValuePair<int, int>(1, 1));
            Assert.AreEqual(1, index);
        }

        /// <summary>
        /// If the key/value pair is at the end of the dictionary, the index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_IList_InBack_ReturnsIndex()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            int index = list.IndexOf(new KeyValuePair<int, int>(2, 2));
            Assert.AreEqual(2, index);
        }

        #endregion

        #region Remove

        /// <summary>
        /// An exception should be thrown if we try to remove a null key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemove_NullKey_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            dictionary.Remove(null);
        }

        /// <summary>
        /// False should be returned if the key is not in the dictionary.
        /// </summary>
        [TestMethod]
        public void TestRemove_NoSuchKey_ReturnsFalse()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            bool result = dictionary.Remove(0);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// If the key exists in the dictionary, the key/value pair should be removed
        /// and true should be returned.
        /// </summary>
        [TestMethod]
        public void TestRemove_KeyFound_ReturnsTrue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.Add(0, 0);
            bool result = dictionary.Remove(0);
            Assert.IsTrue(result, "True should have been returned.");
            Assert.AreEqual(0, dictionary.Count, "The count should have been decremented.");
        }

        /// <summary>
        /// When removing a key/value pair, the index of all subsequent pairs should be decremented.
        /// </summary>
        [TestMethod]
        public void TestRemove_IndexesShift()
        {
            var dictionary = new OrderedDictionary<string, int>() { { "zero", 0 }, { "one", 1 }, { "two", 2 } };
            bool result = dictionary.Remove("zero");
            Assert.IsTrue(result, "True should have been returned.");
            Assert.AreEqual(2, dictionary.Count, "The count should have been decremented.");
            Assert.AreEqual(1, dictionary[0], "One should now occupy the zeroth index.");
            Assert.AreEqual(1, dictionary["one"], "One should map to 1.");
            Assert.AreEqual(2, dictionary[1], "Two should now occupy the first index.");
            Assert.AreEqual(2, dictionary["two"], "Two should map to 2.");
        }

        /// <summary>
        /// An exception should be thrown if we try to remove a null key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemove_ICollection_NullKey_ThrowsException()
        {
            ICollection<KeyValuePair<string, int>> collection = new OrderedDictionary<string, int>();
            collection.Remove(new KeyValuePair<string, int>(null, 0));
        }

        /// <summary>
        /// False should be returned if the key is not in the dictionary.
        /// </summary>
        [TestMethod]
        public void TestRemove_ICollection_NoSuchKey_ReturnsFalse()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>();
            bool result = collection.Remove(new KeyValuePair<int, int>(0, 0));
            Assert.IsFalse(result);
        }

        /// <summary>
        /// If the key exists in the dictionary but the value is different, false should return.
        /// </summary>
        [TestMethod]
        public void TestRemove_ICollection_KeyFound_ValueDifferent_ReturnsFalse()
        {
            ICollection<KeyValuePair<int, int>> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            bool result = dictionary.Remove(new KeyValuePair<int, int>(0, 1));
            Assert.IsFalse(result, "False should have been returned.");
            Assert.AreEqual(1, dictionary.Count, "The count should not have been decremented.");
        }

        /// <summary>
        /// If the key/value pairs exists in the dictionary, it should be removed
        /// and true should be returned.
        /// </summary>
        [TestMethod]
        public void TestRemove_ICollection_PairFound_ReturnsTrue()
        {
            ICollection<KeyValuePair<int, int>> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            bool result = dictionary.Remove(new KeyValuePair<int, int>(0, 0));
            Assert.IsTrue(result, "True should have been returned.");
            Assert.AreEqual(0, dictionary.Count, "The count should have been decremented.");
        }

        #endregion

        #region RemoveAt

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemoveAt_NegativeIndex_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.RemoveAt(-1);
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemoveAt_IndexTooBig_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary.RemoveAt(1);
        }

        /// <summary>
        /// The key/value pair at the given index should be removed.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_InFront_RemovesItem()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            dictionary.RemoveAt(0);
            Assert.AreEqual(2, dictionary.Count, "The count was not decremented.");
            Dictionary<int, int> expected = new Dictionary<int,int>()
            {
                { 1, 1 },
                { 2, 2 },
            };
            assertDictionaryEqual(expected, dictionary, "The item was not removed as expected.");
        }

        /// <summary>
        /// The key/value pair at the given index should be removed.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_InMiddle_RemovesItem()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            dictionary.RemoveAt(1);
            Assert.AreEqual(2, dictionary.Count, "The count was not decremented.");
            Dictionary<int, int> expected = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 2, 2 },
            };
            assertDictionaryEqual(expected, dictionary, "The item was not removed as expected.");
        }

        /// <summary>
        /// The key/value pair at the given index should be removed.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_InBack_RemovesItem()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            dictionary.RemoveAt(2);
            Assert.AreEqual(2, dictionary.Count, "The count was not decremented.");
            Dictionary<int, int> expected = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
            };
            assertDictionaryEqual(expected, dictionary, "The item was not removed as expected.");
        }

        #endregion

        #region TryGetValue

        /// <summary>
        /// An exception should be thrown if the key is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTryGetValue_NullKey_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            int value;
            dictionary.TryGetValue(null, out value);
        }

        /// <summary>
        /// If the key is in the dictionary, the value should be set and true should be returned.
        /// </summary>
        [TestMethod]
        public void TestTryGetValue_KeyFound_ValueSet_ReturnsTrue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 1 } };
            int value;
            bool result = dictionary.TryGetValue(0, out value);
            Assert.AreEqual(1, value, "The value was not stored.");
            Assert.IsTrue(result, "True was not returned.");
        }

        /// <summary>
        /// If the key is not in the dictionary, the value should be set to the default and false should be returned.
        /// </summary>
        [TestMethod]
        public void TestTryGetValue_KeyNotFound_DefaultValueSet_ReturnsFalse()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 1, 1 } };
            int value;
            bool result = dictionary.TryGetValue(0, out value);
            Assert.AreEqual(default(int), value, "The value was not stored.");
            Assert.IsFalse(result, "True was not returned.");
        }

        #endregion

        #region Indexer

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_NegativeIndex_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            int value = dictionary[-1];
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_IndexTooBig_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            int value = dictionary[1];
        }

        /// <summary>
        /// The value at the given index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_GetsValueAtIndex()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 123 },
            };
            int value = dictionary[0];
            Assert.AreEqual(123, value);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_NegativeIndex_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary[-1] = 0;
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_IndexTooBig_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            dictionary[1] = 0;
        }

        /// <summary>
        /// The value should be set at the given index.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_SetsValueAtIndex()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
            };
            dictionary[0] = 123;
            Assert.AreEqual(123, dictionary[0]);
        }

        /// <summary>
        /// An exception should be thrown if the key is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexer_Getter_NullKey_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            int value = dictionary[null];
        }

        /// <summary>
        /// An exception should be thrown if the key is not in the dictionary.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestIndexer_Getter_KeyNotFound_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            int value = dictionary["Bad"];
        }

        /// <summary>
        /// The value associated with the given key should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_KeyFound_ReturnsValue()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "A", 0 },
                { "B", 1 },
            };
            int value = dictionary["A"];
            Assert.AreEqual(0, value);
        }

        /// <summary>
        /// An exception should be thrown if the key is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexer_Setter_NullKey_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            dictionary[null] = 0;
        }

        /// <summary>
        /// The item should be added to the dictionary if it isn't already present.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_KeyNotFound_PairAdded()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>();
            dictionary["Key"] = 0;
            Assert.IsTrue(dictionary.ContainsKey("Key"), "The key was not added to the dictionary.");
            Assert.AreEqual(0, dictionary["Key"], "The value was not associated with the key.");
        }

        /// <summary>
        /// The value associated with the given key should be set.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_KeyFound_SetsValue()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "A", 0 },
            };
            dictionary["A"] = 2;
            Assert.AreEqual(2, dictionary["A"]);
        }

        /// <summary>
        /// An exception should be thrown if we try to get a key/value at a negative index.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_IList_Getter_NegativeIndex_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>();
            KeyValuePair<int, int> pair = list[-1];
        }

        /// <summary>
        /// An exception should be thrown if we try to get a key/value at a index past the end of the dictionary.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_IList_Getter_IndexTooBig_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>();
            KeyValuePair<int, int> pair = list[1];
        }

        /// <summary>
        /// The key/value pair at the given index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexer_IList_Getter_ReturnsPair()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
            };
            KeyValuePair<int, int> pair = list[0];
            Assert.AreEqual(new KeyValuePair<int, int>(0, 0), pair);
        }

        /// <summary>
        /// An exception should be thrown if we try to set a pair at a negative index.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_IList_Setter_NegativeIndex_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>();
            list[-1] = new KeyValuePair<int, int>(0, 0);
        }

        /// <summary>
        /// An exception should be thrown if we try to set a pair past the end of the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_IList_Setter_IndexTooBig_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>();
            list[0] = new KeyValuePair<int, int>(0, 0);
        }

        /// <summary>
        /// An exception should be thrown if we try to set a null key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexer_IList_Setter_NullKey_ThrowsException()
        {
            IList<KeyValuePair<string, int>> list = new OrderedDictionary<string, int>() { { "A", 0 } };
            list[0] = new KeyValuePair<string, int>(null, 0);
        }

        /// <summary>
        /// If we are trying to set a key that is already in the dictionary, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIndexer_IList_Setter_DuplicateKey_ThrowsException()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>() { { 0, 0 }, { 1, 1 } };
            list[0] = new KeyValuePair<int, int>(1, 0);
        }

        /// <summary>
        /// If the pair at the given index has the same key, then we just replace the value.
        /// </summary>
        [TestMethod]
        public void TestIndexer_IList_Setter_SameKey_ReplacesValue()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>() { { 0, 0 } };
            list[0] = new KeyValuePair<int, int>(0, 1);
            Assert.AreEqual(new KeyValuePair<int, int>(0, 1), list[0]);
        }

        /// <summary>
        /// If there is a different key at the index, the key/value pair are replaced.
        /// </summary>
        [TestMethod]
        public void TestIndexer_IList_Setter_DifferentKey_ReplacesPair()
        {
            IList<KeyValuePair<int, int>> list = new OrderedDictionary<int, int>() { { 0, 0 } };
            list[0] = new KeyValuePair<int, int>(1, 1);
            Assert.AreEqual(new KeyValuePair<int, int>(1, 1), list[0]);
        }

        #endregion

        #region Clear

        /// <summary>
        /// Should remove all of the items from the dictionary.
        /// </summary>
        [TestMethod]
        public void TestClear_RemovesAllItems()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// The enumerator should access the key/value pairs in the order they were added.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_GetsItemsInOrder()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "Hello", 0 },
                { "Goodbye", 1 },
                { "Farewell", 2 },
            };
            IEnumerator<KeyValuePair<string, int>> enumerator = dictionary.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext(), "The first item was not enumerated.");
            Assert.AreEqual(new KeyValuePair<string, int>("Hello", 0), enumerator.Current, "The first value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The second item was not enumerated.");
            Assert.AreEqual(new KeyValuePair<string, int>("Goodbye", 1), enumerator.Current, "The second value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The third item was not enumerated.");
            Assert.AreEqual(new KeyValuePair<string, int>("Farewell", 2), enumerator.Current, "The third value is wrong.");
            Assert.IsFalse(enumerator.MoveNext(), "No more items should have been enumerated.");
        }

        /// <summary>
        /// The enumerator should access the key/value pairs in the order they were added.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_IEnumerable_GetsItemsInOrder()
        {
            IEnumerable collection = new OrderedDictionary<string, int>()
            {
                { "Hello", 0 },
                { "Goodbye", 1 },
                { "Farewell", 2 },
            };
            IEnumerator enumerator = collection.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext(), "The first item was not enumerated.");
            Assert.AreEqual(new KeyValuePair<string, int>("Hello", 0), enumerator.Current, "The first value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The second item was not enumerated.");
            Assert.AreEqual(new KeyValuePair<string, int>("Goodbye", 1), enumerator.Current, "The second value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The third item was not enumerated.");
            Assert.AreEqual(new KeyValuePair<string, int>("Farewell", 2), enumerator.Current, "The third value is wrong.");
            Assert.IsFalse(enumerator.MoveNext(), "No more items should have been enumerated.");
        }

        /// <summary>
        /// The enumerator should access the key/value pairs in the order they were added.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetEnumerator_ModifyCollection_ThrowsException()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "Hello", 0 },
                { "Goodbye", 1 },
                { "Farewell", 2 },
            };
            foreach (var pair in dictionary)
            {
                dictionary.Add("Later", 3);
            }
        }

        #endregion

        #region Keys

        /// <summary>
        /// A KeyCollection should be returned if going through the IDictionary class.
        /// </summary>
        [TestMethod]
        public void TestKeys_IDictionary_ReturnsKeyCollection()
        {
            IDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> keys = dictionary.Keys;
            Assert.IsInstanceOfType(keys, typeof(OrderedDictionary<int, int>.KeyCollection));
        }

        #endregion

        #region Values

        /// <summary>
        /// A ValueCollection should be returned if going through the IDictionary class.
        /// </summary>
        [TestMethod]
        public void TestValues_IDictionary_ReturnsValueCollection()
        {
            IDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> values = dictionary.Values;
            Assert.IsInstanceOfType(values, typeof(OrderedDictionary<int, int>.ValueCollection));
        }

        #endregion

        #region Contains

        /// <summary>
        /// An exception should be thrown if we search for a null key.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_NullKey_ThrowsException()
        {
            ICollection<KeyValuePair<string, int>> collection = new OrderedDictionary<string, int>();
            collection.Contains(new KeyValuePair<string, int>(null, 0));
        }

        /// <summary>
        /// If the key is not found, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestContains_KeyNotFound_ReturnsFalse()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>() { { 0, 0 } };
            bool result = collection.Contains(new KeyValuePair<int, int>(1, 1));
            Assert.IsFalse(result);
        }

        /// <summary>
        /// If the key is found but the value is different, then false should be returned.
        /// </summary>
        [TestMethod]
        public void TestContains_KeyFound_ValueDifferent_ReturnsFalse()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>() { { 0, 0 } };
            bool result = collection.Contains(new KeyValuePair<int, int>(0, 1));
            Assert.IsFalse(result);
        }

        /// <summary>
        /// If the key is found and the value is the same, then true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContains_KeyFound_ValueSame_ReturnsTrue()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>() { { 0, 0 } };
            bool result = collection.Contains(new KeyValuePair<int, int>(0, 0));
            Assert.IsTrue(result);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// An exception should be thrown if the array is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyTo_NullArray_ThrowsException()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>();
            collection.CopyTo(null, 0);
        }

        /// <summary>
        /// An exception should be thrown if the array index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCopyTo_NegativeArrayIndex_ThrowsException()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>();
            KeyValuePair<int, int>[] array = new KeyValuePair<int,int>[0];
            collection.CopyTo(array, -1);
        }

        /// <summary>
        /// If the array after the given array index is too small to hold the key/value pairs,
        /// an exception should be thrown.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_RemainingArrayTooSmall_NothingWritten()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>() { { 0, 0 } };
            KeyValuePair<int, int>[] array = new KeyValuePair<int,int>[0];
            collection.CopyTo(array, 0);
        }

        /// <summary>
        /// If the array is large enough to hold the key/value pairs, then they should be stored in the array.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_ArrayLargeEnough_StoresPairs()
        {
            ICollection<KeyValuePair<int, int>> collection = new OrderedDictionary<int, int>() { { 1, 1 } };
            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[1];
            collection.CopyTo(array, 0);
            Assert.AreEqual(new KeyValuePair<int, int>(1, 1), array[0]);
        }

        #endregion

        #region KeyCollection

        #region Contains

        /// <summary>
        /// False should be returned if we search for a missing key.
        /// </summary>
        [TestMethod]
        public void TestKeyCollection_Contains_MissingKey_ReturnsFalse()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> keys = dictionary.Keys;
            bool result = keys.Contains(0);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// True should be returned if we search for a key that exists.
        /// </summary>
        [TestMethod]
        public void TestKeyCollection_Contains_KeyExists_ReturnsTrue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            ICollection<int> keys = dictionary.Keys;
            bool result = keys.Contains(0);
            Assert.IsTrue(result);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// An exception should be thrown if the array is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestKeyCollection_CopyTo_NullArray_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            var keys = dictionary.Keys;
            keys.CopyTo(null, 0);
        }

        /// <summary>
        /// An exception should be thrown if the array index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestKeyCollection_CopyTo_NegativeArrayIndex_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            var keys = dictionary.Keys;
            int[] array = new int[0];
            keys.CopyTo(array, -1);
        }

        /// <summary>
        /// If the array after the given array index is too small to hold the key/value pairs,
        /// an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKeyCollection_CopyTo_RemainingArrayTooSmall_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            var keys = dictionary.Keys;
            int[] array = new int[0];
            keys.CopyTo(array, 0);
        }

        /// <summary>
        /// If the array is large enough to hold the key/value pairs, then they should be stored in the array.
        /// </summary>
        [TestMethod]
        public void TestKeyCollection_CopyTo_ArrayLargeEnough_StoresPairs()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 1, 1 } };
            var keys = dictionary.Keys;
            int[] array = new int[1];
            keys.CopyTo(array, 0);
            Assert.AreEqual(1, array[0]);
        }

        #endregion

        #region Count

        /// <summary>
        /// The key collection should have the same size as a the dictionary.
        /// </summary>
        [TestMethod]
        public void TestKeyCollection_Count_EqualsDictionarys()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            var keys = dictionary.Keys;
            Assert.AreEqual(dictionary.Count, keys.Count);
        }

        #endregion

        #region GetEnumerable

        /// <summary>
        /// The items in the keys collection should be iterated over in order.
        /// </summary>
        [TestMethod]
        public void TestKeyCollection_GetEnumerable_GetsKeysInOrder()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "Hello", 0 },
                { "Goodbye", 1 },
                { "Farewell", 2 },
            };
            var keys = dictionary.Keys;
            IEnumerator<string> enumerator = keys.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext(), "The first item was not enumerated.");
            Assert.AreEqual("Hello", enumerator.Current, "The first value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The second item was not enumerated.");
            Assert.AreEqual("Goodbye", enumerator.Current, "The second value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The third item was not enumerated.");
            Assert.AreEqual("Farewell", enumerator.Current, "The third value is wrong.");
            Assert.IsFalse(enumerator.MoveNext(), "No more items should have been enumerated.");
        }

        /// <summary>
        /// The enumerator should access the key/value pairs in the order they were added.
        /// </summary>
        [TestMethod]
        public void TestKeyCollection_GetEnumerator_IEnumerable_GetsItemsInOrder()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "Hello", 0 },
                { "Goodbye", 1 },
                { "Farewell", 2 },
            };
            IEnumerable keys = dictionary.Keys;
            IEnumerator enumerator = keys.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext(), "The first item was not enumerated.");
            Assert.AreEqual("Hello", enumerator.Current, "The first value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The second item was not enumerated.");
            Assert.AreEqual("Goodbye", enumerator.Current, "The second value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The third item was not enumerated.");
            Assert.AreEqual("Farewell", enumerator.Current, "The third value is wrong.");
            Assert.IsFalse(enumerator.MoveNext(), "No more items should have been enumerated.");
        }

        #endregion

        #region Add

        /// <summary>
        /// We cannot add to a key collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestKeyCollection_Add_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> keys = dictionary.Keys;
            keys.Add(0);
        }

        #endregion

        #region Clear

        /// <summary>
        /// We cannot clear a key collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestKeyCollection_Clear_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> keys = dictionary.Keys;
            keys.Clear();
        }

        #endregion

        #region IsReadOnly

        /// <summary>
        /// A KeyCollection is read-only.
        /// </summary>
        [TestMethod]
        public void TestKeyCollection_IsReadOnly_ReturnsTrue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> keys = dictionary.Keys;
            Assert.IsTrue(keys.IsReadOnly);
        }

        #endregion

        #region Remove

        /// <summary>
        /// We cannot remove from a key collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestKeyCollection_Remove_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> keys = dictionary.Keys;
            keys.Remove(0);
        }

        #endregion

        #endregion

        #region ValueCollection

        #region Contains

        /// <summary>
        /// False should be returned if we search for a missing value.
        /// </summary>
        [TestMethod]
        public void TestValueCollection_Contains_MissingValue_ReturnsFalse()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> values = dictionary.Values;
            bool result = values.Contains(0);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// True should be returned if we search for a value that exists.
        /// </summary>
        [TestMethod]
        public void TestValueCollection_Contains_ValueExists_ReturnsTrue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            ICollection<int> values = dictionary.Values;
            bool result = values.Contains(0);
            Assert.IsTrue(result);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// An exception should be thrown if the array is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestValueCollection_CopyTo_NullArray_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            var values = dictionary.Values;
            values.CopyTo(null, 0);
        }

        /// <summary>
        /// An exception should be thrown if the array index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestValueCollection_CopyTo_NegativeArrayIndex_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            var values = dictionary.Values;
            int[] array = new int[0];
            values.CopyTo(array, -1);
        }

        /// <summary>
        /// If the array after the given array index is too small to hold the values,
        /// an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestValueCollection_CopyTo_RemainingArrayTooSmall_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 0, 0 } };
            var values = dictionary.Values;
            int[] array = new int[0];
            values.CopyTo(array, 0);
        }

        /// <summary>
        /// If the array is large enough to hold the values, then they should be stored in the array.
        /// </summary>
        [TestMethod]
        public void TestValueCollection_CopyTo_ArrayLargeEnough_StoresPairs()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>() { { 1, 1 } };
            var values = dictionary.Values;
            int[] array = new int[1];
            values.CopyTo(array, 0);
            Assert.AreEqual(1, array[0]);
        }

        #endregion

        #region Count

        /// <summary>
        /// The value collection should have the same size as a the dictionary.
        /// </summary>
        [TestMethod]
        public void TestValueCollection_Count_EqualsDictionarys()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>()
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
            };
            var values = dictionary.Values;
            Assert.AreEqual(dictionary.Count, values.Count);
        }

        #endregion

        #region GetEnumerable

        /// <summary>
        /// The items in the values collection should be iterated over in order.
        /// </summary>
        [TestMethod]
        public void TestValueCollection_GetEnumerable_GetsValuesInOrder()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "Hello", 0 },
                { "Goodbye", 1 },
                { "Farewell", 2 },
            };
            var values = dictionary.Values;
            IEnumerator<int> enumerator = values.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext(), "The first item was not enumerated.");
            Assert.AreEqual(0, enumerator.Current, "The first value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The second item was not enumerated.");
            Assert.AreEqual(1, enumerator.Current, "The second value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The third item was not enumerated.");
            Assert.AreEqual(2, enumerator.Current, "The third value is wrong.");
            Assert.IsFalse(enumerator.MoveNext(), "No more items should have been enumerated.");
        }

        /// <summary>
        /// The enumerator should access the key/value pairs in the order they were added.
        /// </summary>
        [TestMethod]
        public void TestValueCollection_GetEnumerator_IEnumerable_GetsItemsInOrder()
        {
            OrderedDictionary<string, int> dictionary = new OrderedDictionary<string, int>()
            {
                { "Hello", 0 },
                { "Goodbye", 1 },
                { "Farewell", 2 },
            };
            IEnumerable values = dictionary.Values;
            IEnumerator enumerator = values.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext(), "The first item was not enumerated.");
            Assert.AreEqual(0, enumerator.Current, "The first value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The second item was not enumerated.");
            Assert.AreEqual(1, enumerator.Current, "The second value is wrong.");
            Assert.IsTrue(enumerator.MoveNext(), "The third item was not enumerated.");
            Assert.AreEqual(2, enumerator.Current, "The third value is wrong.");
            Assert.IsFalse(enumerator.MoveNext(), "No more items should have been enumerated.");
        }

        #endregion

        #region Add

        /// <summary>
        /// We cannot add to a value collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestValueCollection_Add_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> values = dictionary.Values;
            values.Add(0);
        }

        #endregion

        #region Clear

        /// <summary>
        /// We cannot clear a value collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestValueCollection_Clear_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> values = dictionary.Values;
            values.Clear();
        }

        #endregion

        #region IsReadOnly

        /// <summary>
        /// A ValueCollection is read-only.
        /// </summary>
        [TestMethod]
        public void TestValueCollection_IsReadOnly_ReturnsTrue()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> values = dictionary.Values;
            Assert.IsTrue(values.IsReadOnly);
        }

        #endregion

        #region Remove

        /// <summary>
        /// We cannot remove from a value collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestValueCollection_Remove_ThrowsException()
        {
            OrderedDictionary<int, int> dictionary = new OrderedDictionary<int, int>();
            ICollection<int> values = dictionary.Values;
            values.Remove(0);
        }

        #endregion

        #endregion

        private static void assertDictionaryEqual<TKey, TValue>(IDictionary<TKey, TValue> expected, IDictionary<TKey, TValue> actual, string message)
        {
            Assert.AreEqual(expected.Count, actual.Count, message);
            foreach (KeyValuePair<TKey, TValue> pair in expected)
            {
                Assert.IsTrue(actual.ContainsKey(pair.Key), message);
                Assert.AreEqual(expected[pair.Key], actual[pair.Key], message);
            }
        }
    }
}
