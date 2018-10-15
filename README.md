Ordered Dictionary
=====================================

A generic version of the .NET `OrderedDictionary`.

Download using NuGet: [Truncon.Collections.OrderedDictionary](http://www.nuget.org/packages/Truncon.Collections/)

## Overview
.NET does not provide a type-safe version of the `OrderedDictionary` class in the `System.Collections.Specialized` namespace. Unfortunately, a generic version still has not been added as of .NET 4.7. Although, there has been an [open vote](https://visualstudio.uservoice.com/forums/121579-visual-studio-2015/suggestions/16494583-generic-ordereddictionary) to add it. Part of the challenge is that there are various ways to implement an ordered dictionary with varying performance characteristics depending on which operations you execute the most. The goal of this library to to provide an implementation that will suit the more common use cases using. This implementation relies on existing .NET collections rather than trying to build a custom data structure. 

`OrderedDictionary` should not be confused with a `SortedDictionary`. An `OrderedDictionary` is simply a dictionary that remembers the order that its items were added. This is useful for representing things like function parameter lists or columns in a database query result. In these examples, parameters have names but they also appear in a particular order.

A generic `OrderedDictionary` has various advantages over the non-generic version. First, it has better runtime because of less conversions and it actually uses less memory. Second, it implements the `IList<T>` interface which means you can treat it like a `List<KeyValuePair<TKey, TValue>>`. Being generic also makes it LINQ-friendly. Finally, this implementation also allows you to quickly retrieve the key at a particular index.

## Performance Guidelines
Most of the operations on `OrderedDictionary` run in constant time (amortized). Here I list those which have a different performance characteristic:

*  **Insert** - Inserting into the front or middle of the collection will cause all of the subsequent key/value pairs to be shifted. Inserting onto the end runs in amortized constant time.
* **Remove** - Calls RemoveAt (see below).
* **RemoveAt** - Removing from the front or the middle of the collection will cause all the subsequent key/values pairs to be shifted. Removing from the end runs in constant time.
* **GetEnumerator** - Iterates over the key/value pairs linearly.
* **CopyTo** - Copies the key/value pairs linearly.

Guaranteeing constant time performance comes at the slight cost of additional memory. Each key will be allocated twice, along with it's index and the associated value. This should be minimal for most use-cases.

## IList interface
The `OrderedDictionary` class implements the `IList<KeyValuePair<TKey, TValue>>` interface, allowing it be treated like an ordered list, such as `List<T>`. One odd consequence of this interface is that the indexer (`dict[0] = new KeyValuePair<string, int>("Hello", 123)`) allows you to swap out the key/value pair appearing at a given index. If you try to change the key to a value already existing in the dictionary an exception will be thrown. Of course, it is perfectly okay to specify the current key and simply change the value.

## OrderedDictionary<int, int>
One special case to be aware of is `OrderedDictionary<int, int>`. In this case, both the key and position indexers will have this same signature:

```csharp
public TValue this[int index] { /*...*/ }
```

Due to language rules, C# will prefer the position indexer over the key indexer. If you must get the value by key, call `IndexOf` first:

```csharp
var value = dict[dict.IndexOf(0)];  // 0 is the key, not the index here
```

This is effectively what the keyed indexer does anyway.

## License
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
