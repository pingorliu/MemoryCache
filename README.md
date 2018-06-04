# MemoryCache

## Description
MemoryCache is a fast fixed-size thread-safe key-value in memory cache based on Eviction Policy. It supports both primitive type or user defined type as the key.
## Code Coverage
Overall code coverage percentage: **99.70%**
## How To Use
### Primitive Type(i.e. int, string, decimal) as Key
#### Define the memory cache and  its size

> MemoryCache<int, int> cache = new MemoryCache<int, int>(3);

#### Add or update record in cache

> cache.AddOrUpdate(1, 1);

#### Get record from cache

> int value;                                 
> bool result = cache.TryGetValue(1, out value);// result is true if the key existed in the cache

### Use User Defined Type as Key
To use a User Define Type as the key, a Comparer needs to be implemented and passed to the constructor of the MemoryCache.
For example:
User-Defined key

>     public class Key
>     {
>         public int x { get; set; }
>         public int y { get; set; }
>     }

The Comaparer needs to inherit from IEqualityComparer and implement Equals and GetHashCode functions

>     public class KeyComparer : IEqualityComparer<Key>
>     {
>         public bool Equals(Key l, Key r)
>         {
>             return (l.x == r.x && l.y == r.y);
>         }
> 
>         public int GetHashCode(Key obj)
>         {
>             return obj.x ^ obj.y;
>         }
>     }

#### Define the memory cache and its size

> MemoryCache<Key, Value> cache = new MemoryCache<Key, Value>(3, new KeyComparer());

#### Add or update record in cache

> cache.AddOrUpdate(new Key { x = 1, y = 1 }, new Value { a = 1, b = 1});

#### Get record from cache

> Value value;                                         
> bool result = cache.TryGetValue(new Key { x = 1, y = 1 },out value);