using AutoMapper;
using EccomerceApi.Models;
using EccomerceApi.ModelsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            // Domain object for example Brand can convert/map to CreateBrandDTO and backwards. 
            CreateMap<Brand, CreateBrandDTO>().ReverseMap();
            CreateMap<Brand, UpdateBrandDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();

            CreateMap<UserType, CreateUserTypeDTO>().ReverseMap();
            CreateMap<UserType, UpdateUserTypeDTO>().ReverseMap();
            CreateMap<UserType, UserTypeDTO>().ReverseMap();

            CreateMap<Tag, CreateTagDTO>().ReverseMap();
            CreateMap<Tag, UpdateTagDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Cart, CreateCartDTO>().ReverseMap();
            CreateMap<Cart, UpdateCartDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();

            CreateMap<CartItem, CreateCartItemDTO>().ReverseMap();
            CreateMap<CartItem, UpdateCartItemDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();

            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();

            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<OrderItem, CreateOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<Payment, CreatePaymentDTO>().ReverseMap();
            CreateMap<Payment, UpdatePaymentDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();

            CreateMap<ProductCategory, CreateProductCategoryDTO>().ReverseMap();
            CreateMap<ProductCategory, UpdateProductCategoryDTO>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();

            CreateMap<ProductReview, CreateProductReviewDTO>().ReverseMap();
            CreateMap<ProductReview, UpdateProductReviewDTO>().ReverseMap();
            CreateMap<ProductReview, ProductReviewDTO>().ReverseMap();

            CreateMap<ProductTag, CreateProductTagDTO>().ReverseMap();
            CreateMap<ProductTag, UpdateProductTagDTO>().ReverseMap();
            CreateMap<ProductTag, ProductTagDTO>().ReverseMap();

            CreateMap<Shipment, CreateShipmentDTO>().ReverseMap();
            CreateMap<Shipment, UpdateShipmentDTO>().ReverseMap();
            CreateMap<Shipment, ShipmentDTO>().ReverseMap();

        }
    }
}
