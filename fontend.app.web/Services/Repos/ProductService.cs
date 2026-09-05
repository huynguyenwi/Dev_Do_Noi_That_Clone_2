using core.data;
using CRM.data;
using csdl.web.Services;
using Microsoft.Extensions.Caching.Memory;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using static core.data.ResultModel;
namespace core.Services
{
    public class ProductService : HuyService, ProductService_I
    {
        private readonly IMemoryCache _cache;
        public ProductService(
          IMemoryCache cache
        )
        {
            _cache = cache;
        }

        public async Task<vw_Product> GetViewByID(Guid productId)
        {
            try
            {
                using (var db = _connectionData.OpenDbConnection())
                {
                    var query = db.From<vw_Product>().Where(x => x.productId == productId);

                    var result = await db.SingleAsync<vw_Product>(query);
                    return result;
                }    
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<vw_ProductModels>> GetProductModel(Guid productId)
        {
            try
            {
                using (var db = _connectionData.OpenDbConnection())
                {
                    var query = Query_GetProductModel(db, productId);
                    var rows = await db.SelectAsync(query);
                    //var result = rows.GroupBy(r => r.productId).Select(g => g.First()).ToList();

                    // ✅ Lưu cache (5 phút)
                    // _cache.Set(cacheKey, result, DefaultDurationcache);

                    return rows;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private SqlExpression<vw_ProductModels> Query_GetProductModel(IDbConnection db, Guid productId)
        {
            var query = db.From<vw_ProductModels>();
            query = query.Where((vw_ProductModels e) => (e.productId == productId));
            return query;
        }
        public async Task<List<vw_Products_Home>> GetProductRelated(Guid categoryId)
        {
            try
            {
                using(var db = _connectionData.OpenDbConnection())
                {
                    var product = Query_GetProductRealated(db, categoryId);
                    var rows = await db.SelectAsync(product);

                    return rows;
                }    
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private SqlExpression<vw_Products_Home> Query_GetProductRealated (IDbConnection db, Guid categoryId)
        {
            var query = db.From<vw_Products_Home>();
            query = query.Where((vw_Products_Home e) => e.categoryId == categoryId);
            return query;
        }

        public async Task<List<vw_ProductModelImages>> GetProductModelImage(Guid productId)
        {
            try
            {
                using (var db = _connectionData.OpenDbConnection())
                {
                    var query = Query_GetProductModelImage(db, productId);
                    var rows = await db.SelectAsync(query);
                    //var result = rows.GroupBy(r => r.productId).Select(g => g.First()).ToList();

                    // ✅ Lưu cache (5 phút)
                    // _cache.Set(cacheKey, result, DefaultDurationcache);

                    return rows;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //private SqlExpression<vw_ProductModelImages> Query_GetProductModelImage(IDbConnection db, Guid productId)
        //{
        //    var query = db.From<vw_ProductModelImages, vw_ProductModels>();
        //    query = query.Where((vw_ProductModelImages e, vw_ProductModels x) => (e.isPublic == true) && x.productId == productId && x.modelId == e.modelId);
        //    return query;
        //}


        private SqlExpression<vw_ProductModelImages> Query_GetProductModelImage(IDbConnection db, Guid productId)
        {
            var query = db.From<vw_ProductModelImages>()
                          .Where(e => e.isPublic == true)
                          .And(e => Sql.In(e.modelId,
                              db.From<vw_ProductModels>()
                                .Where(x => x.productId == productId)
                                .Select(x => x.modelId)
                          ));

            return query;
        }


        public async Task<List<vw_Products_Home>> GetProductsHome()
        {

            //string cacheKey = $"HomeProducts_{page.offset}_{page.limit}_{page.search}_{page.home_type}_{page.orderby}";

            //if (_cache.TryGetValue(cacheKey, out List<vw_Products_Home> cachedData))
            //{
            //    return cachedData; // ✅ Trả về cache
            //}
            using (var db = _connectionData.OpenDbConnection())
            {
                var query = Query_ViewAllItemHome(db);
                var rows = await db.SelectAsync(query);
                //var result = rows.GroupBy(r => r.productId).Select(g => g.First()).ToList();

                // ✅ Lưu cache (5 phút)
                // _cache.Set(cacheKey, result, DefaultDurationcache);

                return rows;
            }
        }


        private SqlExpression<vw_Products_Home> Query_ViewAllItemHome(IDbConnection db)
        {
            var query = db.From<vw_Products_Home>();
            //query = query.Where((vw_Products_Home e) => (e.isDeleted == false || e.isDeleted == null) && e.isPublic == true);
            return query;
        }


        public async Task<List<vw_Products_Home>> GetAllProduct(Product_SearchModel page)
        {
            if (page == null) page = new Product_SearchModel() { offset = 1, limit = 100 };
            if (page.search == null) page.search = "";

            using (var db = _connectionData.OpenDbConnection())
            {
                var query = Query_ViewAllItem(db, page);

                // lọc theo từ khóa
                if (!string.IsNullOrWhiteSpace(page.search))
                {
                    var keyword = page.search.Trim();
                    query = query.Where(x => x.name.Contains(keyword));
                }

                // lọc theo chuyên mục
                //if (page.PublicationCateId.HasValue)
                //{
                //    query = query.Where(x => x.Catid == page.PublicationCateId.Value);
                //}

                if (page.cateid.HasValue && page.cateid.Value != Guid.Empty)
                {
                    query = query.Where(x => x.categoryId == page.cateid.Value);
                }


                // sắp xếp
                if (page.orderby == 1)
                    query = query.OrderByDescending(x => x.productId);
                else if (page.orderby == 2)
                    query = query.OrderBy(x => x.productId);

                // phân trang
                query = query.Skip((page.offset - 1) * page.limit).Take(page.limit);

                Console.WriteLine(query.ToSelectStatement());
                var rows = await db.SelectAsync(query);
                return rows.ToList();
            }
        }



        public async Task<long> CountViewAll(Product_SearchModel page)
        {
            if (page.search == null) page.search = "";
            using (var db = _connectionData.OpenDbConnection())
            {
                var query = Query_ViewAllItem(db, page);
                query = query.Select(x => x.productId);
                return await db.CountAsync(query);
            }
        }

        private SqlExpression<vw_Products_Home> Query_ViewAllItem(IDbConnection db, Product_SearchModel page)
        {
            var query = db.From<vw_Products_Home>()
                          .OrderByDescending(x => x.productId);
            //query = query.Where(e => e.Is == false);
            if (page.cateid.HasValue && page.cateid.Value != Guid.Empty)
            {
                query = query.Where(e => e.categoryId == page.cateid.Value);
            }

            if (!string.IsNullOrWhiteSpace(page.search))
            {
                query = query.Where(e => e.name.Contains(page.search));
            }

            return query;
        }
        public async Task<List<vw_Products_Home>> GetTrendingProduct(int length)
        {
            //string cacheKey = $"TrendingPublication_{length}";

            //if (!_cache.TryGetValue(cacheKey, out List<vw_Publication> cachedData))
            //{
            using (var db = _connectionData.OpenDbConnection())
            {
                var query = db.From<vw_Products_Home>()
                              .Skip(0)
                              .Take(length);

                var rows = await db.SelectAsync(query);
                return rows.ToList();
                //    cachedData = rows.ToList();

                //    _cache.Set(cacheKey, cachedData, DefaultDurationcache);
                //}
                //}

                //return cachedData;
            }
        }
    }
}


