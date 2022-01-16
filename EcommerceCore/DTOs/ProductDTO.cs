using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int? BrandId { get; set; }
        public string OtherBrand { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        public double? CostPrice { get; set; }
        public bool IsDiscount { get; set; }
        public double? DiscountPrice { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        [Required]
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
    }
    public class UpdateProductDTO : CreateProductDTO
    {

    }
    public class ProductDTO : CreateProductDTO
    {
        public int Id { get; set; }
        public BrandDTO Brand { get; set; }
        public virtual IList<ProductTagDTO> ProductTags { get; set; }
       /* public virtual IList<CartItemDTO> CartItems { get; set; }*/
        public virtual IList<ProductReviewDTO> ProductReviews { get; set; }
        public virtual IList<ProductCategoryDTO> ProductCategories { get; set; }
        public virtual IList<CommentDTO> Comments { get; set; }
        public virtual IList<OrderItemDTO> OrderItems { get; set; }
    }
}
