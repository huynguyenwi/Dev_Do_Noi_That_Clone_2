using System;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System.Collections.Generic;
using core.Models;

namespace CRM.data
{
    [Alias("Product")]
    public partial class Product
    {
        public Guid productId { get; set; }
        public Guid categoryId { get; set; }
        public string? name { get; set; }
        public string? shortDescription { get; set; }
        public string? longDescription { get; set; }
        public decimal? virtualPrice { get; set; }
        public decimal? realPrice { get; set; }
        public decimal? capitalPrice { get; set; }
        public decimal? averageStar { get; set; }
        public DateTime? created { get; set; }
        public DateTime? modified { get; set; }
        public bool? isPublic { get; set; }
        public bool? isDeleted { get; set; }
    }
    [Alias("vw_Product")]
    public partial class vw_Product
    {
        public Guid productId { get; set; }
        public Guid categoryId { get; set; }
        public string? name { get; set; }
        public string? shortDescription { get; set; }
        public string? longDescription { get; set; }
        public decimal? virtualPrice { get; set; }
        public decimal? realPrice { get; set; }
        public decimal? capitalPrice { get; set; }
        public decimal? averageStar { get; set; }
        public DateTime? created { get; set; }
        public DateTime? modified { get; set; }
        public bool? isPublic { get; set; }
        public bool? isDeleted { get; set; }
    }

    [Alias("vw_Products_Home")]
    public partial class vw_Products_Home
    {
        public Guid productId { get; set; }
        public Guid categoryId { get; set; }
        public string name { get; set; }
        public decimal? virtualPrice { get; set; }
        public decimal? realPrice { get; set; }
        public decimal? capitalPrice { get; set; }
        public decimal? averageStar { get; set; }
        public string imgProducts { get; set; } // ảnh chính
        public string imgPImage { get; set; }   // ảnh hover
    }

    //}

    public partial class Product_ViewDetail
    {
        public vw_Product product { get; set; }
        public List<vw_ProductModels> arrProductModel { get; set; }
        public List<vw_ProductModelImages> arrProductModelImage { get; set; }

        public List<vw_Products_Home> arrRelated { get; set; }

        //public vw_Company company { get; set; }
        //public List<vw_Product_Catalog_Menu> arrCatelog { get; set; }
        //public List<vw_Products_Tag> tag { get; set; }
        //public List<Product_Vote> arrVote { get; set; }
        //public List<vw_Member_Home> arrExpert { get; set; }
        //public List<vw_Company_Home> arrInter { get; set; }
        //public List<vw_Products_Home> arrRelated { get; set; }
        //public List<vw_Products_Home> arrView { get; set; }
    }

    public partial class Product_SearchModel
    {
        public int offset;
        public int limit;
        public string search;
        /// <summary>
        /// 1: Sản phẩm nổi bật
        /// 2: Sản phẩm bán chay
        /// 3: Sản phẩm xem nhiều nhất
        /// </summary>
        public int home_type;
        //public string cateid;
        public Guid? cateid;
        public Guid? ProductId;
        public int orderby;
        public string businessId;
        public string expertId;
        public string CountryId;
        public Guid Companyid;
        public string tagId;

    }
    public class Product_Filter : PagingModel
    {

        public int Count;
        public string mess;
        public bool IsSuccess;
    }




}
