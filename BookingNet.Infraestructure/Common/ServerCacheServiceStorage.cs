using BookingNet.Application.Common.ServerCache;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;
using System.Text;

namespace BookingNet.Infraestructure.Common
{
    public class ServerCacheServiceStorage<TEntity> : IServerCacheServiceStorage<TEntity>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IHttpContextAccessor _contextAccessor;
        private byte[] _cachedEntities;
        private string _serializedData;
        private string _deserializedData;
        private string _key;

        public ServerCacheServiceStorage(IDistributedCache distributedCache, IHttpContextAccessor contextAccessor)
        {
            _distributedCache = distributedCache;
            _contextAccessor = contextAccessor;
            _key = $"[{_contextAccessor.HttpContext.Request.Method}]_" + 
                new Uri($"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}" +
                $"{_contextAccessor.HttpContext.Request.Path}{_contextAccessor.HttpContext.Request.QueryString}").ToString();
        }

        public async Task<TEntity> GetDataFromCacheAsync()
        {
            return await GetSerializedData(_key) != null ? JsonConvert.DeserializeObject<TEntity>(_deserializedData) : default(TEntity);
        }

        public async Task<IEnumerable<TEntity>> GetDataListFromCacheAsync()
        {
            return await GetSerializedData(_key) != null ? JsonConvert.DeserializeObject<IEnumerable<TEntity>>(_deserializedData).ToList() : default(IEnumerable<TEntity>); 
        }

        public async Task SetDataToCacheAsync(TEntity data)
        {
            try 
            { 
                await _distributedCache.SetAsync(_key, await SetSerializedData(_key, data), DistributedCacheEntryOptions());
            }
            catch (Exception ex) 
            {
                return;
            }
        }

        public async Task SetDataListToCacheAsync(IEnumerable<TEntity> data)
        {
            try
            {
                await _distributedCache.SetAsync(_key, await SetListSerializedData(_key, data), DistributedCacheEntryOptions());
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task RemoveDataFromCacheAsync(string key) 
        { 
            await _distributedCache.RemoveAsync(key);
        }

        private async Task<string> GetSerializedData(string key) 
        {
            try
            { 
                _cachedEntities = await _distributedCache.GetAsync(key);
                _deserializedData = _cachedEntities != null ? Encoding.UTF8.GetString(_cachedEntities) : null;

                return _deserializedData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<byte[]> SetSerializedData(string key, TEntity data) 
        {
            _serializedData = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(_serializedData);
        }

        private async Task<byte[]> SetListSerializedData(string key, IEnumerable<TEntity> data)
        {
            _serializedData = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(_serializedData);
        }

        private DistributedCacheEntryOptions DistributedCacheEntryOptions() 
        { 
            return new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));
        }
    }
}