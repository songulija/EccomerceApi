using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceData.Models
{
    /// <summary>
    /// CostPrice is true price of product. price that you sell is more
    /// </summary>
    public class Product : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Brand))]
        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public string OtherBrand { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double? CostPrice { get; set; }
        public bool IsDiscount { get; set; }
        public double? DiscountPrice { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Code { get; set; }
        public double? LengthWithoutPackaging { get; set; }
        public double? WidthWithoutPackaging { get; set; }
        public double? HeightWithoutPackaging { get; set; }
        public double? LengthWithPackaging { get; set; }
        public double? WidthWithPackaging { get; set; }
        public double? HeightWithPackaging { get; set; }
        public int? WeightGross { get; set; }
        public int? WeightNetto { get; set; }
        public string PackagingBoxCode { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
