using EcommerceData.Models;
using System.Threading.Tasks;

namespace EcommerceCore.IRepository
{
    public interface IUnitOfWork
    {
        //basically a register for every variation of Generic Repository
        IGenericRepository<Brand> Brands { get; }
        IGenericRepository<UserType> UserTypes { get; }
        IGenericRepository<Tag> Tags { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<User> Users { get; }
/*        IGenericRepository<Cart> Carts { get; }
        IGenericRepository<CartItem> CartItems { get; }*/
        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<ProductCategory> ProductCategories { get; }
        IGenericRepository<ProductReview> ProductReviews { get; }
        IGenericRepository<ProductTag> ProductTags { get; }
        IGenericRepository<Shipment> Shipments { get; }
        // one more operation which is Save. to save all modified record at once
        Task Save();
    }
}
