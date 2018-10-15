Ordered Dictionary
=====================================

A generic version of the .NET OrderedDictionary.

Download using NuGet: [Truncon.Collections.OrderedDictionary](http://www.nuget.org/packages/Truncon.Collections/)

## Overview
.NET provides a type-safe version of the `OrderedDictionary` class in the `System.Collections.Specialized` namespace. Unfortunately, a generic version has never been added to .NET (as of .NET 4.7).

`OrderedDictionary` should not be confused with a `SortedDictionary`. An `OrderedDictionary` is simply a dictionary that remembers the order that its items were added. This is useful for representing things like parameter lists. Parameters have names, but they also appear in a particular order.

The generic `OrderedDictionary` has various advantages over the non-generic version. First, it has better runtime because of less conversions and it actually uses less memory. Second, it implements the `IList<T>` interface which means you can treat it like a `List<KeyValuePair<TKey, TValue>>`. Being generic also makes it LINQ-friendly. Finally, it allows you to quickly retrieve the key at a particular index (which you can't do with the non-generic version for some, unknown reason).

The only slow operations on an `OrderedDictionary` involve inserting or removing items at a particular index (`Insert` and `RemoveAt`), as these must shift the indexes of any subsequently added items.

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
