Ordered Dictionary
=====================================

A generic version of the .NET OrderedDictionary.

Download using NuGet: [Truncon.Collections.OrderedDictionary](http://www.nuget.org/packages/Truncon.Collections/)

## Overview
.NET provides a non-generic `OrderedDictionary` class in the `System.Collections.Specialized` namespace. Unfortunately, a generic version has never been added to .NET (as of .NET 4.5).

`OrderedDictionary` should not be confused with a `SortedDictionary`. An `OrderedDictionary` is simply a dictionary that remembers the order that its items were added. This is useful for representing things like parameter lists. Parameters have names, but they also appear in a particular order.

The generic `OrderedDictionary` has various advantages over the non-generic version. First, it has better runtime because of less conversions and it actually uses less memory. Second, it implements the `IList<T>` interface which means you can treat it like a `List<KeyValuePair<TKey, TValue>>`. Being generic also makes it LINQ-friendly. Finally, it allows you to quickly retrieve the key at a particular index (which you can't do with the non-generic version for some, unknown reason).

The only slow operation on an `OrderedDictionary` is finding the index of a key, which requires a linear search. Just be wary of operations that involve finding or removing an item by its key.

## License
If you are looking for a license, you won't find one. The software in this project is free, as in "free as air". Feel free to use my software anyway you like. Use it to build up your evil war machine, swindle old people out of their social security or crush the souls of the innocent.

I love to hear how people are using my code, so drop me a line. Feel free to contribute any enhancements or documentation you may come up with, but don't feel obligated. I just hope this code makes someone's life just a little bit easier.
