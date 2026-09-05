using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.data;
using core.data;
namespace csdl.web.Services
{
    public interface ProductService_I
    {
        Task<List<vw_Products_Home>> GetProductsHome();
        Task<List<vw_Products_Home>> GetAllProduct(Product_SearchModel page);
        Task<long> CountViewAll(Product_SearchModel page);
        Task<vw_Product> GetViewByID(Guid productId);
        Task<List<vw_ProductModels>> GetProductModel(Guid productId);
        Task<List<vw_ProductModelImages>> GetProductModelImage(Guid productId);
        Task<List<vw_Products_Home>> GetProductRelated(Guid categoryId);
        Task<List<vw_Products_Home>> GetTrendingProduct(int length);
    }
}
