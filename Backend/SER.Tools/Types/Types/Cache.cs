using Microsoft.Extensions.Caching.Memory;

namespace SER.Tools.Types;
public class Cache<TItem> : IDisposable
{
	private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

	public Boolean ContainsKey(Object key) => _cache.TryGetValue(key, out TItem cach);

	public void CreateValue(Object key, TItem? value = default)
	{
		MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));
		_cache.Set(key, value, cacheEntryOptions);
	}

	public TItem? GetOrCreate(Object key, Func<TItem> createItem)
	{
		TItem cacheEntry;
		if (!_cache.TryGetValue(key, out cacheEntry)) // Ищем ключ в кэше.
		{
			// Ключ отсутствует в кэше, поэтому получаем данные.
			cacheEntry = createItem();

			MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));

			// Сохраняем данные в кэше.
			_cache.Set(key, cacheEntry, cacheEntryOptions);
		}
		return cacheEntry;
	}

	public void Dispose()
	{
		_cache?.Dispose();
	}
}
