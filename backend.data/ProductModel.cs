using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.Models;

namespace CRM.data
{
    [Alias("ProductModels")]
    public partial class ProductModels
    {
        public Guid modelId { get; set; }
        public Guid productId { get; set; }
        public string? modelName { get; set; }
        public string? description { get; set; }
        public decimal? virtualPrice { get; set; }
        public decimal? realPrice { get; set; }
        public decimal? capitalPrice { get; set; }
        public DateTime? created { get; set; }
        public DateTime? modified { get; set; }
    }

    [Alias("ProductModelImages")]
    public partial class ProductModelImages
    {
        public Guid modelImageId { get; set; }
        public Guid modelId { get; set; }
        public string? imageUrl { get; set; }
        public bool? isMain { get; set; }
        public DateTime? created { get; set; }
        public bool? isPublic { get; set; }
    }

    [Alias("vw_ProductModels")]
    public partial class vw_ProductModels
    {
        public Guid modelId { get; set; }
        public Guid productId { get; set; }
        public string? modelName { get; set; }
        public string? description { get; set; }
        public decimal? virtualPrice { get; set; }
        public decimal? realPrice { get; set; }
        public decimal? capitalPrice { get; set; }
        public DateTime? created { get; set; }
        public DateTime? modified { get; set; }
    }

    [Alias("vw_ProductModelImages")]
    public partial class vw_ProductModelImages
    {
        public Guid modelImageId { get; set; }
        public Guid modelId { get; set; }
        public string? imageUrl { get; set; }
        public bool? isMain { get; set; }
        public DateTime? created { get; set; }
        public bool? isPublic { get; set; }
    }
}
