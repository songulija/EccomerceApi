﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateCartItemDTO
    {
     /*   [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime Date { get; set; }*/
    }
    public class UpdateCartItemDTO : CreateCartItemDTO
    {
    }
    public class CartItemDTO : CreateCartItemDTO
    {
        public int Id { get; set; }
/*        public CartDTO Cart { get; set; }
        public ProductDTO Product { get; set; }*/
    }
}
