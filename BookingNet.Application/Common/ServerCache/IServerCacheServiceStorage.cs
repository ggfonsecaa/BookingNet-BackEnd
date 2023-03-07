namespace BookingNet.Application.Common.ServerCache
{
    public interface IServerCacheServiceStorage<TEntity>
    {
        public Task<TEntity> GetDataFromCacheAsync();
        public Task<IEnumerable<TEntity>> GetDataListFromCacheAsync();
        public Task SetDataToCacheAsync(TEntity data);
        public Task SetDataListToCacheAsync(IEnumerable<TEntity> data);
        public Task RemoveDataFromCacheAsync(string key);
    }
}