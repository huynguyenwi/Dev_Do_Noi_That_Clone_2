using CRM.data;

namespace csdl.web.Services
{
    public interface Product_CatService_I
    {
        Task<List<vw_Category>> GetAllCate();
    }
}
