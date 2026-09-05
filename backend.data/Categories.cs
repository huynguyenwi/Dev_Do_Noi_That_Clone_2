using core.Models;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.data
{
    [Alias("Categories")]
    public partial class Categories
    {
        public Guid categoryId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime? created { get; set; }
        public DateTime? modified { get; set; }
        public bool? isPublic { get; set; }
        public bool? isDeleted { get; set; }
    }
    [Alias("vw_Category")]
    public partial class vw_Category
    {
        public Guid categoryId { get; set; }
        public string? categoryName { get; set; }
        public string? description { get; set; }
        public DateTime? created { get; set; }
        public DateTime? modified { get; set; }
        public bool? isPublic { get; set; }
        public bool? isDeleted { get; set; }
        public int? totalProducts { get; set; }
    }
}
