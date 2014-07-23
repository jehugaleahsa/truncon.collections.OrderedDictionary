﻿using System;
using System.Collections.Generic;

namespace Truncon.Collections.Extensions
{
    public static class CollectionExtensions
    {
        #region ToOrderedDictionary

        /// <summary>
        /// Creates a new OrderedDictionary from the given collection, using the key selector to extract the key.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the collection.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The items to created the OrderedDictionary from.</param>
        /// <param name="keySelector">A delegate that can extract a key from an item in the collection.</param>
        /// <returns>An OrderedDictionary mapping the extracted keys to their values.</returns>
        public static OrderedDictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return toOrderedDictionary(source, keySelector, null);
        }

        /// <summary>
        /// Creates a new OrderedDictionary from the given collection, using the key selector to extract the key.
        /// The key comparer is passed to the OrderedDictionary for comparing the extracted keys.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the collection.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The items to created the OrderedDictionary from.</param>
        /// <param name="keySelector">A delegate that can extract a key from an item in the collection.</param>
        /// <param name="comparer">The key equality comparer to use to compare keys in the dictionary.</param>
        /// <returns>An OrderedDictionary mapping the extracted keys to their values.</returns>
        public static OrderedDictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return toOrderedDictionary(source, keySelector, comparer);
        }

        private static OrderedDictionary<TKey, TSource> toOrderedDictionary<TSource, TKey>(
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            OrderedDictionary<TKey, TSource> dictionary = new OrderedDictionary<TKey, TSource>(comparer);
            foreach (TSource item in source)
            {
                TKey key = keySelector(item);
                dictionary.Add(key, item);
            }
            return dictionary;
        }

        #endregion
    }
}
