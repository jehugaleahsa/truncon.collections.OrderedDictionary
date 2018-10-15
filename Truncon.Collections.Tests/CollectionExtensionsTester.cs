using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Truncon.Collections.Extensions;

namespace Truncon.Collections.Tests
{
    [TestClass]
    public class CollectionExtensionsTester
    {
        #region ToOrderedDictionary

        /// <summary>
        /// An exception should be thrown if the collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_NullCollection_ThrowsException()
        {
            IEnumerable<string> collection = null;
            Func<string, int> keySelector = s => s.Length;
            Extensions.CollectionExtensions.ToOrderedDictionary(collection, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_NullKeySelector_ThrowsException()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = null;
            Extensions.CollectionExtensions.ToOrderedDictionary(collection, keySelector);
        }

        /// <summary>
        /// We should be able to build an OrderedDictionary from a collection by extracting keys
        /// from the items.
        /// </summary>
        [TestMethod]
        public void TestToOrderedDictionary_BuildsOrderedDictionary()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = s => s.Length;
            var dictionary = collection.ToOrderedDictionary(keySelector);
            Assert.AreEqual(4, dictionary.Count);
        }

        /// <summary>
        /// An exception should be thrown if the collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_WithKeyComparer_NullCollection_ThrowsException()
        {
            IEnumerable<string> collection = null;
            Func<string, int> keySelector = s => s.Length;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Extensions.CollectionExtensions.ToOrderedDictionary(collection, keySelector, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_WithComparer_NullKeySelector_ThrowsException()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Extensions.CollectionExtensions.ToOrderedDictionary(collection, keySelector, comparer);
        }

        /// <summary>
        /// We should be able to build an OrderedDictionary from a collection by extracting keys
        /// from the items.
        /// </summary>
        [TestMethod]
        public void TestToOrderedDictionary_WithComparer_BuildsOrderedDictionary()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = s => s.Length;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            var dictionary = collection.ToOrderedDictionary(keySelector, comparer);
            Assert.AreSame(comparer, dictionary.Comparer, "The comparer was not set.");
            var expected = new Dictionary<int, string>()
            {
                { 5, "Hello"},
                { 7, "Goodbye" },
                { 8, "Farewell" },
                { 9, "Greetings" },
            };
            assertDictionaryEqual(expected, dictionary, "The dictionary was generated as expected.");
        }

        private static void assertDictionaryEqual<TKey, TValue>(IDictionary<TKey, TValue> expected, IDictionary<TKey, TValue> actual, string message)
        {
            Assert.AreEqual(expected.Count, actual.Count, message);
            foreach (KeyValuePair<TKey, TValue> pair in expected)
            {
                Assert.IsTrue(actual.ContainsKey(pair.Key), message);
                Assert.AreEqual(expected[pair.Key], actual[pair.Key], message);
            }
        }

        #endregion
    }
}
