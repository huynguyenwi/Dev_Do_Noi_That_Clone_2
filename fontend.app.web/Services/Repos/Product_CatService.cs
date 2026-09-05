using CRM.data;
using csdl.web.Services;
using Microsoft.Extensions.Caching.Memory;
using ServiceStack.OrmLite;

namespace core.Services
{
    public class Product_CatService : HuyService, Product_CatService_I
    {
        private readonly IMemoryCache _cache;
        public Product_CatService(IMemoryCache cache)
        {
            _cache = cache;
        }


        public async Task<List<vw_Category>> GetAllCate()
        {
            //const string cacheKey = "GetAllCate_Cache";

            //if (_cache.TryGetValue(cacheKey, out List<vw_Category> cachedData))
            //{
            //    return cachedData; // lấy từ cache
            //}

            using (var db = _connectionData.OpenDbConnection())
            {
                var query = db.From<vw_Category>()
                              .OrderByDescending(x => x.categoryId);

                try
                {
                    var rows = await db.SelectAsync(query);
                    var result = rows.ToList();

                    // set cache
                    //_cache.Set(cacheKey, result, DefaultDurationcache);

                    return result;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
