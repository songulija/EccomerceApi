using EcommerceCore.IRepository;
using EcommerceData.Models;
using System;
using System.Threading.Tasks;

namespace EcommerceCore.Repository
{
    // inherit from IUnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Brand> _brands;
        private IGenericRepository<UserType> _userTypes;
        private IGenericRepository<Tag> _tags;
        private IGenericRepository<Category> _categories;
        private IGenericRepository<Product> _products;
        private IGenericRepository<User> _users;
/*        private IGenericRepository<Cart> _carts;
        private IGenericRepository<CartItem> _cartItems;*/
        private IGenericRepository<Comment> _comments;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<OrderItem> _orderItems;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<ProductCategory> _productCategories;
        private IGenericRepository<ProductReview> _productReviews;
        private IGenericRepository<ProductTag> _productTags;
        private IGenericRepository<Shipment> _shipments;
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        // if transportations is null then provide new GenericRepository of type Transportations. providing context
        public IGenericRepository<Brand> Brands => _brands ??= new GenericRepository<Brand>(_context);

        public IGenericRepository<UserType> UserTypes => _userTypes ??= new GenericRepository<UserType>(_context);
        public IGenericRepository<Tag> Tags => _tags ??= new GenericRepository<Tag>(_context);
        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);
        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);
        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
/*        public IGenericRepository<Cart> Carts => _carts ??= new GenericRepository<Cart>(_context);
        public IGenericRepository<CartItem> CartItems => _cartItems ??= new GenericRepository<CartItem>(_context);*/
        public IGenericRepository<Comment> Comments => _comments ??= new GenericRepository<Comment>(_context);
        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);
        public IGenericRepository<OrderItem> OrderItems => _orderItems ??= new GenericRepository<OrderItem>(_context);
        public IGenericRepository<Payment> Payments => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<ProductCategory> ProductCategories => _productCategories ??= new GenericRepository<ProductCategory>(_context);
        public IGenericRepository<ProductReview> ProductReviews => _productReviews ??= new GenericRepository<ProductReview>(_context);
        public IGenericRepository<ProductTag> ProductTags => _productTags ??= new GenericRepository<ProductTag>(_context);
        public IGenericRepository<Shipment> Shipments => _shipments ??= new GenericRepository<Shipment>(_context);
        // garbage collector
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
